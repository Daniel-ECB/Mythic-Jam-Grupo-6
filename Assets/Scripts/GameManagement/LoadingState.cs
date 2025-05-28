using System.Collections;
using UnityEngine.SceneManagement;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class LoadingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly string _targetScene;

        public LoadingState(GameManager gameManager, string targetScene)
        {
            _gameManager = gameManager;
            _targetScene = targetScene;
        }

        public void Enter()
        {
            _gameManager.StartCoroutine(LoadSceneWithFade());
        }

        public void Exit()
        {
            // Optionally: cleanup or hide loading UI
        }

        public void Update()
        {
            // Optionally: update loading UI/progress bar
        }

        private IEnumerator LoadSceneWithFade()
        {
            var fader = SceneFader.Instance;
            if (fader != null)
                yield return fader.FadeOut();

            var loadOp = SceneManager.LoadSceneAsync(_targetScene);
            while (!loadOp.isDone)
                yield return null;

            if (fader != null)
                yield return fader.FadeIn();

            _gameManager.StartGameplay();
        }
    }
}
