using UnityEngine;

namespace FabriciohodDev.PoolingSystem.Scripts
{
    [CreateAssetMenu(fileName = "New Object Pool", menuName = "Pooling System/Create Object Pool", order = 0)]
    public class ObjectPool : ScriptableObject
    {
        [SerializeField] private int initialCount;
        [SerializeField] private string poolName;
        [SerializeField] private bool canExpend = true;
        [SerializeField] private GameObject objectPrefab;

        public int InitCount() => initialCount;
        public bool CanExpend() => canExpend;
        public string PoolName() => poolName;
        public GameObject ObjectPrefab() => objectPrefab;
    }
}