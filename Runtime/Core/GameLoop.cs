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

        // HELPERS

        public Coroutine StartCoroutine(IEnumerator routine) => LoopRunner.StartCoroutine(routine);
        public void StopCoroutine(Coroutine routine) => LoopRunner.StopCoroutine(routine);

        public Transform RootTransform => LoopRunner.transform;
        GameLoopRunner LoopRunner;

        public GameLoop(GameLoopRunner loopRunner)
        {
            LoopRunner = loopRunner;
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