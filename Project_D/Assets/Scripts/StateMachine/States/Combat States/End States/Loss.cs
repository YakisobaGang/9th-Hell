using System.Collections;
using ProjectD.Combat;

namespace ProjectD.StateMachine.States.Combat_States.End_States
{
    public class Loss : State
    {
        public Loss(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            BattleSystem.onPlayerLoss.Raise();
            yield break;
        }
    }
}