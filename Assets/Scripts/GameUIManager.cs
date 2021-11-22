using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public static GameUIManager instance;
    public GameObject go;
    [Header("Panels:")]
    public GameObject sceneGamePause;
    public GameObject sceneGameOver;

    [SerializeField] private Text money;

    private bool paused = false;

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
    }

    void Start()
    {
        sceneGameOver.SetActive(false);
        sceneGamePause.SetActive(false);
    }

    public void LoadMenu()
    {
        GameManager.instance.SetRecord();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void LoadNewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void LoadGameOverPanel()
    {
        GameManager.instance.SetRecord();
        Time.timeScale = 0;
        sceneGameOver.SetActive(true);
    }
    public void LoadPausePanel()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            sceneGamePause.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            sceneGamePause.SetActive(false);
        }
    }
    public void ClickToDrop()
    {
        PlayerScript.instance.DropBomb();
    }
    public void UpdateMoneyText(int amount)
    {
        money.text = amount.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        go.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        #if UNITY_EDITOR
        go.SetActive(false);
        #endif
    }
}
