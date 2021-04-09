using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Dialogue
{
    public class LoadSceneByIndexAfterTime : MonoBehaviour
    {
        [SerializeField] private float timeInSeconds;
        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(CLoadSceneAfterTime(sceneIndex));
        }

        private IEnumerator CLoadSceneAfterTime(int sceneIndex)
        {
            yield return new WaitForSeconds(timeInSeconds);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}