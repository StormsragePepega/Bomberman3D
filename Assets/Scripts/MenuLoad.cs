using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject optionPanel;

    private void Awake()
    {
        optionPanel.SetActive(false);
    }
    
    public void GoPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadOptionsMenuPanel(bool check)
    {
        if (check)
        {
            optionPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        else
        {
            optionPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
    public void ResetRecord()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }
}
