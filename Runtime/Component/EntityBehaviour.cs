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
        public EntityManager EntityManager { get; private set; }
        public Entity Entity { get; private set; }
        public EntityBehaviourMode Mode = EntityBehaviourMode.StartDestroy;

        //static StringBuilder logBuilder = new StringBuilder();

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

            var components = GetComponents<IEntityComponent>();
            //logBuilder.AppendLine($"[EntityBehaviour] {gameObject.name}");

            for (var i = 0; i < components.Length; i++)
            {
                var component = components[i];
                entityManager.AddComponentObject(Entity, component);
                //logBuilder.AppendLine(component.ToString());
            }

            //Log.InfoEditor(logBuilder.ToString());
            //logBuilder.Clear();
        }

        public void DestroyEntity()
        {
            EntityManager.TryAddComponent<DestroyEvent>(Entity);
            Entity = default;
        }
    }
}
