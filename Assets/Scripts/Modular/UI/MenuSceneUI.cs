using UnityEngine;
using UnityEngine.UI;
using LoadScene;

namespace UI
{
    public class MenuSceneUI : MonoBehaviour
    {
        public void LoadGameScene()
        {
            Loader.Load(Loader.Scene.GameScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
