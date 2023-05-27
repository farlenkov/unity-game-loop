using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace UnityGameLoop
{
    public abstract partial class GameLoopSystem<LOOP> : SystemBase
        where LOOP : GameLoop
    {
        protected LOOP Loop;
        protected float DeltaTime { get; private set; }
        protected float FixedDeltaTime { get; private set; }
        protected float ElapsedTime { get; private set; }
        public new bool Enabled = true;

        protected virtual bool NeedUpdate => true;

        // HELPERS

        public GameObject Instantiate(GameObject original, Transform parent = null) => Object.Instantiate(original, parent);
        public T Instantiate<T>(T monoBehaviour, Transform parent = null) where T : Object => Object.Instantiate(monoBehaviour, parent);
        public void DestroyObject(Object original) => Object.Destroy(original);

        // INIT

        protected virtual GameLoopFuncList UpdateList => Loop.Update;

        public void Init(LOOP loop)
        {
            Loop = loop;

            if (Enabled)
            {
                Loop.World.AddSystem(this);

                Loop.Start.Add(Start);
                UpdateList.Add(Update);
                Loop.Destroy.Add(Destroy);

                OnInit();
            }
        }

        protected virtual void OnInit()
        {
            
        }

        // START

        void Start(float dt)
        {
            DeltaTime = dt;
            FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
            ElapsedTime = UnityEngine.Time.time;

            OnStart();
        }

        protected virtual void OnStart()
        {

        }

        // UPDATE

        void Update(float dt)
        {
            DeltaTime = dt;
            FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
            ElapsedTime = UnityEngine.Time.time;

            if (!NeedUpdate)
                return;

            try
            {
                Update();
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        protected override void OnUpdate()
        {

        }

        // DESTROY

        void Destroy(float dt)
        {
            if (Loop.World.IsCreated)
                Loop.World.DestroySystem(this);
        }
    }
}