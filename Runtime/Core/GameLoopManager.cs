using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Farlenkov
{
    public abstract class GameLoopManager<LOOP>
        where LOOP : GameLoop
    {
        protected LOOP Loop;
        public bool Enabled = true;

        protected abstract void Init();

        public void Init(LOOP loop)
        {
            Loop = loop;

            if (Enabled)
                Init();
        }

        // HELPERS

        public GameObject Instantiate(GameObject original) => Object.Instantiate(original);
        public GameObject Instantiate(GameObject original, Vector3 pos, Quaternion rot) => Object.Instantiate(original, pos, rot);

        public Object Instantiate(Object original) => Object.Instantiate(original);
        public void DestroyObject(Object original) => Object.Destroy(original);

        public Coroutine StartCoroutine(IEnumerator routine) => Loop.StartCoroutine(routine);
        public void StopCoroutine(Coroutine routine) => Loop.StopCoroutine(routine);
    }
}