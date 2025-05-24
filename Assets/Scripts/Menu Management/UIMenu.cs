using System;
using System.Collections;
using UnityEngine;

namespace MythicGameJam.UI.Menus
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIMenu : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _canvasGroup;
        [SerializeField]
        protected float _transitionDuration = 0.3f;

        public event Action OnMenuEntered;
        public event Action OnMenuLeft;

        protected virtual void Awake()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup == null)
                Debug.LogWarning($"{nameof(UIMenu)} on {gameObject.name} requires a CanvasGroup.");
        }

        protected virtual void OnValidate()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual IEnumerator EnterMenu(bool instant = false, Action onComplete = null)
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
                OnMenuEntered?.Invoke();
                onComplete?.Invoke();
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
            OnMenuEntered?.Invoke();
            onComplete?.Invoke();
        }

        public virtual IEnumerator LeaveMenu(bool instant = false, Action onComplete = null)
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
                OnMenuLeft?.Invoke();
                onComplete?.Invoke();
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
            OnMenuLeft?.Invoke();
            onComplete?.Invoke();
        }
    }
}
