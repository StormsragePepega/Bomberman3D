using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //public 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        gameObject.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("Volume"));
        DontDestroyOnLoad(gameObject);
    }
}
