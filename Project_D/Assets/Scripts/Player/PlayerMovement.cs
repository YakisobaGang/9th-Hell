using System;
using ProjectD.Dialogue;
using ProjectD.Player.Input;
using UnityEngine;

namespace ProjectD.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        public event Action<Vector2> OnPlayerMove;

        private PlayerInputActions inputActions;
        private Rigidbody rb;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            DialogueManager.OnDialogueStart += HandleStartDialogue;
            DialogueManager.OnDialogueEnd += HandleStartDialogue;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void OnDestroy()
        {
            DialogueManager.OnDialogueStart -= HandleStartDialogue;
            DialogueManager.OnDialogueEnd -= HandleStartDialogue;
        }

        private void HandleStartDialogue()
        {
            enabled = !enabled;
        }

        private void Movement()
        {
            var inputVector = inputActions.OpenArea.Movement.ReadValue<Vector2>();
            OnPlayerMove?.Invoke(inputVector.normalized);

            rb.velocity = new Vector3(inputVector.x, 0, inputVector.y) * speed;
        }
    }
}