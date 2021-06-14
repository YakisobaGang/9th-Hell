using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD
{
    public class TransitionBrain : MonoBehaviour
    {
        public Animator anim;
        public float transitionTime = 1f;

        public void LoadNextLevel()
        {
            StartCoroutine(LoadingLevel((SceneManager.GetActiveScene().buildIndex + 1)));
        }

        public void LoadMainMenu()
        {
            StartCoroutine(LoadingMainMenu(0));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        IEnumerator LoadingLevel(int levelIndex)
        {
            anim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(levelIndex);
        }

        IEnumerator LoadingMainMenu(int levelIndex)
        {
            anim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(0);
        }
    }
}