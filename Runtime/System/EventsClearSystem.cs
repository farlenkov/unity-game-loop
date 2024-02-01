using Unity.Entities;

namespace UnityGameLoop
{
    public partial class EventsClearSystem : GameLoopSystem<GameLoop>
    {
        protected override void OnUpdate()
        {
            ClearSpawnEvents();
            ClearDestroyEvents();
        }

        void ClearSpawnEvents()
        {
            Entities
                .WithStructuralChanges()
                .WithAll<SpawnEvent>()
                .ForEach((Entity entity) =>
                {
                    EntityManager.RemoveComponent<SpawnEvent>(entity);

                }).Run();
        }

        void ClearDestroyEvents()
        {
            Entities
                .WithStructuralChanges()
                .WithAll<DestroyEvent>()
                .ForEach((Entity entity) =>
                {
                    EntityManager.DestroyEntity(entity);

                }).Run();
        }
    }
}
