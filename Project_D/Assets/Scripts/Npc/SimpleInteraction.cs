using System;
using ProjectD.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Npc
{
    public class SimpleInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent firstTime;
        [SerializeField] private UnityEvent secondTime;
        [SerializeField] private UnityEvent thirdTime;
        [SerializeField] private DialogueCounter dialogueCounter;
        private bool interactionIsPress;

        private void OnEnable()
        {
            PlayerInteraction.OnPlayerPressInteraction += HandleInteractionPress;
        }

        private void OnDisable()
        {
            PlayerInteraction.OnPlayerPressInteraction -= HandleInteractionPress;
        }

        private void Update()
        {
            //print(interactionIsPress);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!Input.GetKeyDown(KeyCode.E) && interactionIsPress == false)
                return;

            interactionIsPress = false;
            if (dialogueCounter.GetCurrentCount() == 0)
            {
                print("1");
                firstTime?.Invoke();
                dialogueCounter.IncreaseCount();
                return;
            }

            if (dialogueCounter.GetCurrentCount() == 1)
            {
                print("2");
                secondTime?.Invoke();
                dialogueCounter.IncreaseCount();
                return;
            }

            if (dialogueCounter.GetCurrentCount() == 2)
            {
                print("3");
                thirdTime?.Invoke();
                dialogueCounter.IncreaseCount();
            }
        }

        private void HandleInteractionPress()
        {
            interactionIsPress = true;
        }
    }
}