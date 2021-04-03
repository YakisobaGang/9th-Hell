using System.Collections;
using UnityEngine;

namespace ProjectD.Combat.States
{
    public class EnemyTurn : BattleState
    {
        public EnemyTurn(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(0.7f);

            var isDead =
                BattleManager.playerInstance.health.TakeDamage(BattleManager
                    .enemysInstance[BattleManager.currentEnemyIndex].unit.BaseDamage);

            if (isDead)
            {
                BattleManager.combatState.SetState(new Loss());
                yield break;
            }

            if (BattleManager.currentEnemyIndex == BattleManager.enemysInstance.Count - 1)
            {
                BattleManager.combatState.SetState(new PlayerTurn(BattleManager));
                BattleManager.currentEnemyIndex = 0;
                yield break;
            }

            yield return BattleManager.PassToNextEnemy();
            BattleManager.combatState.SetState(new EnemyTurn(BattleManager));
        }
    }
}