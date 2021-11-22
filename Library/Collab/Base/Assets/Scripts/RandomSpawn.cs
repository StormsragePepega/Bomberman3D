using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
    public static RandomSpawn instance;

    public GameObject rockPrefab;
    public GameObject coinPrefab;
    public GameObject destBlockPrefab;
    public GameObject playerPrefab;

    private Transform startPointForPlayer;
    public Transform StartPointForPlayer
    {
        get
        {
            return startPointForPlayer;
        }
        set
        {
            startPointForPlayer = value;
        }
    }

    public Transform DestrBlocksParent;
    public Transform CoinsParent;

    public Transform firstPoint;
    public Transform lastPoint;
    public Vector3 boxSize;
    private Vector3 first, second;
    public List<Vector3> coordinates = new List<Vector3>(); //список координат для пустых мест
    public List<Vector3> listForStalactite = new List<Vector3>(); //список координат над платформой
    [SerializeField]
    private int coinsAmount;
    public int CoinsAmount
    {
        get
        {
            return coinsAmount;
        }
        set
        {
            coinsAmount = value;
        }
    }
    [SerializeField]
    private int destBlockAmount;    
    public int DestBlockAmount
    {
        get
        {
            return destBlockAmount;
        }
        set
        {
            destBlockAmount = value;
        }
    }

    [SerializeField]
    public int rocksRandAmount;

    public float timeDelayCurrent;
    public float timeDelayMin;
    public float timeDelayChange;

    System.Random random = new System.Random();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SpawnPlayer();
        CreatingPointsWithFreeSpace();
        CreatingPointsForRocks();
        SpawnDestrBlocks();
        SpawnCoins();
    }
    void Start()
    {
        StartCoroutine(SpawnRocks());
    }

    public void CreatingPointsWithFreeSpace()
    {
        first = firstPoint.position;
        second = lastPoint.position;
        Collider[] colliders;
        float xPozStart = first.x;

        for (; first.z >= second.z;)
        {
            for (; first.x <= second.x;)
            {
                colliders = Physics.OverlapBox(first, boxSize);
                if (colliders.Length == 0)
                {
                    coordinates.Add(first);
                }
                first.x++;
            }
            first.z--;
            first.x = xPozStart;
        }
    }

    public void CreatingPointsForRocks()
    {
        first = firstPoint.position + new Vector3(0, 10, 0);
        second = lastPoint.position + new Vector3(0, 10, 0);
        Collider[] colliders;
        float xPozStart = first.x;

        for (; first.z >= second.z;)
        {
            for (; first.x <= second.x;)
            {
                colliders = Physics.OverlapBox(first, boxSize);
                if (colliders.Length == 0)
                {
                    listForStalactite.Add(first);
                }
                first.x++;
            }
            first.z--;
            first.x = xPozStart;
        }
    }

    void SpawnPlayer()
    {
        startPointForPlayer = gameObject.GetComponent<Transform>();
        Instantiate(playerPrefab, startPointForPlayer.position, startPointForPlayer.rotation);
    }
    public void SpawnCoins()
    {
        Vector3 tempCoin;
        List<Vector3> coinSpawnPos = new List<Vector3>();
        Collider[] colliders;

        for (int i = 0; i < coinsAmount; i++)
        {
        M1:
            tempCoin = coordinates[random.Next(coordinates.Count)];

            foreach (Vector3 item in coinSpawnPos)
            {
                if (item == tempCoin)
                {
                    goto M1;
                }
            }
            coinSpawnPos.Add(tempCoin);
            colliders = Physics.OverlapBox(tempCoin, boxSize);
            if (colliders.Length == 0)
            {
                Instantiate(coinPrefab, tempCoin - new Vector3(0, (boxSize.y + 0.05f) / 2), Quaternion.identity, CoinsParent);
            }
            else
            {
                goto M1;
            }
            coinSpawnPos.Clear();
        }
    }

    public void SpawnDestrBlocks()
    {
        Vector3 tempDestrBlocks;
        List<Vector3> DestrBlocks = new List<Vector3>();
        Collider[] colliders;

        for (int i = 0; i < destBlockAmount; i++)
        {
        M1:
            tempDestrBlocks = coordinates[random.Next(coordinates.Count)];

            foreach (Vector3 item in DestrBlocks)
            {
                if (item == tempDestrBlocks)
                {
                    goto M1;
                }
            }
            DestrBlocks.Add(tempDestrBlocks);
            colliders = Physics.OverlapBox(tempDestrBlocks, boxSize);
            if (colliders.Length == 0)
            {
                Instantiate(destBlockPrefab, tempDestrBlocks - new Vector3(0, (boxSize.y) / 2), Quaternion.identity, DestrBlocksParent);
            }
            else
            {
                goto M1;
            }
            DestrBlocks.Clear();
        }
    }

    IEnumerator SpawnRocks()
    {
        Vector3 tempCoordRock;
        List<Vector3> rockSpawnPos = new List<Vector3>();

        while (true)
        {
            for (int i = 0; i < rocksRandAmount; i++)
            {
            M2:
                tempCoordRock = listForStalactite[random.Next(listForStalactite.Count)];

                foreach (Vector3 item in rockSpawnPos)
                {
                    if (item == tempCoordRock)
                    {
                        goto M2;
                    }
                }
                rockSpawnPos.Add(tempCoordRock);
                Instantiate(rockPrefab, tempCoordRock, Quaternion.identity);

                yield return new WaitForSeconds(0.5f);
            }

            rockSpawnPos.Clear();

            yield return new WaitForSeconds(timeDelayCurrent);

            if (timeDelayCurrent > timeDelayMin && (timeDelayCurrent - timeDelayChange) > timeDelayMin)
            {
                timeDelayCurrent -= timeDelayChange;
            }
        }
    }
}
