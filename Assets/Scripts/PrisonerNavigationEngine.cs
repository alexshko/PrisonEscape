using UnityEngine;
using UnityEngine.AI;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonerNavigationEngine : MonoBehaviour
    {
        public Transform target { get; set; }

        private NavMeshAgent navAgent;
        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        public void startGoingToTarget()
        {
            navAgent.SetDestination(target.position);

        }
    }
}
