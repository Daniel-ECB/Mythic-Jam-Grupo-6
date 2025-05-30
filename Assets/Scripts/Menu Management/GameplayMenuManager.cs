using MythicGameJam.Audio;
using MythicGameJam.Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicGameJam.UI.Menus
{
    public sealed class GameplayMenuManager : Singleton<GameplayMenuManager>
    {
        public enum SubmenuType
        {
            PauseMenu,
            VictoryMenu
        }

        [Header("Submenu References")]
        [SerializeField]
        private PauseMenu _pauseMenu;
        [SerializeField]
        private VictoryMenu _victoryMenu;

        [SerializeField]
        private AudioClip _gameplayClip;

        private readonly Dictionary<SubmenuType, UIMenu> _submenuMap = new();
        private SubmenuType? _currentSubmenu;

        protected override void Awake()
        {
            base.Awake();
            InitializeSubmenuMap();
            HideAllSubmenus();
        }

        private void Start()
        {
            if (_gameplayClip != null)
                BaseAudioSystem.Instance.PlayMusic(_gameplayClip);
        }

        private void InitializeSubmenuMap()
        {
            _submenuMap[SubmenuType.PauseMenu] = _pauseMenu;
            _submenuMap[SubmenuType.VictoryMenu] = _victoryMenu;
        }

        public void ShowSubmenu(SubmenuType submenu, bool instant = false)
        {
            if (_currentSubmenu == submenu)
                return;

            StartCoroutine(ShowSubmenuRoutine(submenu, instant));
        }

        private IEnumerator ShowSubmenuRoutine(SubmenuType submenu, bool instant)
        {
            // Hide all submenus with fade out
            foreach (var element in _submenuMap.Values)
            {
                if (element != null && element.gameObject.activeSelf)
                    yield return StartCoroutine(element.LeaveMenu(instant));
            }

            if (_submenuMap.TryGetValue(submenu, out var menu) && menu != null)
            {
                menu.gameObject.SetActive(true);
                yield return StartCoroutine(menu.EnterMenu(instant));
            }

            _currentSubmenu = submenu;
        }

        public void HideAllSubmenus(bool instant = false)
        {
            StartCoroutine(HideAllSubmenusRoutine(instant));
        }

        private IEnumerator HideAllSubmenusRoutine(bool instant)
        {
            foreach (var menu in _submenuMap.Values)
            {
                if (menu != null && menu.gameObject.activeSelf)
                    yield return StartCoroutine(menu.LeaveMenu(instant));
            }
            _currentSubmenu = null;
        }
    }
}
