using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonersSpawnEngine : MonoBehaviour
    {
        //public Transform EscapeTargetsTreeRoot;
        //private GameObject[] EscapeTargets;
        private List<Transform> EscapeTargets;

        private void Start()
        {
            EscapeTargets = new List<Transform>();
            fIndAllEscapeTargets();
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
            //EscapeTargets = GameObject.FindGameObjectsWithTag("EscapeTarget");

        }
    }
}
