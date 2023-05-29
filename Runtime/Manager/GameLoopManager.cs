using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityGameLoop
{
    public abstract class GameLoopManager<LOOP>
        where LOOP : GameLoop
    {
        protected LOOP Loop;
        protected float ElapsedTime => UnityEngine.Time.time;
        public bool Enabled = true;

        public void Init(LOOP loop)
        {
            Loop = loop;

            if (Enabled)
                OnInit();
        }

        protected virtual void OnInit()
        {

        }

        // HELPERS

        public GameObject Instantiate(GameObject original) => Object.Instantiate(original);
        public GameObject Instantiate(GameObject original, Vector3 pos, Quaternion rot) => Object.Instantiate(original, pos, rot);

        public Object Instantiate(Object original) => Object.Instantiate(original);
        public void DestroyObject(Object original) => Object.Destroy(original);

        public EntityManager EntityManager => Loop.EntityManager;
    }
}