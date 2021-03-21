using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.Combat.UI
{
    public class UnitHud : MonoBehaviour
    {
        [SerializeField] private Image healBar;
        private Unit unit;

        private void Awake() =>
            unit = GetComponent<Unit>();

        private void OnEnable() =>
            unit.OnHealthChange += UpdateHealBar;

        private void OnDisable() =>
            unit.OnHealthChange -= UpdateHealBar;

        private void UpdateHealBar(int newHealValue)
        {
            healBar.fillAmount = (float)newHealValue / healBar.fillAmount;
        }
    }
}