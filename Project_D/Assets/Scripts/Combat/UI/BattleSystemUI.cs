using System.Collections.Generic;
using ProjectD.Player;
using ProjectD.Player.Combat_States;
using ProjectD.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.Combat.UI
{
    public class BattleSystemUI : MonoBehaviour
    {
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private GameObject chooseEnemyPanel;
        [SerializeField] private List<Button> chooseEnemyButtons;
        [SerializeField] private List<Button> chooseAbilityButtons;
        [SerializeField] private GameObject chooseAbilityPanel;
        [SerializeField] private TMP_Text remainingActionsText;

        private void OnEnable()
        {
            battleManager.OnPlayerTurnCountChange += HandleRemainingActionsChange;
            PlayerStateMachine.OnChangeState += HandleChangeState;
        }

        private void OnDisable()
        {
            battleManager.OnPlayerTurnCountChange -= HandleRemainingActionsChange;
            PlayerStateMachine.OnChangeState -= HandleChangeState;
        }

        private void SelectingAbility()
        {
            chooseAbilityPanel.SetActive(true);
            chooseEnemyPanel.SetActive(false);

            var playerAbilitys = battleManager.playerInstance.GetAllAbilitys;

            for (var i = 0; i < playerAbilitys.Length; i++)
            {
                chooseAbilityButtons[i].gameObject.SetActive(true);
                chooseAbilityButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                    playerAbilitys[i].abilityName;
            }
        }

        private void SelectingTarget()
        {
            chooseEnemyPanel.SetActive(true);
            chooseAbilityPanel.SetActive(false);

            for (var i = 0; i < battleManager.enemysInstance.ToArray().Length; i++)
            {
                if (battleManager.enemysInstance[i].gameObj == null)
                {
                    chooseEnemyButtons[i].gameObject.SetActive(false);
                    continue;
                }
                chooseEnemyButtons[i].gameObject.SetActive(true);
                chooseEnemyButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                    battleManager.enemysInstance[i].unit.GetUnitName;

                var obj = battleManager.enemysInstance[i].gameObj;

                chooseEnemyButtons[i].onClick.AddListener(() => { battleManager.OnSelectedTargetButton(obj); });
            }
        }

        private void HandleChangeState(State state)
        {
            switch (state)
            {
                case SelectingAbility _:
                    SelectingAbility();
                    break;
                case SelectingTarget _:
                    SelectingTarget();
                    break;
                default:
                    chooseAbilityPanel.SetActive(false);
                    chooseEnemyPanel.SetActive(false);
                    break;
            }
        }

        private void HandleRemainingActionsChange(int actions)
        {
            var temp = actions + 1;
            remainingActionsText.SetText($"Remaining Actions: {temp.ToString()}/3");
        }
    }
}