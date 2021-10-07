using UnityEngine;
using UnityEngine.AI;

namespace alexshko.prisonescape.Prisoners
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
