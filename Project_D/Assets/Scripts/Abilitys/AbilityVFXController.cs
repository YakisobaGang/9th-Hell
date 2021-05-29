using FabriciohodDev.PoolingSystem.Scripts;
using UnityEngine;
using UnityEngine.Playables;

namespace ProjectD.Abilitys
{
    [CreateAssetMenu(fileName = "NewVFXController", menuName = "Create VFX Controller", order = 3)]
    public class AbilityVFXController : ScriptableObject
    {
        [HideInInspector] public GameObject vfxGameObject;
        [HideInInspector] public PlayableDirector playableDirector;
        public ObjectPool vfxPool;

        public void SpawnVFX(Vector3 position, Quaternion rotation)
        {
            vfxGameObject = PoolingManager.SpawnObject(vfxPool.PoolName(), position, rotation);
            playableDirector = vfxGameObject.GetComponent<PlayableDirector>();
        }

        public void PlayVFX()
        {
            playableDirector.Play();
        }
    }
}