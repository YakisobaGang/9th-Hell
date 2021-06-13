using System;
using UnityEngine;

namespace ProjectD.Npc
{
    [CreateAssetMenu(fileName = "Dialogue Counter", menuName = "Dialogue counter", order = 4)]
    public class DialogueCounter : ScriptableObject
    {
        [SerializeField] private int count = 0;

        public void IncreaseCount() => count++;

        public int GetCurrentCount() => count;

        public void Reset()
        {
            count = 0;
        }
    }
}