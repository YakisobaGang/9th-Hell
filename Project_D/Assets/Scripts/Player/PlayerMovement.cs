using UnityEngine;

namespace ProjectD.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private PlayerInputActions inputActions;
        private Rigidbody rb;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            rb = GetComponent<Rigidbody>();
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

        private void Movement()
        {
            var inputVector = inputActions.OpenArea.Movement.ReadValue<Vector2>();

            rb.velocity = new Vector3(inputVector.x, 0, inputVector.y) * speed;
        }
    }
}