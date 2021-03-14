using System.Collections;
using System.Collections.Generic;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Commands
{
    [ExecuteInEditMode]
    public class CommandHandler : MonoBehaviour
    {
        [SerializeField] private Queue<ICommand> commandsList;
        private WaitForSeconds waitForSeconds;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(0.5f);
            commandsList = new Queue<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            commandsList.Enqueue(command);
        }

        public void DoCommands()
        {
            StartCoroutine(nameof(DoCommandsOverTime));
        }

        private IEnumerator DoCommandsOverTime()
        {
            foreach (var command in commandsList.ToArray())
            {
                var temp = commandsList.Dequeue();
                yield return waitForSeconds;
                temp.Execute();
            }
        }
    }
}