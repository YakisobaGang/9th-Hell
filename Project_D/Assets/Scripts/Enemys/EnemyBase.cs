using ProjectD.Commands;
using ProjectD.Interfaces;
using ProjectD.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectD.Enemys
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected Fighter enemyData;
    }
}