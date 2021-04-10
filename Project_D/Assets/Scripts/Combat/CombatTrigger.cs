using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Combat
{
    public class CombatTrigger : MonoBehaviour
    {
        [SerializeField] private int combatSceneIndex;
        [SerializeField] private float timeToChangeScene;
        private bool canLoadScene = false;

        public void ChangeScene()
        {
            StartCoroutine(WaitTimeToChangeScene());
        }

        private IEnumerator LoadSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(combatSceneIndex);

            while (!asyncLoad.isDone && canLoadScene)
            {
                yield return null;
            }
        }

        private IEnumerator WaitTimeToChangeScene()
        {
            yield return new WaitForSeconds(timeToChangeScene);
            yield return LoadSceneAsync();
            canLoadScene = true;
        }
    }
}