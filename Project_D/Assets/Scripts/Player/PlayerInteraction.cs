using System;
using ProjectD.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectD.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                OnPlayerPressInteraction?.Invoke();
            }
        }

        public static event Action OnPlayerPressInteraction;
    }
}