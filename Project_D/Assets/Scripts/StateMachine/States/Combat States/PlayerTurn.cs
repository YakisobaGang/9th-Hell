using System.Collections;
using ProjectD.Combat;
using UnityEngine;

namespace ProjectD.StateMachine.States.Combat_States
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            yield break;
        }

        public override IEnumerator Attack()
        {
            var enemyIsDead = BattleSystem.Player.UsingAbility(BattleSystem.playerAbilityIndex, BattleSystem.Enemy(0));

            yield return new WaitForSeconds(1f);

            if (enemyIsDead)
            {
                BattleSystem.DeadEnemys++;
            }

            if (BattleSystem.PlayerTurnCount >= 3)
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }

        public override IEnumerator Heal()
        {
            var temp = BattleSystem.Player.GetAbility(BattleSystem.playerAbilityIndex);
            BattleSystem.Player.Heal(temp.GetDamageValue);

            yield return new WaitForSeconds(1f);

            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}