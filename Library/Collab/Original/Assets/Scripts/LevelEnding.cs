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
            Debug.Log("teleporting");
            {
                //other.gameObject.transform.position = new Vector3(-8.5781f, 1.681356f, 1.419181e-07f);
                other.gameObject.transform.position = spawn.transform.position;

            }
            //other.transform.position = RandomSpawn.instance.StartPointForPlayer.position;//здесь телепартируем поцыка на старт
            //other.GetComponent<Transform>().position = new Vector3(-8.5f, 0, 1.5f);
            //other.transform.position = spawn.transform.position;

            splashScreen.GetComponent<Animator>().SetTrigger("Activate"); //здесь включаем анимацию затемнения
            
            //Здесь перегенерация монет и блоков

            GameManager.instance.TempScore++;
            PlayerPrefs.SetInt("TempScore", GameManager.instance.TempScore);
        }
    }

}
