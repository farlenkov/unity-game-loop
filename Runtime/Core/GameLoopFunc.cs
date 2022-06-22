using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameLoop
{
    public class GameLoopFunc
    {
        public Action<float> Exec;

        public float Interval;
        public float NextTick;
        public float PrevTick;
    }

    public class GameLoopFuncList : List<GameLoopFunc>
    {
        public void Add(
            Action<float> callback, 
            float interval = 0)
        {
            Add(new GameLoopFunc()
            {
                Exec = callback,
                Interval = interval
            });
        }
    }
}