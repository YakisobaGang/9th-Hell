using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.Combat.UI
{
    public class BattleSystemUI : MonoBehaviour
    {
        [SerializeField] private BattleSystem battleSystem;
        [SerializeField] private GameObject chooseEnemyPanel;
        [SerializeField] private Button[] chooseEnemyButtons;
        [SerializeField] private GameObject chooseAbilityPanel;

        private void Update()
        {
            switch (battleSystem.combatState)
            {
                case CombatState.SelectingTarget:
                    chooseEnemyPanel.SetActive(true);
                    chooseAbilityPanel.SetActive(false);

                    for (int i = 0; i < battleSystem.enemysInstance.Count; i++)
                    {
                        chooseEnemyButtons[i].gameObject.SetActive(true);
                        chooseEnemyButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                            battleSystem.enemysInstance[i].unit.GetUnitName;
                        
                        var obj = battleSystem.enemysInstance[i].gameObj;
                        
                        chooseEnemyButtons[i].onClick.AddListener(() =>
                        {
                            battleSystem.OnSelectedTargetButton(obj);
                        });
                    }
                    break;
                case CombatState.SelectingAbility:
                    chooseAbilityPanel.SetActive(true);
                    chooseEnemyPanel.SetActive(false);
                    break;
                default:
                    chooseAbilityPanel.SetActive(false);
                    chooseEnemyPanel.SetActive(false);
                    break;
            }
        }
    }
}