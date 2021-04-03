using System.Collections;

namespace ProjectD.StateMachine
{
    public abstract class State
    {
        public virtual IEnumerator Start()
        {
            yield break;
        }
    }
}