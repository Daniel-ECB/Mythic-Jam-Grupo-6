using UnityEngine;

public class BaseAudioSystem : MonoBehaviour
{
    public static BaseAudioSystem instance;
    public AudioSource audioSource;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
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

    /* Code for pause and settings Menu
    BaseAudioSystem.Instance.PauseMusic(); 
    BaseAudioSystem.Instance.ResumeMusic();*/

}
