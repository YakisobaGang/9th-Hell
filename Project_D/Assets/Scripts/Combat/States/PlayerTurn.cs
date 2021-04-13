using System.Collections;
using ProjectD.Player.Combat_States;

namespace ProjectD.Combat.States
{
    public class PlayerTurn : BattleState
    {
        public PlayerTurn(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            BattleManager.ResetEnemyTurnIndex();

            BattleManager.playerInstance.stateMachine.SetState(new SelectingAbility(BattleManager));
            
            yield break;
        }
    }
}