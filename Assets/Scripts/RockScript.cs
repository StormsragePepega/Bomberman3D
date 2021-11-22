using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public GameObject rockEffect;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        GameObject effect = (GameObject)Instantiate(rockEffect, transform.position, transform.rotation);

        Destroy(effect, 2f);

        if (other.CompareTag("Player"))
        {
            GameUIManager.instance.LoadGameOverPanel();
            Destroy(other);
        }
    }
}
