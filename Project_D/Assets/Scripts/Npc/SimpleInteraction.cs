using System;
using ProjectD.Dialogue;
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
        [SerializeField] private float range = 3f;
        [SerializeField] private LayerMask playerLayerMask;
        public Collider[] result;
        private FinalDialogueTrigger finalDialogueTrigger;

        private void Start()
        {
            TryGetComponent<FinalDialogueTrigger>(out finalDialogueTrigger);
        }

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
            
            result = Physics.OverlapSphere(transform.position, range, playerLayerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void HandleInteractionPress()
        {

            if (result.Length == 0)
                return;
            
            switch (dialogueCounter.GetCurrentCount())
            {
                case 0:
                    firstTime?.Invoke();
                    dialogueCounter.IncreaseCount();
                    break;
                case 1:
                    secondTime?.Invoke();
                    dialogueCounter.IncreaseCount();
                    break;
                case 2:
                    if(finalDialogueTrigger is null)
                        break;
                    thirdTime?.Invoke();
                    dialogueCounter.IncreaseCount();
                    break;
                default:
                    return;
            }
        }
    }
}