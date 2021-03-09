using ProjectD.Abilitys;
using ProjectD.Commands;
using ProjectD.Interfaces;
using UnityEngine;

namespace ProjectD.Player.Combat
{
    public class PlayerTurnBaseCombat : MonoBehaviour
    {
        public CommandHandler commandHandler = new CommandHandler();

        public AbilityBase[] myAbilitys = new AbilityBase[0];
    }
}