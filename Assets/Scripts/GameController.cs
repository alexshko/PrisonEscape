using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class GameController : MonoBehaviour
    {
        public Transform gameOverUIRef;
        public bool isGameActive { get; set; }
        public static GameController Instance
        {
            get => instance;
        }
        private static GameController instance;

        private void Awake()
        {
            instance = this;
            isGameActive = true;
        }

        public void FinishGame()
        {
            Debug.Log("Finished Game.");
            if (gameOverUIRef)
            {
                gameOverUIRef.gameObject.SetActive(true);
                isGameActive = false;
            }
        }
    }
}
