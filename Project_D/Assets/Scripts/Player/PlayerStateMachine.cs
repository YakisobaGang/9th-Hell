using System;
using ProjectD.StateMachine;

namespace ProjectD.Player
{
    public class PlayerStateMachine : FiniteStateMachine
    {
        public static State CurrentPlayerState;
        public static event Action<State> OnChangeState;

        public override void SetState(State newState)
        {
            base.SetState(newState);
            CurrentPlayerState = newState;
            OnChangeState?.Invoke(_state);
        }
    }
}