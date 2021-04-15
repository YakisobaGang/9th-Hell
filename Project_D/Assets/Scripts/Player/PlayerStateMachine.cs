using System;
using ProjectD.StateMachine;
using UnityEngine;

namespace ProjectD.Player
{
    public class PlayerStateMachine : FiniteStateMachine
    {
        public static State CurrentPlayerState;
        public static event Action<State> OnChangeState;

        public override void SetState(State newState)
        {
            if(gameObject.activeInHierarchy == false)
                return;
            
            try
            {
                base.SetState(newState);
                CurrentPlayerState = newState;
                OnChangeState?.Invoke(_state);
            }
            catch (MissingReferenceException err)
            {
                print(err);
                return;
            }
        }
    }
}