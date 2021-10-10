using alexshko.prisonescape.Core;
using UnityEngine;
using UnityEngine.AI;

namespace alexshko.prisonescape.Prisoners
{
    public class PrisonerEngine : MonoBehaviour
    {
        //the target to go to using the NevMeshAgent
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
            //check if prisoner entered it's Escape target. if yes, finish the game and show proper message (by GameController):
            Debug.Log("Prisoner entered trigger");
            if (other.tag == "EscapeTarget")
            {
                GameController.Instance.FinishGame();
            }
        }
    }
}
