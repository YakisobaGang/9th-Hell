using System;
using System.Collections;
using System.Collections.Generic;
using ProjectD.Commands;
using UnityEngine;

namespace ProjectD.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private CommandHandler commandHandler;
        [Header("Steup Battle")] [SerializeField]
        private GameObject playerPrefab;

        [SerializeField] private GameObject[] enemysPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform[] enemysSpawnPoint;
        
        private int playerTurnCount = 3;
        private Unit playerInstance;
        
        public CombatState combatState { get; private set; }
        public List<(GameObject gameObj ,Unit unit)> enemysInstance { get; private set; }

        private void Awake()
        {
            enemysInstance = new List<(GameObject, Unit)>();
        }

        private void Start()
        {
            StartCoroutine(SetupBattle());
        }

        private CombatState SetState(CombatState state)
        {
            return combatState = state;
        }

        private IEnumerator SetupBattle()
        {
            SetState(CombatState.Processing);

            var playerGO = Instantiate(playerPrefab, playerSpawnPoint);
            playerInstance = playerGO.GetComponent<Unit>();

            for (var i = 0; i < enemysPrefab.Length; i++)
            {
                var enemyGO = Instantiate(enemysPrefab[i], enemysSpawnPoint[i]);
                enemysInstance.Add((enemyGO, enemyGO.GetComponent<Unit>()));
            }
            
            yield return new WaitForSeconds(0.5f);
            
            SetState(CombatState.PlayerTurn);
            PlayerTurn();
        }

        private IEnumerator PlayerAction()
        {
            PlayerQueueActions();
            yield return new WaitForSeconds(1f);
        }

        private void PlayerQueueActions()
        {
            new CommandSander(() => playerInstance.UsingAbility(), commandHandler);
            playerTurnCount--;

            if (playerTurnCount == 0)
            {
                commandHandler.DoCommands();
                SetState(CombatState.EnemyTurn);
                EnemyTurn();
                return;
            }

            SetState(CombatState.PlayerTurn);
            PlayerTurn();
        }

        private void EnemyTurn()
        {
            throw new NotImplementedException();
        }

        private void PlayerTurn()
        {
            if (combatState != CombatState.PlayerTurn)
                return;
            SetState(CombatState.SelectingTarget);
        }

        #region UI fuctions

        public void OnSelectedTargetButton(GameObject target)
        {
            if (combatState != CombatState.SelectingTarget)
                return;

            playerInstance.SetTarget(target);

            SetState(CombatState.SelectingAbility);
        }
        
        public void OnAbilityButton(int index)
        {
            if (combatState != CombatState.SelectingAbility)
                return;

            playerInstance.SetChooseAbility(index);

            StartCoroutine(PlayerAction());
        }

        #endregion
    }
}