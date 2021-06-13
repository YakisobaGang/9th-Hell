using System.Collections.Generic;
using UnityEngine;

namespace ProjectD.Npc
{
#if UNITY_EDITOR
    public class ResetDialogueCounter : ScriptableObject
    {
        [SerializeField] private List<DialogueCounter> counters = new List<DialogueCounter>();
        [SerializeField] private bool resetAll;
        
        [ContextMenu("Reset All")]
        private void ResetAll()
        {
            counters.ForEach(counter => counter.Reset());
        }

        private void OnValidate()
        {
            if (resetAll)
            {
                Debug.Log("Counter reset");
                ResetAll();
                resetAll = false;
            }
        }
    }
#endif
}