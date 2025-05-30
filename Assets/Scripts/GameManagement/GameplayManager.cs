using MythicGameJam.Core.Utils;
using UnityEngine;
using UnityEngine.UI;
using MythicGameJam.UI.Menus;

namespace MythicGameJam.Core.GameManagement
{
    public sealed class GameplayManager : Singleton<GameplayManager>
    {
        [Header("Score UI")]
        [SerializeField]
        private Image _manaIndicatorFill;

        [Header("Win Condition")]
        [SerializeField]
        private int killsToWin = 20;

        private int _currentKills = 0;
        private bool _hasWon = false;

        private void Start()
        {
            _currentKills = 0;
            _hasWon = false;
            UpdateScoreUI();
        }

        public void RegisterEnemyKill()
        {
            if (_hasWon)
                return;

            _currentKills++;
            UpdateScoreUI();

            if (_currentKills >= killsToWin)
            {
                _hasWon = true;
                HandleVictory();
            }
        }

        private void UpdateScoreUI()
        {
            if (_manaIndicatorFill != null)
                _manaIndicatorFill.fillAmount = Mathf.Clamp01(_currentKills / (float)killsToWin);
        }

        private void HandleVictory()
        {
            GameManager.Instance.PauseGame(() =>
                GameplayMenuManager.Instance.ShowSubmenu(GameplayMenuManager.SubmenuType.VictoryMenu));
        }
    }
}
