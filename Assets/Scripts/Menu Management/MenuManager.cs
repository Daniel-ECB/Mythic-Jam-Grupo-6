using MythicGameJam.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicGameJam.UI.Menus
{
    public sealed class MenuManager : MonoBehaviour
    {
        public enum MenuType
        {
            Main,
            Options,
            Credits
        }

        [Header("Menu References")]
        [SerializeField]
        private UIMenu _mainMenu;
        [SerializeField]
        private UIMenu _optionsMenu;
        [SerializeField]
        private UIMenu _creditsMenu;

        [SerializeField]
        private AudioClip _mainMenuClip;

        private readonly Dictionary<MenuType, UIMenu> _menuMap = new();
        private MenuType _currentMenu;
        private Coroutine _transitionRoutine;
        private bool _isTransitioning;

        public event Action<MenuType> OnMenuChanged;

        private void Awake()
        {
            InitializeMenuMap();
            ValidateMenus();
            SwitchMenu(MenuType.Main, instant: true);
        }

        private void Start()
        {
            if (_mainMenuClip != null)
                BaseAudioSystem.Instance.PlayMusic(_mainMenuClip);
        }

        private void InitializeMenuMap()
        {
            _menuMap[MenuType.Main] = _mainMenu;
            _menuMap[MenuType.Options] = _optionsMenu;
            _menuMap[MenuType.Credits] = _creditsMenu;
        }

        private void ValidateMenus()
        {
            foreach (var pair in _menuMap)
            {
                if (pair.Value == null)
                    Debug.LogWarning($"Menu reference for {pair.Key} is not assigned in MenuManager.", this);
            }
        }

        private void SwitchMenu(MenuType target, bool instant = false)
        {
            if (_isTransitioning || _currentMenu == target)
                return;

            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);

            _transitionRoutine = StartCoroutine(HandleTransition(_currentMenu, target, instant));
        }

        private IEnumerator HandleTransition(MenuType from, MenuType to, bool instant)
        {
            _isTransitioning = true;

            if (_menuMap.TryGetValue(from, out var fromMenu) && fromMenu != null)
                yield return fromMenu.LeaveMenu(instant);

            if (_menuMap.TryGetValue(to, out var toMenu) && toMenu != null)
                yield return toMenu.EnterMenu(instant);

            _currentMenu = to;
            _isTransitioning = false;
            _transitionRoutine = null;
            OnMenuChanged?.Invoke(to);
        }

        public void OnClickShowMainMenu() => SwitchMenu(MenuType.Main);
        public void OnClickShowOptions() => SwitchMenu(MenuType.Options);
        public void OnClickShowCredits() => SwitchMenu(MenuType.Credits);
    }
}
