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
        protected float ElapsedTime { get; private set; }
        public new bool Enabled = true;

        // INIT

        public void Init(LOOP loop)
        {
            Loop = loop;

            if (Enabled)
            {
                Loop.World.AddSystem(this);
                Init();
            }
        }

        protected virtual void Init()
        {
            Loop.Start.Add(Start);
            Loop.Update.Add(Update);
            Loop.Destroy.Add(Destroy);
        }

        // START

        void Start(float dt)
        {
            OnStart();
        }

        protected virtual void OnStart()
        {

        }

        // UPDATE

        protected void Update(float dt)
        {
            DeltaTime = dt;
            ElapsedTime = UnityEngine.Time.time;

            Update();
        }

        protected override void OnUpdate()
        {

        }

        // DESTROY

        protected virtual void Destroy(float dt)
        {
            if (Loop.World.IsCreated)
                Loop.World.DestroySystem(this);
        }

        // HELPERS

        public GameObject Instantiate(GameObject original) => Object.Instantiate(original);
        public Object Instantiate(Object original) => Object.Instantiate(original);
        public void DestroyObject(Object original) => Object.Destroy(original);
    }
}