using System;
using System.Text;
using Unity.Entities;
using UnityEngine;
using UnityServiceRegistry;
using UnityUtility;

namespace UnityGameLoop
{
    public sealed class EntityBehaviour : MonoBehaviour, IComponentData
    {
        public EntityBehaviourMode Mode = EntityBehaviourMode.StartDestroy;
        public EntityManager EntityManager { get; private set; }
        public Entity Entity { get; private set; }

        void Start()
        {
            if (Mode == EntityBehaviourMode.StartDestroy)
                CreateEntity();
        }

        void OnDestroy()
        {
            if (Mode == EntityBehaviourMode.StartDestroy)
                DestroyEntity();
        }

        void OnEnable()
        {
            if (Mode == EntityBehaviourMode.EnableDisable)
                CreateEntity();
        }

        void OnDisable()
        {
            if (Mode == EntityBehaviourMode.EnableDisable)
                DestroyEntity();
        }

        public void CreateEntity()
        {
            if (!ServiceRegistry.TryGetService(out EntityManager entityManager))
            {
                Log.Error($"[EntityBehaviour: CreateEntity] Service 'EntityManager' not found for '{gameObject.name}'");
                return;
            }

            if (entityManager.Exists(Entity))
            {
                Log.Error($"[EntityBehaviour: CreateEntity] Entity already created for '{gameObject.name}'");
                return;
            }

            EntityManager = entityManager;
            Entity = entityManager.CreateEntity();
            entityManager.AddComponentData(Entity, new SpawnEvent());

            var entityComponents = GetComponents<IEntityComponent>();

            for (var i = 0; i < entityComponents.Length; i++)
            {
                var entityComponent = entityComponents[i] as MonoBehaviour;

                if (entityComponent.enabled)
                    entityManager.AddComponentObject(Entity, entityComponent);
            }
        }

        public void DestroyEntity()
        {
            EntityManager.TryAddComponent<DestroyEvent>(Entity);
            Entity = default;
        }
    }
}