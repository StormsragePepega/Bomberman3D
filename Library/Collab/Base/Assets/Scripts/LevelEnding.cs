using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnding : MonoBehaviour
{
    public GameObject splashScreen;
    public GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.transform.position = RandomSpawn.instance.StartPointForPlayer.position;//здесь телепартируем поцыка на старт
            //other.GetComponent<Transform>().position = new Vector3(-8.5f, 0, 1.5f);
            other.GetComponent<Transform>().position = spawn.transform.position;

            splashScreen.GetComponent<Animator>().SetTrigger("Activate"); //здесь включаем анимацию затемнения
            
            //Здесь перегенерация монет и блоков

            GameManager.instance.TempScore++;
            PlayerPrefs.SetInt("TempScore", GameManager.instance.TempScore);
        }
    }
}
