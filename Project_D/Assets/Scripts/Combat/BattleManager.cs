using System;
using System.Collections;
using System.Collections.Generic;
using ProjectD.Combat.States;
using ProjectD.Commands;
using ProjectD.Player.Combat;
using ProjectD.StateMachine;
using UnityEngine;

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

#if UNITY_EDITOR
        private void Update()
        {
            Debug.Log($"<Color=green>{combatState.CurrentState()}</color>");
        }
#endif

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