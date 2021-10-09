using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonersSpawnEngine : MonoBehaviour
    {
        public Transform prisonerPref;
        public Transform spawnStartPosition;
        public int PrisonersPerInterval = 3;
        public float secondsBetweenIntervals = 2;

        private List<Transform> EscapeTargets;
        private float timeLastSpawn;


        private void Start()
        {
            EscapeTargets = new List<Transform>();
            timeLastSpawn = Time.time;
            fIndAllEscapeTargets();
        }

        private void Update()
        {
            if (Time.time >= timeLastSpawn + secondsBetweenIntervals)
            {
                MakeSpawn();
            }
        }

        private void MakeSpawn()
        {
            timeLastSpawn = Time.time;
            for (int i=0;i<PrisonersPerInterval; i++)
            {
                PrisonerNavigationEngine prisoner = Instantiate(prisonerPref, spawnStartPosition.position, Quaternion.identity).GetComponent<PrisonerNavigationEngine>();
                prisoner.target = chooseRandomTarget();
                prisoner.startGoingToTarget();
            }
        }

        private Transform chooseRandomTarget()
        {
            //is it distributed uniformly?
            int rnd = (int)(UnityEngine.Random.Range(0,EscapeTargets.Count));
            return EscapeTargets.ToArray()[rnd];
        }

        private void fIndAllEscapeTargets()
        {

            foreach (var possibleTarget in GetComponentsInChildren<Transform>())
            {
                if (possibleTarget.tag == "EscapeTarget")
                {
                    EscapeTargets.Add(possibleTarget);
                }
            }

        }
    }
}
