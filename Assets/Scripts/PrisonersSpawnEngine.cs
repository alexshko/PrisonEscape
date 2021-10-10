using alexshko.prisonescape.life;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonersSpawnEngine : MonoBehaviour
    {
        public Text[] CountOfKilledUIRef;
        public Transform prisonerPref;
        public Transform spawnStartPosition;
        public int PrisonersPerInterval = 3;
        public float secondsBetweenIntervals = 2;

        private List<Transform> EscapeTargets;
        private float timeLastSpawn;

        private int prisonersKilled;


        private void Start()
        {
            prisonersKilled = 0;
            EscapeTargets = new List<Transform>();
            timeLastSpawn = Time.time;
            fIndAllEscapeTargets();
        }

        private void Update()
        {
            if (Core.GameController.Instance.isGameActive)
            {
                if (Time.time >= timeLastSpawn + secondsBetweenIntervals)
                {
                    MakeSpawn();
                }
            }
        }

        private void MakeSpawn()
        {
            timeLastSpawn = Time.time;
            for (int i=0;i<PrisonersPerInterval; i++)
            {
                PrisonerEngine prisoner = Instantiate(prisonerPref, spawnStartPosition.position, Quaternion.identity).GetComponent<PrisonerEngine>();
                prisoner.GetComponent<LifeEngine>().OnDieEvent += PrisonerActionOnDeath;
                prisoner.target = chooseRandomTarget();
                prisoner.startGoingToTarget();
            }
        }

        private Transform chooseRandomTarget()
        {
            //is it distributed uniformly?
            int rnd = (int)(UnityEngine.Random.Range(0,EscapeTargets.Count));
            Debug.LogFormat("random number: {0}", rnd);
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

        private void PrisonerActionOnDeath()
        {
            prisonersKilled++;
            foreach (var txtUIItem in CountOfKilledUIRef)
            {
                txtUIItem.text = prisonersKilled.ToString();
            }
            Debug.LogFormat("Killed so far: {0}", prisonersKilled);
        }
    }
}
