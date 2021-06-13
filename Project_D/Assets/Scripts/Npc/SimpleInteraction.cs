using ProjectD.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Npc
{
    public class SimpleInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent firstTime;
        [SerializeField] private UnityEvent secondTime;
        [SerializeField] private DialogueCounter dialogueCounter;
        private bool InteractionIsPress;

        private void OnEnable()
        {
            PlayerInteraction.OnPlayerPressInteraction += HandleInteractionPress;
        }

        private void OnDisable()
        {
            PlayerInteraction.OnPlayerPressInteraction -= HandleInteractionPress;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player") || !InteractionIsPress) return;
            
            InteractionIsPress = false;

            if (dialogueCounter.GetCurrentCount() == 0)
            {
                firstTime?.Invoke();
                dialogueCounter.IncreaseCount();
                return;
            }

            if (dialogueCounter.GetCurrentCount() >= 1)
            {
                secondTime?.Invoke();
                dialogueCounter.IncreaseCount();
            }
        }

        private void HandleInteractionPress()
        {
            InteractionIsPress = !InteractionIsPress;
        }
    }
}