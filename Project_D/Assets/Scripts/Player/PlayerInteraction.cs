using System;
using ProjectD.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectD.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerInputActions inputActions;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.OpenArea.Interact.performed += HandleInteractPress;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            inputActions.OpenArea.Interact.performed -= HandleInteractPress;
        }

        public static event Action OnPlayerPressInteraction;

        private void HandleInteractPress(InputAction.CallbackContext obj)
        {
            OnPlayerPressInteraction?.Invoke();
        }
    }
}