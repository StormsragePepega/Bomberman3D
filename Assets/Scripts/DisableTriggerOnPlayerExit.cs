using UnityEngine;
public class DisableTriggerOnPlayerExit : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
