using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace alexshko.prisonescape.Core
{
    public class PrisonerEngine : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent navAgent;
        // Start is called before the first frame update
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            navAgent.SetDestination(target.position);
        }
    }
}
