using Unity.Entities;

namespace UnityGameLoop
{
    public static class EntityManagerExt
    {
        public static bool TryAddComponent<T>(
            this EntityManager entityManager, 
            Entity entity) 
            where T : unmanaged, IComponentData
        {
            try
            {
                entityManager.AddComponentData(entity, new T());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
