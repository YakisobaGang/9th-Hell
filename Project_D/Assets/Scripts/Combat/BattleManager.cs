using System;
using System.Collections;
using System.Collections.Generic;
using ProjectD.Combat.States;
using ProjectD.Commands;
using ProjectD.Player.Combat;
using ProjectD.StateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD.Combat
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private CommandHandler commandHandler;

        [Header("Steup Battle")]
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField] private GameObject[] enemysPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform[] enemysSpawnPoint;
        [SerializeField] public UnityEvent onWon;
        [SerializeField] public UnityEvent onLos;

        internal int currentEnemyIndex;
        private int playerTurnCount = 2;
        public PlayerUnit playerInstance { get; private set; }
        public FiniteStateMachine combatState { get; private set; }
        public List<(GameObject gameObj, Unit unit)> enemysInstance { get; private set; }

        private void Awake()
        {
            gameObject.AddComponent<FiniteStateMachine>();
            combatState = GetComponent<FiniteStateMachine>();
            enemysInstance = new List<(GameObject, Unit)>();
        }

        private void Start()
        {
            combatState.SetState(new Starting(this));
            StartCoroutine(nameof(SetupBattle));
        }

        private void Update()
        {
            Debug.Log($"<Color=green>{combatState.CurrentState()}</color>");

            if (enemysInstance.TrueForAll(enemy => enemy.gameObj.activeInHierarchy == false))
            {
                combatState.SetState(new Won(this));
            }

            if (playerInstance.gameObject.activeInHierarchy == false)
            {
                combatState.SetState(new Loss(this));
            }
        }

        public event Action<GameObject> OnSelectTarget;
        public event Action<int> OnSelectAbility;
        public event Action<int> OnPlayerTurnCountChange;


        private IEnumerator SetupBattle()
        {
            OnPlayerTurnCountChange?.Invoke(playerTurnCount);

            var playerGO = Instantiate(playerPrefab, playerSpawnPoint);
            playerInstance = playerGO.GetComponent<PlayerUnit>();

            for (var i = 0; i < enemysPrefab.Length; i++)
            {
                var enemyGO = Instantiate(enemysPrefab[i], enemysSpawnPoint[i]);
                enemysInstance.Add((enemyGO, enemyGO.GetComponent<Unit>()));
            }

            yield return new WaitForSeconds(0.5f);

            combatState.SetState(new PlayerTurn(this));
        }

        public IEnumerator PassToNextEnemy()
        {
            yield return new WaitForSeconds(0.5f);

            currentEnemyIndex++;
        }

        public void ResetEnemyTurnIndex()
        {
            currentEnemyIndex = 0;
        }

        public bool HasPlayerTurnEnd()
        {
            if (playerTurnCount == 0)
            {
                playerTurnCount = 2;
                OnPlayerTurnCountChange?.Invoke(playerTurnCount);

                return true;
            }

            playerTurnCount--;
            OnPlayerTurnCountChange?.Invoke(playerTurnCount);

            return false;
        }

        [ContextMenu("Kill all Enemys")]
        public void KillAllEnemys()
        {
            enemysInstance.ForEach(enemy =>
            {
                enemy.unit.health.TakeDamage(999999);
            });
            combatState.SetState(new Won(this));
        }

        [ContextMenu("Kill Player")]
        public void KillPlayer()
        {
            playerInstance.health.TakeDamage(9999999);
            combatState.SetState(new Loss(this));
        }

        #region UI fuctions

        public void OnSelectedTargetButton(GameObject target)
        {
            OnSelectTarget?.Invoke(target);
        }

        public void OnAbilityButton(int index)
        {
            OnSelectAbility?.Invoke(index);
        }

        #endregion
    }
}