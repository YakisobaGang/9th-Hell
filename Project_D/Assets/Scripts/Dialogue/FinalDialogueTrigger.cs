using System.Collections.Generic;
using ProjectD.Npc;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Dialogue
{
    public class FinalDialogueTrigger : MonoBehaviour
    {
        [SerializeField] private List<DialogueCounter> counters = new List<DialogueCounter>();
        public UnityEvent finalDialogue;

        private int total;

        public bool CheckCondition()
        {
            counters.ForEach(counters => total += counters.GetCurrentCount());

            if (total >= 3)
            {
                finalDialogue?.Invoke();
                return true;
            }
            
            return false;
        }
    }
}