using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int tempScore = 0;
    public int TempScore
    {
        get
        {
            return tempScore;
        }
        set
        {
            tempScore = value;
        }
    }
    [SerializeField]
    private int bestScore = 0;
    public int BempScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore = value;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("TempScore", tempScore);
        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    public void SetRecord()
    {
        if (tempScore > bestScore)
        {
            bestScore = tempScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
    public void SetTotalCoins()
    {
        if (tempScore > bestScore)
        {
            bestScore = tempScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
