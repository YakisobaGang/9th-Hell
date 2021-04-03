using System.Collections;

namespace ProjectD.Combat.States
{
    public class Starting : BattleState
    {
        public Starting(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            yield break;
        }
    }
}