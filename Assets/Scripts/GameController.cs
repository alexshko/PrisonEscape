using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class GameController : MonoBehaviour
    {
        [Tooltip("the UI panel to show when the game is over")]
        public Transform gameOverUIRef;

        //boolean to indicate if the game is playing currently. mainly for syncing with other classes:
        public bool isGamePlaying { get; set; }

        //singelton pattern:
        public static GameController Instance
        {
            get => instance;
        }
        private static GameController instance;

        private void Awake()
        {
            instance = this;
            isGamePlaying = true;
        }

        public void FinishGame()
        {
            Debug.Log("Finished Game.");
            if (gameOverUIRef)
            {
                //call the game over Panel:
                gameOverUIRef.gameObject.SetActive(true);
                isGamePlaying = false;
            }
        }
    }
}
