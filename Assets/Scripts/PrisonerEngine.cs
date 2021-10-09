using alexshko.prisonescape.Core;
using UnityEngine;
using UnityEngine.AI;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonerEngine : MonoBehaviour
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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Prisoner entered trigger");
            if (other.tag == "EscapeTarget")
            {
                GameController.Instance.FinishGame();
            }
        }
    }
}
