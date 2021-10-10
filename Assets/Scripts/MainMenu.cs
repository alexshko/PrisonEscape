using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace alexshko.prisonescape.Core
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel(string name)
        {
            MyLoadScene(name);
        }

        public void QuitGame() {
            Application.Quit();
        }

        private async void MyLoadScene(string name)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            while (!ao.isDone)
            {
                await Task.Delay(20);
            }
        }
    }
}
