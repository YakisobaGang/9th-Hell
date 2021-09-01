using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD.Npc
{
    public class ResetDialogueCounter : ScriptableObject
    {
        [SerializeField] private List<DialogueCounter> counters = new List<DialogueCounter>();
        [SerializeField] private bool resetAll;

        private void Awake()
        {
            ResetAll();
        }

        [ContextMenu("Reset All")]
        public void ResetAll()
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
}