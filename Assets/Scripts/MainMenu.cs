using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace alexshko.prisonescape.Core
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel(string name)
        {
            MyLoadScene(name).ConfigureAwait(true);
        }

        public void QuitGame() {
            Application.Quit();
        }

        private async Task MyLoadScene(string name)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            while (!ao.isDone)
            {
                await Task.Delay(20);
            }
        }
    }
}
