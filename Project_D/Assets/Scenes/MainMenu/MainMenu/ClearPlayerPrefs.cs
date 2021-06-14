using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class ClearPlayerPrefs : MonoBehaviour
    {
        private void Awake()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
