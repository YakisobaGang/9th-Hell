using ProjectD.ScriptableObjects;
using ProjectD.StateMachine.States.Combat_States;
using UnityEngine;

namespace ProjectD.Combat
{
    public class BattleSystem : StateMachine.StateMachine
    {
        [SerializeField] private Fighter player;
        [SerializeField] private Fighter enemy;

        public Fighter Player => player;
        public Fighter Enemy => enemy;

        private void Start()
        {
            SetState(new Begin(this));
        }

        public void OnAttackButton()
        {
            StartCoroutine(State.Attack());
        }

        public void OnHealButton()
        {
            StartCoroutine(State.Heal());
        }
    }
}