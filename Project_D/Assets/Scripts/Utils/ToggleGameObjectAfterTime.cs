using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace ProjectD.Utils
{
    public class ToggleGameObjectAfterTime : MonoBehaviour
    {
        [SerializeField] private float timeInSeconds;
        [SerializeField] private GameObject @object;

        public void ToggleAfterTime()
        {
            StartCoroutine(CToggleAfterTime());
        }

        IEnumerator CToggleAfterTime()
        {
            yield return new WaitForSeconds(timeInSeconds);

            @object.SetActive(!@object.activeInHierarchy);
        }
    }
}