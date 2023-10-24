using UnityEngine;
using UnityEngine.Audio;

public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl Instance;
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioSource audioButton, audioWin, audioLose, audioPick, audioScorePlus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeVolume(float vol)
    {
        // Tính toán lại giá trị Để giá trị của Slider (0.0001 - 1) phù hợp với Volume của AudioMixer
        AudioMixer.SetFloat("Master", Mathf.Log10(vol) * 20);
    } 

    // Âm thanh cho các trường hợp
    public void AudioButton()
    {
        audioButton.Play();
    }
    public void AudioWin()
    {
        audioWin.Play();
    }
    public void AudioLose()
    {
        audioLose.Play();
    }
    public void AudioPick()
    {
        audioPick.Play();
    }
    public void AudioScorePlus()
    {
        audioScorePlus.Play();
    }
}
