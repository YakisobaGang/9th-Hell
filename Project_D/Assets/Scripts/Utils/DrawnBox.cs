using System;
using UnityEngine;

namespace ProjectD.Utils
{
#if UNITY_EDITOR
    public class DrawnBox : MonoBehaviour
    {
        [SerializeField] private Vector3 size;
        [SerializeField] private Color color;
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, size);
        }
    }
#endif
}