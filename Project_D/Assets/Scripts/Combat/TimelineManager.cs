using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace ProjectD.Combat
{
    public class TimelineManager : MonoBehaviour
    {
        private PlayableDirector[] timelines;
        public static  TimelineManager Instance { get; private set; }
        private void Awake()
        {
            if (!(Instance is null) && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            
        }

        private void Start()
        {
            timelines = GameObject.FindObjectsOfType<PlayableDirector>(includeInactive:true);
        }

        public bool HasAnyTimelinesPlaying()
        {
            for (int i = 0; i < timelines.Length; i++)
            {
                if (timelines[i].state == PlayState.Playing)
                    return true;
            }
            return false;
        }
    }
}