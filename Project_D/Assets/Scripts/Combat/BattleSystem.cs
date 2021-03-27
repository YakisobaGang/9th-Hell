﻿using System;
using System.Collections;
using System.Collections.Generic;
using ProjectD.Commands;
using UnityEngine;

namespace ProjectD.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private CommandHandler commandHandler;

        [Header("Steup Battle")]
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField] private GameObject[] enemysPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform[] enemysSpawnPoint;
        private int currentEnemyIndex;

        private int playerTurnCount = 3;
        public event Action<int> OnPlayerTurnCountChange;
        public Unit playerInstance { get; private set; }
        public CombatState combatState { get; private set; }
        public List<(GameObject gameObj, Unit unit)> enemysInstance { get; private set; }

        private void Awake()
        {
            enemysInstance = new List<(GameObject, Unit)>();
        }

        private void Start()
        {
            StartCoroutine(SetupBattle());
        }

        private void Update()
        {
            print(combatState.ToString());
        }

        private void SetState(CombatState state)
        {
            combatState = state;
        }

        private IEnumerator SetupBattle()
        {
            SetState(CombatState.Processing);
            OnPlayerTurnCountChange?.Invoke(playerTurnCount);

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

        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(0.7f);
            playerTurnCount = 3;
            OnPlayerTurnCountChange?.Invoke(playerTurnCount);

            var isDead = playerInstance.TakeDamage(enemysInstance[currentEnemyIndex].unit.BaseDamage);
            if (isDead)
            {
                SetState(CombatState.Loss);
                StopAllCoroutines();
                yield break;
            }

            if (currentEnemyIndex == enemysInstance.Count - 1)
            {
                SetState(CombatState.PlayerTurn);
                currentEnemyIndex = 0;
                PlayerTurn();
                yield break;
            }

            StartCoroutine(PassToNextEnemy());
            SetState(CombatState.EnemyTurn);
            yield return EnemyTurn();
        }

        private IEnumerator PassToNextEnemy()
        {
            yield return new WaitForSeconds(0.5f);

            currentEnemyIndex++;
        }

        #region Player

        private IEnumerator PlayerAction()
        {
            PlayerQueueActions();
            yield return new WaitForSeconds(1f);
        }

        private void PlayerQueueActions()
        {
            new CommandSander(() => playerInstance.UsingAbility(), commandHandler);
            playerTurnCount--;
            OnPlayerTurnCountChange?.Invoke(playerTurnCount);

            if (playerTurnCount == 0)
            {
                commandHandler.DoCommands();
                SetState(CombatState.EnemyTurn);
                StartCoroutine(EnemyTurn());
                return;
            }

            SetState(CombatState.PlayerTurn);
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            if (combatState != CombatState.PlayerTurn)
                return;
            SetState(CombatState.SelectingAbility);
        }

        #endregion

        #region UI fuctions

        public void OnSelectedTargetButton(GameObject target)
        {
            if (combatState != CombatState.SelectingTarget)
                return;

            playerInstance.AddTarget(target);

            SetState(CombatState.SelectingAbility);
            StartCoroutine(PlayerAction());
        }

        public void OnAbilityButton(int index)
        {
            if (combatState != CombatState.SelectingAbility)
                return;
            playerInstance.SetChooseAbility(index);

            SetState(CombatState.SelectingTarget);
        }

        #endregion
    }
}