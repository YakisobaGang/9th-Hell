using System;
using ProjectD.Player;
using UnityEngine;

namespace ProjectD
{
    public class PlayerAnimController : MonoBehaviour
    {
        private Animator anim;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int isWalking = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GetComponent<PlayerMovement>().OnPlayerMove += HandlePlayerMoveDir;
        }

        private void OnDisable()
        {
            GetComponent<PlayerMovement>().OnPlayerMove -= HandlePlayerMoveDir;
        }

        private void HandlePlayerMoveDir(Vector2 dir)
        {
            print($"X: {dir.x:N1} | Y: {dir.y:N1}");

            if ((dir.x, dir.y) == (0, 0))
            {
                anim.SetBool(isWalking, false);
                anim.SetFloat(Horizontal, dir.x);
                anim.SetFloat(Vertical, dir.y);
                return;
            }
            anim.SetBool(isWalking, true);
            anim.SetFloat(Horizontal, dir.x);
            anim.SetFloat(Vertical, dir.y);
        }
    }
}
