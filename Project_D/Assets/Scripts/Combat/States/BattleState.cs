using ProjectD.StateMachine;

namespace ProjectD.Combat.States
{
    public abstract class BattleState : State
    {
        protected static BattleManager BattleManager;

        public BattleState(BattleManager battleManager)
        {
            BattleManager = battleManager;
        }
    }
}