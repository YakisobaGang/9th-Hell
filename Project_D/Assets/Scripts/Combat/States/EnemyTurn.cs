using System.Collections;
using System.Collections.Generic;
using ProjectD.Player.Combat;
using UnityEngine;

namespace ProjectD.Combat.States
{
    public class EnemyTurn : BattleState
    {
        private readonly PlayerUnit playerInfo = BattleManager.playerInstance;
        private readonly List<(GameObject gameObj, Unit unit)> enemysInstance = BattleManager.enemysInstance;
        private readonly int currentEnemyIndex = BattleManager.currentEnemyIndex;
        public EnemyTurn(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            BattleManager.VfxCountReset();
            yield return new WaitForSeconds(0.7f);

            var isDead =
                playerInfo.health.TakeDamage(enemysInstance[currentEnemyIndex].unit.BaseDamage);

            if (isDead)
            {
                BattleManager.combatState.SetState(new Loss(BattleManager));
                yield break;
            }

            if (currentEnemyIndex == enemysInstance.Count - 1)
            {
                BattleManager.combatState.SetState(new PlayerTurn(BattleManager));
                BattleManager.currentEnemyIndex = 0;
                yield break;
            }


            yield return BattleManager.PassToNextEnemy();

            if (enemysInstance[currentEnemyIndex].gameObj == null)
            {
                BattleManager.combatState.SetState(new PlayerTurn(BattleManager));
                yield break;
            }

            BattleManager.combatState.SetState(new EnemyTurn(BattleManager));
        }
    }
}