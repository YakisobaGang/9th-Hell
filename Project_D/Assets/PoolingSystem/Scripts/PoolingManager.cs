using System.Collections.Generic;
using UnityEngine;

namespace FabriciohodDev.PoolingSystem.Scripts
{
    public class PoolingManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectPool> objectPools = new List<ObjectPool>();

        private readonly Dictionary<string, Queue<GameObject>> ObjectsInstance =
            new Dictionary<string, Queue<GameObject>>();

        public static PoolingManager Instance { get; private set; }

        private void Awake()
        {
            if (!(Instance is null) && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            foreach (var pool in objectPools)
            {
                var tempQueue = new Queue<GameObject>();

                for (var j = 0; j < pool.InitCount(); j++)
                {
                    var obj = Instantiate(pool.ObjectPrefab());
                    obj.SetActive(false);
                    tempQueue.Enqueue(obj);
                }

                ObjectsInstance.Add(pool.PoolName(), tempQueue);
            }
        }

        public static GameObject SpawnObject(string poolName, Vector3 position, Quaternion rotation)
        {
            if (!HasPool(poolName))
                return null;

            var queue = Instance.ObjectsInstance[poolName];

            if (queue.Count > 0)
            {
                var obj = queue.Dequeue();

                obj.transform.position = position;
                obj.transform.rotation = rotation;

                obj.SetActive(true);
                return obj;
            }

            if (!Instance.PoolCanExpand(poolName)) return null;

            return Instantiate(Instance.GetPool(poolName).ObjectPrefab(), position, rotation);
        }

        public static void AddObjectBackToPool(string poolName, GameObject obj)
        {
            if (!HasPool(poolName))
                return;


            obj.SetActive(false);
            Instance.ObjectsInstance[poolName].Enqueue(obj);
        }

        private bool PoolCanExpand(string poolName)
        {
            return GetPool(poolName).CanExpend();
        }

        private ObjectPool GetPool(string poolName)
        {
            return objectPools.Find(pool => pool.PoolName() == poolName);
        }

        private static bool HasPool(string poolName)
        {
            if (Instance.ObjectsInstance.ContainsKey(poolName))
                return true;

            Debug.LogError($"This {poolName} doesn't exits, create a Pool Object or check if you type something wrong");
            return false;
        }
    }
}