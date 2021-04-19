using System.Linq;
using FabriciohodDev.DialogueSystem.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectD.DialogueSystem
{
    public class DialogueParser : MonoBehaviour
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private Button choicePrefab;
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private UnityEvent onDialogueStart;
        [SerializeField] private UnityEvent onDialogueEnd;
        private DialogueContainer dialogue;

        public void DialogueTrigger(DialogueContainer _dialogue)
        {
            onDialogueStart?.Invoke();
            dialogue = _dialogue;
            var narrativeData = dialogue.nodeLinks[0]; //Entrypoint node
            ProceedToNarrative(narrativeData.targetNodeGuid);
        }

        private void ProceedToNarrative(string narrativeDataGuid)
        {
            var text = dialogue.dialogueNodeData.Find(x => x.guid == narrativeDataGuid).dialogueText;
            var choices = dialogue.nodeLinks.Where(x => x.baseNodeGuid == narrativeDataGuid);
            bool HasEnded() => choices.ToArray().Length == 0;
            dialogueText.SetText(ProcessProperties(text));
            
            if (HasEnded())
            {
                onDialogueEnd?.Invoke();
            }
            
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            foreach (var choice in choices)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<TMP_Text>().SetText(ProcessProperties(choice.portName));
                button.onClick.AddListener(() => ProceedToNarrative(choice.targetNodeGuid));
            }
        }
        

        private string ProcessProperties(string text)
        {
            foreach (var exposedProperty in dialogue.exposedProperties)
            {
                text = text.Replace($"[{exposedProperty.propertyName}]", exposedProperty.propertyValue);
            }
            return text;
        }
    }
}