using System;
using ProjectD.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.UI
{
    public class UpdateHealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Fighter characterData;

        private void Update()
        {
            UpdateHealthDisplay();
        }

        public void UpdateHealthDisplay()
        {
            healthBar.fillAmount = (float) characterData.CurrentHealth / characterData.TotalHealth;
        }
    }
}
