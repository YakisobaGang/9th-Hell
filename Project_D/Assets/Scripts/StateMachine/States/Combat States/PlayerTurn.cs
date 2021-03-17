using System.Collections;
using ProjectD.Combat;
using ProjectD.ScriptableObjects;
using UnityEngine;

namespace ProjectD.StateMachine.States.Combat_States
{
    public class PlayerTurn : State
    {
        private bool enemyIsDead;
        Fighter PlayerData => BattleSystem.Player;
        int AbilityIndex => BattleSystem.playerAbilityIndex;
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem) { }

        public override IEnumerator Start()
        {
            yield break;
        }

        public override IEnumerator Attack()
        {
            Debug.Log(BattleSystem.target.ToString());
            CastAbility(BattleSystem.target);
            
            yield return new WaitForSeconds(1f);


            if (BattleSystem.PlayerTurnCount >= 3)
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }

        private void CastAbility(int targetIndex)
        {
            enemyIsDead = PlayerData.UsingAbility(AbilityIndex, BattleSystem.Enemy(targetIndex).EnemyData);
            
            if (enemyIsDead)
            {
                BattleSystem.DeadEnemys++;
            }
        }

        public override IEnumerator Heal()
        {
            var temp = BattleSystem.Player.GetAbility(AbilityIndex);
            BattleSystem.Player.Heal(temp.GetDamageValue);

            yield return new WaitForSeconds(1f);

            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}