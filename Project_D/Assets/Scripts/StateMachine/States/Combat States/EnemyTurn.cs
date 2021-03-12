using System.Collections;
using ProjectD.Combat;
using ProjectD.StateMachine.States.Combat_States.End_States;
using UnityEngine;

namespace ProjectD.StateMachine.States.Combat_States
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            var isDead = BattleSystem.Player.Damage(BattleSystem.Enemy.Attack);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                BattleSystem.SetState(new Loss(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }
    }
}