using System.Collections;
using ProjectD.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Combat.States
{
    public class Won : BattleState
    {
        public Won(BattleManager battleManager) : base(battleManager)
        {
        }
        public override IEnumerator Start()
        {
            BattleManager.onWon?.Invoke();
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene(1);
        }

    }
}