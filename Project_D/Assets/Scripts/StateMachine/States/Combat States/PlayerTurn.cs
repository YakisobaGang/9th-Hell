using System.Collections;
using ProjectD.Combat;
using ProjectD.StateMachine.States.Combat_States.End_States;
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
            var isDead = BattleSystem.Enemy.Damage(BattleSystem.Player.Attack);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
        }

        public override IEnumerator Heal()
        {
            BattleSystem.Player.Heal(5);

            yield return new WaitForSeconds(1f);
            
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}