using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonersSpawnEngine : MonoBehaviour
    {
        //public Transform EscapeTargetsTreeRoot;
        private GameObject[] EscapeTargets;

        private void Start()
        {
            fIndAllEscapeTargets();
        }

        private void fIndAllEscapeTargets()
        {

            //foreach (var possibleTarget in EscapeTargetsTreeRoot.GetComponentsInChildren<Transform>())
            //{
            //    if (possibleTarget.tag == "EscapeTarget")
            //    {
            //        EscapeTargets.Add(possibleTarget);
            //    }
            //}
            EscapeTargets = GameObject.FindGameObjectsWithTag("EscapeTarget");

        }
    }
}
