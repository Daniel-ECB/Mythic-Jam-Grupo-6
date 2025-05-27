using MythicGameJam.Core.Utils;
using UnityEngine;

namespace MythicGameJam.Audio
{
    public sealed class BaseAudioSystem : Singleton<BaseAudioSystem>
    {
        [SerializeField]
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();

            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.Play();
            }
        }

        public void PauseMusic()
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }

        public void ResumeMusic()
        {
            if (!audioSource.isPlaying)
                audioSource.UnPause();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
