using System;
using System.Collections.Generic;
using ProjectD.Dialogue.UI;
using UnityEngine;

namespace ProjectD.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialoguePanel dialoguePanel;
        private Queue<string> sentences;
        private bool hasStarted = false;

        private void Awake()
        {
            sentences = new Queue<string>();
        }

        public static event Action OnDialogueStart;
        public static event Action OnDialogueEnd;

        public void StartDialogue(Dialogue dialogue)
        {
            if (hasStarted)
            {
                DisplayNexSentences();
                return;
            }

            OnDialogueStart?.Invoke();

            hasStarted = true;
            
            dialoguePanel.gameObject.SetActive(true);
            dialoguePanel.npcNameText.SetText(dialogue.NameNPC());

            sentences.Clear();

            foreach (var sentence in dialogue.Sentences()) sentences.Enqueue(sentence);

            DisplayNexSentences();
        }

        public void DisplayNexSentences()
        {
            if (sentences.Count == 0)
            {
                hasStarted = false;
                EndDialogue();
                return;
            }

            var sentence = sentences.Dequeue();
            dialoguePanel.dialogueText.SetText(sentence);
        }

        private void EndDialogue()
        {
            OnDialogueEnd?.Invoke();
            dialoguePanel.gameObject.SetActive(false);
        }
    }
}