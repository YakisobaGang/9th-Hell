using ProjectD.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Npc
{
    public class SimpleInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent unityEvent;
        private bool interactionIsPress;

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
            if (other.CompareTag("Player") && interactionIsPress)
            {
                interactionIsPress = false;
                unityEvent?.Invoke();
            }
        }

        private void HandleInteractionPress()
        {
            interactionIsPress = !interactionIsPress;
        }
    }
}