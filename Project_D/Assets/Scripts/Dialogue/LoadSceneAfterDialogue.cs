using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Dialogue
{
    public class LoadSceneAfterDialogue : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;

        private void OnEnable()
        {
            DialogueManager.OnDialogueEnd += HandleEndDialogue;
        }

        private void OnDisable()
        {
            DialogueManager.OnDialogueEnd -= HandleEndDialogue;
        }

        private void HandleEndDialogue()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}