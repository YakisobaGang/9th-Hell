using System.Collections;
using System.Collections.Generic;
using ProjectD.Interfaces;
using UnityEngine;
using UnityEngine.Timeline;

namespace ProjectD.Commands
{
    [ExecuteInEditMode]
    public class CommandHandler : MonoBehaviour
    {
        private Queue<ICommand> commandsList;
        private WaitForSeconds waitForSeconds;
        [SerializeField] private SignalReceiver vfxEnd;

        public static CommandHandler Instance { get; private set; }

        private void Awake()
        {
            if (!(Instance is null) && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            waitForSeconds = new WaitForSeconds(5f);
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
                yield return new WaitUntil(() =>
                {
                    temp.Execute();
                    
                    return vfxEnd.GetSignalAssetAtIndex(0);
                });
                
                yield return waitForSeconds;
            }
        }
    }
}