using System;
using UnityEngine;

namespace ProjectD.Dialogue
{
    [Serializable]
    public class Dialogue
    {
        [SerializeField] private string nameNPC;
        [SerializeField] [TextArea(3, 10)] private string[] sentences;

        public string NameNPC()
        {
            return nameNPC;
        }

        public string[] Sentences()
        {
            return sentences;
        }
    }
}