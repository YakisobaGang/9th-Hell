using ProjectD.Commands;
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
        [SerializeField] private Fighter[] enemys;
        [SerializeField] public GameEvent onPlayerLoss;
        [SerializeField] public GameEvent onPlayerWin;
        [SerializeField] private int playerTurnCount = 3;

        public int DeadEnemys { get; set; }
        public int CurrentEnemyIndex { get; private set; }
        public Fighter Player => player;
        public Fighter Enemy (int index) => enemys[index];
        public bool IsTheLastEnemyTurn => CurrentEnemyIndex == enemys.Length - 1;
        public int PlayerTurnCount
        {
            get => playerTurnCount;
            set => playerTurnCount = value;
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
            CheckIsPlayerAlive();

            CheckIfHasAnyEnemyLeft();

            CheckPlayerHasAnyActionsLeft();
        }

        private void Start()
        {
            SetState(new Begin(this));
        }
        
        [ContextMenu("attack")]
        public void OnAttackButton()
        { 
           var temp = new CommandSander(SetStateToPlayerAttack, commandHandler);
            playerTurnCount--;
        }

        [ContextMenu("heal")]
        public void OnHealButton()
        {
            var temp = new CommandSander(SetStateToPlayerHeal, commandHandler);
            playerTurnCount--;
        }
        
        private void CheckPlayerHasAnyActionsLeft()
        {
            if (playerTurnCount <= 0)
            {
                commandHandler.DoCommands();
                playerTurnCount = 3;
                Invoke(nameof(ChangeToEnemyTurn), 1.5f);
            }
        }

        private void CheckIfHasAnyEnemyLeft()
        {
            if (enemys.Length.Equals(DeadEnemys))
            {
                SetState(new Won(this));
            }
        }

        private void CheckIsPlayerAlive()
        {
            if (player.Healing <= 0)
            {
                SetState(new Loss(this));
            }
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