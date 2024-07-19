using UnityEngine;
using UnityEngine.UI;
using LoadScene;

namespace UI
{
    public class MenuSceneUI : MonoBehaviour
    {
        [SerializeField]
        private Button newGamebutton;

        [SerializeField]
        private Button quitGameButton;

        private void Start()
        {
            newGamebutton.onClick.AddListener(LoadGameScene);
            newGamebutton.onClick.AddListener(QuitGame);
        }

        private void LoadGameScene()
        {
            Loader.Load(Loader.Scene.GameScene);
            Debug.Log("Load Game Scene");
        }

        private void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}
