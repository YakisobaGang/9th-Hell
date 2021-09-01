using ProjectD.Npc;
using UnityEngine;

namespace ProjectD.Utils
{
    public class ResetDialogueCounter : MonoBehaviour
    {
        [SerializeField] private DialogueCounter[] dialogueCounters;
        void Start()
        {
            foreach (var dialogueCounter in dialogueCounters)
            {
                dialogueCounter.Reset();
            }
        }
    }
}
