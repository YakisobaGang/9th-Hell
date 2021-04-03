using System.Collections;
using ProjectD.Combat;
using ProjectD.Combat.States;
using ProjectD.Commands;
using ProjectD.Player.Combat;

namespace ProjectD.Player.Combat_States
{
    public class QueueActions : BattleState
    {
        private readonly PlayerUnit player;

        public QueueActions(BattleManager battleManager) : base(battleManager)
        {
            player = BattleManager.playerInstance;
        }

        public override IEnumerator Start()
        {
            new CommandSander(() => player.UsingAbility(), CommandHandler.Instance);

            if (!BattleManager.HasPlayerTurnEnd())
            {
                BattleManager.combatState.SetState(new PlayerTurn(BattleManager));
                yield break;
            }

            player.stateMachine.SetState(new DoActions(BattleManager));
            yield return null;
        }
    }
}