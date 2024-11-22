using Unity.Entities;
using UnityEngine;
using UnityUtility;

namespace UnityGameLoop
{
    public interface IEntityComponent : IComponentData
    {

    }

    [RequireComponent(typeof(EntityBehaviour))]
    public abstract class EntityComponent : MonoBehaviour, IEntityComponent
    {
        [field: ReadOnly]
        [field: SerializeField]
        public EntityBehaviour EntityBehaviour { get; private set; }

        public Entity Entity => EntityBehaviour.Entity;
        public EntityManager EntityManager => EntityBehaviour.EntityManager;

        protected virtual void OnValidate()
        {
            if (EntityBehaviour == null)
                EntityBehaviour = GetComponent<EntityBehaviour>();
        }
    }
}
