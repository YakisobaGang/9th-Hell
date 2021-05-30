using System.Collections;
using ProjectD.Abilitys;
using ProjectD.Combat;
using ProjectD.Combat.States;
using ProjectD.Player.Combat;
using UnityEngine;

namespace ProjectD.Player.Combat_States
{
    public class SelectingAbility : BattleState
    {
        private readonly PlayerUnit player;
        private int? abilityIndex;

        public SelectingAbility(BattleManager battleManager) : base(battleManager)
        {
            player = BattleManager.playerInstance;
            BattleManager.OnSelectAbility += HandleSelectAbility;
            battleManager.onWon.AddListener(HandleEndBattle);
            battleManager.onLos.AddListener(HandleEndBattle);
        }

        private void HandleSelectAbility(int index)
        {
            abilityIndex = index;
        }

        private void HandleEndBattle()
        {
            player.stateMachine.SetState(new Idle());
        }

        public override IEnumerator Start()
        {
            yield return new WaitUntil(() => !(abilityIndex is null));

            if (abilityIndex != null && player.GetAllAbilitys[abilityIndex.Value].abilityType is AbilityTypes.Heal)
            {
                player.AddTarget(player.gameObject);
                player.SetChooseAbility(abilityIndex.Value);

                player.stateMachine.SetState(new QueueActions(BattleManager));
                yield break;
            }

            if (abilityIndex != null)
                player.SetChooseAbility(abilityIndex.Value);

            yield return new WaitForSeconds(0.2f);
            player.stateMachine.SetState(new SelectingTarget(BattleManager));

            BattleManager.OnSelectAbility -= HandleSelectAbility;
            BattleManager.onWon.RemoveListener(HandleEndBattle);
            BattleManager.onLos.RemoveListener(HandleEndBattle);
        }
    }
}