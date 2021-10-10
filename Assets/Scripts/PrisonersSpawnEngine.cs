using alexshko.prisonescape.life;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonersSpawnEngine : MonoBehaviour
    {
        [Tooltip("all Texts that will show the number of kills. " +
            "Updated after each kill")]
        public Text[] CountOfKilledUIRef;
        [Tooltip("Prifab of the prisoner:")]
        public Transform prisonerPref;
        [Tooltip("The position where all the prisoners will be instantiated at.")]
        public Transform spawnStartPosition;
        [Tooltip("How many prisoners to instantiate each interval")]
        public int PrisonersPerInterval = 3;
        [Tooltip("How many seconds between intervals")]
        public float secondsBetweenIntervals = 2;

        //for finding all the escape targets in the game:
        private List<Transform> EscapeTargets;
        private float timeLastSpawn;

        private int prisonersKilled;


        private void Start()
        {
            prisonersKilled = 0;
            EscapeTargets = new List<Transform>();
            //one second to prepare until first interval of prisoners:
            timeLastSpawn = Time.time - secondsBetweenIntervals + 1;
            fIndAllEscapeTargets();
        }

        private void Update()
        {
            if (Core.GameController.Instance.isGamePlaying)
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
                //upon the prisoner's death, call function for updating the score. By adding to the OnDieEvent of prisoner's LifeEngine:
                prisoner.GetComponent<LifeEngine>().OnDieEvent += PrisonerActionOnDeath;
                //prisoner's targets gets picked randomly. once set, he will use NavMeshAgent to go there:
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
