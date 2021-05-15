using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProjectD
{
    public class SceneChanger : MonoBehaviour
    {
        public Animator transition;
        public Animator start;
        public float transitionDuration = 1f;
        
        public void LoadScene()
        {
            StartCoroutine(TransitionTime(1));
        }

        public void QuitGame()
        {
            StartCoroutine(TransitionExit(1));
        }

        IEnumerator TransitionTime(int arg) 
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionDuration);

            SceneManager.LoadScene(2);
        }

        IEnumerator TransitionExit(int arg)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionDuration);

            Application.Quit();
        }
    }
}
