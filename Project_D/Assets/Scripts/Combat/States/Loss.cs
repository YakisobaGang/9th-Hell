using System.Collections;
using ProjectD.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectD.Combat.States
{
    public class Loss : BattleState
    {
        public Loss(BattleManager battleManager) : base(battleManager)
        {
        }
        public override IEnumerator Start()
        {
            BattleManager.onLos?.Invoke();

            yield return new WaitForSeconds(6f);
            
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }

    }
}