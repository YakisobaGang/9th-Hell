using UnityEngine;

namespace ProjectD.StateMachine
{
    public class FiniteStateMachine : MonoBehaviour
    {
        protected State _state;

        public virtual void SetState(State newState)
        {
            _state = newState;
            StartCoroutine(_state.Start());
        }

        public State CurrentState()
        {
            return _state;
        }
    }
}