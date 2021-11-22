using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    private GameUIManager gameUIManager;

    private int coin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //coinsText = GetComponent<Text>();
        coin = PlayerPrefs.GetInt("Coins");

        gameUIManager = GameUIManager.instance;
        gameUIManager.UpdateMoneyText(coin);
    }

}
