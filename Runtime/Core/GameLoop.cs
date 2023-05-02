using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace UnityGameLoop
{
    public class GameLoop
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

        public World World;
        public EntityManager EntityManager => World.EntityManager;

        // RUNNER

        public Transform RootTransform { get; private set; }

        public GameLoop(Transform rootTransform)
        {
            RootTransform = rootTransform;
            World = new World(GetType().Name);

            Start = new GameLoopFuncList();
            Update = new GameLoopFuncList();
            FixedUpdate = new GameLoopFuncList();
            LateUpdate = new GameLoopFuncList();
            DrawGizmos = new GameLoopFuncList();
            Destroy = new GameLoopFuncList();
            Quit = new GameLoopFuncList();
        }
    }
}