using System;
using ProjectD.Combat;
using ProjectD.Player.Combat_States;
using UnityEngine;

namespace ProjectD.Player.Combat
{
    public class PlayerUnit : Unit
    {
        public PlayerStateMachine stateMachine { get; private set; }

        private void Awake()
        {
            gameObject.AddComponent<PlayerStateMachine>();
            stateMachine = GetComponent<PlayerStateMachine>();

            stateMachine.SetState(new Idle());
        }


        private void Update()
        {
            Debug.Log($"<Color=green>{stateMachine.CurrentState()}</color>");
        }
    }
}