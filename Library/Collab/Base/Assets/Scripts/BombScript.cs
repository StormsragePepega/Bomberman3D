using UnityEngine;
using System.Collections;
public class BombScript : MonoBehaviour
{
    private RandomSpawn rs;

    public GameObject explosionPrefab;
    public LayerMask levelMask;
    private bool exploded = false;
    public int explength;

    private isHitBlock block;

    // Start is called before the first frame update
    void Start()
    {
        rs = RandomSpawn.instance;
        Invoke("Explode", 3f);
    }

    void Explode()
    {
        exploded = true;
        GameObject Ex1 = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 0.3f);
        Destroy(Ex1, 0.5f);
    }

    public IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 0; i < explength; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, 1 + i, levelMask);

            if (gameObject.CompareTag("MyBomb"))
            {
                if (!hit.collider)
                {
                    GameObject Exes = Instantiate(explosionPrefab, transform.position + ((1 + i) * direction), explosionPrefab.transform.rotation);
                    Destroy(Exes, 0.5f);
                }
                else if (hit.collider.tag == "Destroyable")
                {
                    block = hit.collider.gameObject.GetComponent<isHitBlock>();
                    if (block.isAlive)
                    {
                        rs.coordinates.Add(hit.collider.gameObject.transform.position);
                        block.isAlive = false;
                    }
                    Destroy(hit.collider.gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
            else if (gameObject.CompareTag("RandomBomb"))
            {
                if (!hit.collider)
                {
                    GameObject Exes = Instantiate(explosionPrefab, transform.position + ((1 + i) * direction), explosionPrefab.transform.rotation);
                    Destroy(Exes, 0.5f);
                }
                else
                {
                    break;
                }
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }
}
