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

        private readonly Dictionary<MenuType, UIMenu> _menuMap = new();
        private MenuType _currentMenu;
        private Coroutine _transitionRoutine;

        private void Awake()
        {
            InitializeMenuMap();
            SwitchMenu(MenuType.Main, instant: true);
        }

        private void InitializeMenuMap()
        {
            _menuMap[MenuType.Main] = _mainMenu;
            _menuMap[MenuType.Options] = _optionsMenu;
            _menuMap[MenuType.Credits] = _creditsMenu;
        }

        private void SwitchMenu(MenuType target, bool instant = false)
        {
            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);

            _transitionRoutine = StartCoroutine(HandleTransition(_currentMenu, target, instant));
        }

        private IEnumerator HandleTransition(MenuType from, MenuType to, bool instant)
        {
            if (_menuMap.TryGetValue(from, out var fromMenu))
                yield return fromMenu.LeaveMenu(instant);

            if (_menuMap.TryGetValue(to, out var toMenu))
                yield return toMenu.EnterMenu(instant);

            _currentMenu = to;
            _transitionRoutine = null;
        }

        public void OnClickShowMainMenu() => SwitchMenu(MenuType.Main);
        public void OnClickShowOptions() => SwitchMenu(MenuType.Options);
        public void OnClickShowCredits() => SwitchMenu(MenuType.Credits);
    }
}
