using System.Collections;
using ProjectD.Combat;
using ProjectD.Combat.States;
using ProjectD.Commands;
using UnityEngine;
using UnityEngine.Timeline;

namespace ProjectD.Player.Combat_States
{
    public class DoActions : BattleState
    {
        public DoActions(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            CommandHandler.Instance.DoCommands();
            BattleManager.playerInstance.stateMachine.SetState(new Idle());


            yield return new WaitUntil(() => BattleManager.vfxCount == 0);
            BattleManager.combatState.SetState(new EnemyTurn(BattleManager));

        }
    }
}