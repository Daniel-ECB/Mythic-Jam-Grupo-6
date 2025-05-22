using System.Collections;
using UnityEngine;

namespace MythicGameJam.UI.Menus
{
    public abstract class UIMenu : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _transitionDuration = 0.3f;

        public virtual IEnumerator EnterMenu(bool instant = false)
        {
            gameObject.SetActive(true);

            if (_canvasGroup == null) yield break;

            float time = 0f;
            float startAlpha = _canvasGroup.alpha;

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            if (instant)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                yield break;
            }

            while (time < _transitionDuration)
            {
                time += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, time / _transitionDuration);
                yield return null;
            }

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual IEnumerator LeaveMenu(bool instant = false)
        {
            if (_canvasGroup == null) yield break;

            float time = 0f;
            float startAlpha = _canvasGroup.alpha;

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            if (instant)
            {
                _canvasGroup.alpha = 0f;
                gameObject.SetActive(false);
                yield break;
            }

            while (time < _transitionDuration)
            {
                time += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / _transitionDuration);
                yield return null;
            }

            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
    }
}
