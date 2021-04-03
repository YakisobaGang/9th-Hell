using System.Collections;
using ProjectD.Combat;
using ProjectD.Combat.States;
using ProjectD.Player.Combat;
using UnityEngine;

namespace ProjectD.Player.Combat_States
{
    public class SelectingTarget : BattleState
    {
        private readonly PlayerUnit player;
        private GameObject target;

        public SelectingTarget(BattleManager battleManager) : base(battleManager)
        {
            player = BattleManager.playerInstance;
            BattleManager.OnSelectTarget += HandleSelectTarget;
        }

        private void HandleSelectTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        public override IEnumerator Start()
        {
            yield return new WaitUntil(() => !(target is null));
            
            player.AddTarget(target);
            player.stateMachine.SetState(new QueueActions(BattleManager));

            yield return null;
            BattleManager.OnSelectTarget -= HandleSelectTarget;
        }
    }
}