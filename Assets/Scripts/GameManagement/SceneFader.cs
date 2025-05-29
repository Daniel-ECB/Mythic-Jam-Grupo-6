using System.Collections;
using UnityEngine;

namespace MythicGameJam.Core.GameManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class SceneFader : Core.Utils.Singleton<SceneFader>
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _fadeDuration = 0.5f;

        protected override void Awake()
        {
            base.Awake();

            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        public IEnumerator FadeOut()
        {
            gameObject.SetActive(true); // Ensure the fader is active before fading out
            yield return Fade(1f);
        }

        public IEnumerator FadeIn()
        {
            yield return Fade(0f);
            gameObject.SetActive(false); // Deactivate after fade-in is complete
        }

        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = _canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < _fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / _fadeDuration;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            _canvasGroup.alpha = targetAlpha;
        }
    }
}
