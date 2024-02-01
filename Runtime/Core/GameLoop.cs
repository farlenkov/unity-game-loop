using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace UnityGameLoop
{
    public class GameLoop : IDisposable
    {
        // FUNC LIST

        public GameLoopFuncList Start { get; private set; }
        public GameLoopFuncList Update { get; private set; }
        public GameLoopFuncList FixedUpdate { get; private set; }
        public GameLoopFuncList LateUpdate { get; private set; }
        public GameLoopFuncList DrawGizmos { get; private set; }
        public GameLoopFuncList Destroy { get; private set; }
        public GameLoopFuncList Quit { get; private set; }

        //ECS

        public World World { get; private set; }
        public EntityManager EntityManager { get; private set; }

        // RUNNER

        public Transform RootTransform { get; internal set; }

        public GameLoop()
        {
            InitInternal();
        }
            
        public GameLoop(Transform rootTransform)
        {
            RootTransform = rootTransform;
            InitInternal();
        }

        void InitInternal()
        {
            World = new World(GetType().Name);
            EntityManager = World.EntityManager;

            Start = new GameLoopFuncList();
            Update = new GameLoopFuncList();
            FixedUpdate = new GameLoopFuncList();
            LateUpdate = new GameLoopFuncList();
            DrawGizmos = new GameLoopFuncList();
            Destroy = new GameLoopFuncList();
            Quit = new GameLoopFuncList();
        }

        public void Dispose()
        {
            if (World.IsCreated)
            {
                World.Dispose();
                World = null;
            }
        }
    }
}