using MythicGameJam.Core.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace MythicGameJam.UI.Menus
{
    public sealed class MainMenu : UIMenu
    {
        [SerializeField]
        private Button _leaveButton;

        protected override void Awake()
        {
            base.Awake();

#if UNITY_WEBGL && !UNITY_EDITOR
            _leaveButton.gameObject.SetActive(false);
#endif
        }

        public void OnLoadGameplaySceneButton(string sceneName)
        {
            GameManager.Instance.LoadScene(sceneName, GameManager.Instance.StartGameplay);
        }
    }
}
