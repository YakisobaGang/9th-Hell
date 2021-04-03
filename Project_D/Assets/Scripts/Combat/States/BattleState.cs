using ProjectD.StateMachine;

namespace ProjectD.Combat.States
{
    public abstract class BattleState : State
    {
        protected BattleManager BattleManager;

        public BattleState(BattleManager battleManager)
        {
            BattleManager = battleManager;
        }
    }
}