using System.Collections;
using ProjectD.Combat;

namespace ProjectD.StateMachine.States.Combat_States.End_States
{
    public class Won : State
    {
        public Won(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            // TODO: lot do final da batalha
            
            yield break;
        }
    }
}