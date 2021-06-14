using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Utils
{
    public class LoadSceneAfterTime : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private int sceneIndex;

        public void LoadScene()
        {
            StartCoroutine(nameof(C_loadScene));
        }

        IEnumerator C_loadScene()
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}