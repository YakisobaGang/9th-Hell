using System;
using ProjectD.Commands;
using ProjectD.Enemys;
using ProjectD.ScriptableObjects;
using ProjectD.StateMachine.States.Combat_States;
using ProjectD.StateMachine.States.Combat_States.End_States;
using UnityEngine;

namespace ProjectD.Combat
{
    public class BattleSystem : StateMachine.StateMachine
    {
        [SerializeField] private CommandHandler commandHandler;
        [SerializeField] private Fighter player;
        [SerializeField] private DumbEnemy[] enemys;
        [SerializeField] public GameEvent onPlayerLoss;
        [SerializeField] public GameEvent onPlayerWin;
        [SerializeField] private int playerTurnCount = 3;

        [HideInInspector] public int playerAbilityIndex = 0;
        public int target { get; private set; }

        public int DeadEnemys { get; set; }
        public int CurrentEnemyIndex { get; private set; }
        public Fighter Player => player;
        public DumbEnemy Enemy(int index) => enemys[index];
        public bool IsTheLastEnemyTurn => CurrentEnemyIndex == enemys.Length - 1;
        public int PlayerTurnCount
        {
            get => playerTurnCount;
            private set => playerTurnCount = value;
        }
        public void PassToNextEnemy()
        {
            if (CurrentEnemyIndex.Equals(enemys.Length - 1))
            {
                CurrentEnemyIndex = 0;
            }
            else
            {
                CurrentEnemyIndex++;
            }
        }

        private void ChangeToEnemyTurn()
        {
            SetState(new EnemyTurn(this));
        }

        private void LateUpdate()
        {
            print(State.ToString());
            
            CheckIsPlayerAlive();

            CheckIfHasAnyEnemyLeft();

            CheckPlayerHasAnyActionsLeft();
            
            
            void CheckPlayerHasAnyActionsLeft()
            {
                if (playerTurnCount <= 0)
                {
                    commandHandler.DoCommands();
                    playerTurnCount = 3;
                    Invoke(nameof(ChangeToEnemyTurn), 1.5f);
                }
            }

            void CheckIfHasAnyEnemyLeft()
            {
                if (enemys.Length.Equals(DeadEnemys))
                {
                    SetState(new Won(this));
                }
            }

            void CheckIsPlayerAlive()
            {
                if (player.CurrentHealth <= 0)
                {
                    SetState(new Loss(this));
                }
            }
        }

        private void Start()
        {
            SetState(new Begin(this));
        }

        public void OnAttackButton(int index)
        {
            playerAbilityIndex = index;
            var temp = new CommandSander(SetStateToPlayerAttack, commandHandler);
            playerTurnCount--;
        }

        public void OnHealButton(int index)
        {
            playerAbilityIndex = index;
            var temp = new CommandSander(SetStateToPlayerHeal, commandHandler);
            playerTurnCount--;
        }

        public void OnSelectTarget(int index)
        {
            target = index;
        }


        private void SetStateToPlayerAttack()
        {
            StartCoroutine(State.Attack());
        }

        private void SetStateToPlayerHeal()
        {
            StartCoroutine(State.Heal());
        }
    }
}