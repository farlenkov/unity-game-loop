using Unity.Entities;
using UnityEngine;
using UnityServiceRegistry;

namespace UnityGameLoop
{
    public abstract class EntityBehaviour : MonoBehaviour, IComponentData
    {
        public EntityManager EntityManager { get; private set; }
        public Entity Entity { get; private set; }

        protected virtual void Start()
        {
            ServiceRegistry.GetService(out EntityManager entityManager);
            EntityManager = entityManager;
            Entity = entityManager.SpawnObject(this);
        }

        protected virtual void OnDestroy()
        {
            EntityManager.DestroyObject(Entity);
        }
    }
}
