using System;
using System.Collections;
using System.Collections.Generic;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Commands
{
    [ExecuteInEditMode]
    public class CommandHandler : MonoBehaviour
    {
        [SerializeField] private readonly List<ICommand> commandsList = new List<ICommand>();

        private WaitForSeconds waitForSeconds;            

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(0.5f);
        }

        public void AddCommand(ICommand command)
        {
            commandsList.Add(command);
        }

        public void DoCommands()
        {
            StartCoroutine(nameof(DoCommandsOverTime));
        }

        private IEnumerator DoCommandsOverTime()
        {
            foreach (var command in commandsList) 
            {
                command.Execute();

                yield return waitForSeconds;
            }
        }
    }
}