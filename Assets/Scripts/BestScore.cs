using UnityEngine.UI;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    Text text;

    //void Start()
    //{
    //    text = GetComponent<Text>();

    //    int bestScore = PlayerPrefs.GetInt("BestScore");

    //    text.text = "BEST SCORE IS: " + bestScore;
    //}

    void Update()
    {
        text = GetComponent<Text>();

        int bestScore = PlayerPrefs.GetInt("BestScore");

        text.text = "BEST SCORE IS: " + bestScore;
    }
}
