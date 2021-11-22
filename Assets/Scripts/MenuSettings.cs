using UnityEngine.UI;
using UnityEngine;
public class MenuSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    void Start()
    {
        audioSource = GameObject.Find("GameAudio").GetComponent<AudioSource>();

        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void setVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}