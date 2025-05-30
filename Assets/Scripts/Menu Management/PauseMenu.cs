using MythicGameJam.Core.GameManagement;
using UnityEngine.SceneManagement;

namespace MythicGameJam.UI.Menus
{
    public sealed class PauseMenu : UIMenu
    {
        public void OnContinueButton()
        {
            GameManager.Instance.StartGameplay();
        }

        public void OnRestartButton()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.LoadScene(currentScene, GameManager.Instance.StartGameplay);
        }

        public void OnMainMenuButton()
        {
            GameManager.Instance.LoadScene("MainMenu", GameManager.Instance.GoToHomeScreen);
        }
    }
}
