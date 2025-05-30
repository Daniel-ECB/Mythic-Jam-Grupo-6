using UnityEngine;
using UnityEngine.UI;

namespace MythicGameJam.Player
{
    public sealed class PlayerFormManager : MonoBehaviour
    {
        public enum PlayerForm { Human, Elephant, Cow }

        [Header("Form Prefabs")]
        [SerializeField]
        private GameObject[] formPrefabs; // 0: Human, 1: Elephant, 2: Cow

        [Header("UI")]
        [SerializeField]
        private Image hitRadialImage;

        [Header("Settings")]
        [SerializeField]
        private int hitsToTransform = 4;

        private int _currentFormIndex = 0;
        private int _currentHits = 0;
        private GameObject _currentFormInstance;

        private void Start()
        {
            SpawnForm(_currentFormIndex);
            UpdateUI();
        }

        private void Update()
        {
            // Simulate hit with space bar
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                TakeHit();
            }
        }

        public void TakeHit()
        {
            _currentHits++;
            if (_currentHits >= hitsToTransform)
            {
                _currentHits = 0;
                CycleForm();
            }
            UpdateUI();
        }

        private void CycleForm()
        {
            _currentFormIndex = (_currentFormIndex + 1) % formPrefabs.Length;
            SpawnForm(_currentFormIndex);
        }

        private void SpawnForm(int formIndex)
        {
            if (_currentFormInstance != null)
                Destroy(_currentFormInstance);

            _currentFormInstance = Instantiate(formPrefabs[formIndex], transform.position, transform.rotation, transform);
        }

        private void UpdateUI()
        {
            if (hitRadialImage != null)
                hitRadialImage.fillAmount = 1f - (_currentHits / (float)hitsToTransform);
        }

        public PlayerForm CurrentForm => (PlayerForm)_currentFormIndex;
    }
}
