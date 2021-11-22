using UnityEngine;
using System.Collections;
public class LevelEnding : MonoBehaviour
{
    public GameObject splashScreen;
    public GameObject spawn;

    public Transform DestrBlocksParent;
    public Transform CoinsParent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Time.timeScale = 0;

            StartCoroutine(EnableTime());

            other.gameObject.SetActive(false);

            PlayerScript.instance.Teleport(spawn.transform.position); //здесь телепартируем поцыка на старт

            splashScreen.GetComponent<Animator>().SetTrigger("Activate"); //здесь включаем анимацию затемнения

            GenerateNewLevel();

            GameManager.instance.TempScore++;
            PlayerPrefs.SetInt("TempScore", GameManager.instance.TempScore);

            other.gameObject.SetActive(true);
        }
    }

    void GenerateNewLevel()
    {
        foreach (Transform child in DestrBlocksParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in CoinsParent)
        {
            Destroy(child.gameObject);
        }

        RandomSpawn.instance.CreatingPointsWithFreeSpace();
        RandomSpawn.instance.SpawnDestrBlocks();
        RandomSpawn.instance.SpawnCoins();
    }

    public IEnumerator EnableTime()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;
    }
}


