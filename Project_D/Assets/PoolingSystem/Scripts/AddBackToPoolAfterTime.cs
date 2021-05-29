using UnityEngine;

namespace FabriciohodDev.PoolingSystem.Scripts
{
    public class AddBackToPoolAfterTime : MonoBehaviour
    {
        [SerializeField] private ObjectPool pool;
        [SerializeField] private float timeInSeconds = 3f;
        [SerializeField] private bool addBackOnEnable;

        private void OnEnable()
        {
            if(!addBackOnEnable)
                return;
            
            Invoke(nameof(AddBackToPool), timeInSeconds);
        }

        public void AddBackToPool()
        {
            PoolingManager.AddObjectBackToPool(pool.PoolName(), gameObject);
        }
    }
}