using System.Collections;
using ProjectD.Combat;
using UnityEngine;

namespace ProjectD.StateMachine.States.Combat_States
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            Debug.Log("Current enemy playing " + BattleSystem.CurrentEnemyIndex);
            BattleSystem.Player.Damage(BattleSystem.Enemy(BattleSystem.CurrentEnemyIndex).Attack);

            yield return new WaitForSeconds(1f);

            if (BattleSystem.IsTheLastEnemyTurn)
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
            else
            {
                BattleSystem.PassToNextEnemy();
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
        }
    }
}