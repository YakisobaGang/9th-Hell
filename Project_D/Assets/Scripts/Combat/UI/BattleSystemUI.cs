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
        [SerializeField] private Button[] chooseAbilityButtons;
        [SerializeField] private GameObject chooseAbilityPanel;

        private void Update()
        {
            switch (battleSystem.combatState)
            {
                case CombatState.SelectingTarget:
                    chooseEnemyPanel.SetActive(true);
                    chooseAbilityPanel.SetActive(false);

                    for (var i = 0; i < battleSystem.enemysInstance.Count; i++)
                    {
                        chooseEnemyButtons[i].gameObject.SetActive(true);
                        chooseEnemyButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                            battleSystem.enemysInstance[i].unit.GetUnitName;

                        var obj = battleSystem.enemysInstance[i].gameObj;

                        chooseEnemyButtons[i].onClick.AddListener(() => { battleSystem.OnSelectedTargetButton(obj); });
                    }

                    break;
                case CombatState.SelectingAbility:
                    chooseAbilityPanel.SetActive(true);
                    chooseEnemyPanel.SetActive(false);

                    var playerAbilitys = battleSystem.playerInstance.GetAllAbilitys;

                    for (var i = 0; i < playerAbilitys.Length; i++)
                    {
                        chooseAbilityButtons[i].gameObject.SetActive(true);
                        chooseAbilityButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text =
                            playerAbilitys[i].abilityName;
                    }

                    break;
                default:
                    chooseAbilityPanel.SetActive(false);
                    chooseEnemyPanel.SetActive(false);
                    break;
            }
        }
    }
}