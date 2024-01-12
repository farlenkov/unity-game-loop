using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace UnityGameLoop
{
    public abstract class GameLoopRunner : MonoBehaviour
    {

    }

    public abstract class GameLoopRunner<LOOP> : GameLoopRunner
        where LOOP : GameLoop
    {
        protected LOOP Loop;

        void Start()
        {
            Call(0, Loop.Start);
        }

        protected virtual void Update()
        {
            Call(Time.deltaTime, Loop.Update);
        }

        void FixedUpdate()
        {
            Call(Time.fixedDeltaTime, Loop.FixedUpdate);
        }

        void LateUpdate()
        {
            Call(Time.deltaTime, Loop.LateUpdate);
        }

        void OnDrawGizmos()
        {
            if (Loop != null)
                Call(Time.deltaTime, Loop.DrawGizmos);
        }

        protected virtual void OnDestroy()
        {
            Call(Time.time, Loop.Destroy);
        }

        void OnApplicationQuit()
        {
            Call(Time.time, Loop.Quit);
        }

        protected T CreateSystem<T>() where T : SystemBase, new()
        {
#if UNITY_2022_3_OR_NEWER
            return Loop.World.CreateSystemManaged<T>();
#else
            return Loop.World.CreateSystem<T>();
#endif
        }

        protected T CreateManager<T>() where T : GameLoopManager, new()
        {
            var system = new T();
            return system;
        }

        protected void Call(float dt, GameLoopFuncList funcs)
        {
            var time = Time.time;
            var count = funcs.Count;

            for (var i = 0; i < count; i++)
            {
                var func = funcs[i];

                if (func.Interval == 0)
                {
                    func.Exec(dt);
                }
                else if (func.NextTick <= time)
                {
                    func.Exec(time - func.PrevTick);
                    func.PrevTick = time;
                    func.NextTick = time + func.Interval;
                }
            }
        }
    }
}