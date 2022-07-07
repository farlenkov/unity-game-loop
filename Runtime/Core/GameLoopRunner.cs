using System.Collections;
using System.Collections.Generic;
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
            Call(Time.deltaTime, Loop.DrawGizmos);
        }

        void OnDestroy()
        {
            Call(Time.time, Loop.Destroy);
        }

        void OnApplicationQuit()
        {
            Call(Time.time, Loop.Quit);
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