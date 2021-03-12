using System.Collections;
using ProjectD.Combat;
using UnityEngine;

namespace ProjectD.StateMachine.States.Combat_States
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}