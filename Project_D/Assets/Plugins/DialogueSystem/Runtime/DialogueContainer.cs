using System.Collections.Generic;
using UnityEngine;

namespace FabriciohodDev.DialogueSystem.Runtime
{
    [System.Serializable]
    public class DialogueContainer : ScriptableObject
    {
        public string NpcName = "null";
        public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
        public List<DialogueNodeData> dialogueNodeData = new List<DialogueNodeData>();
        public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();
    }
}