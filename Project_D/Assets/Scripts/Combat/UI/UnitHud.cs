using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.Combat.UI
{
    public class UnitHud : MonoBehaviour
    {
        [SerializeField] private Image healBar;
        [SerializeField] private TMP_Text unitDisplayText;
        [SerializeField] private Unit unit;

        private void Awake()
        {
            unit = GetComponent<Unit>();
        }

        private void Start()
        {
            if (unitDisplayText is null) return;
            unitDisplayText.SetText(unit.GetUnitName);
        }

        private void OnEnable()
        {
            unit.OnHealthChange += UpdateHealBar;
        }

        private void OnDisable()
        {
            unit.OnHealthChange -= UpdateHealBar;
        }

        private void UpdateHealBar(float newHealValue)
        {
            healBar.fillAmount = newHealValue;
        }
    }
}