using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public static RandomMap instance;
    public GameObject pillarPrefab;
    public GameObject boxPrefab;
    public GameObject wallPrefab;
    public Map map = new Map(10, 22);
    public Generator generator = new Generator(10, 22);
    public void render()
    {
        for (int i = 0; i < map.height; i++)
        {
            for (int j = 0; j < map.width; j++)
            {
                Vector3 x = new Vector3(i * 0.45f, 0, j * 0.45f);
                Instantiate(boxPrefab, x, Quaternion.identity);
            }
        }

        // Start is called before the first frame update

        // Update is called once per frame

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        generator.GenerateMap(ref map);
        render();
    }
    void Start()
    { }
}
public enum Tile
{
    wall, pillar, box, empty
}
public class Map
{

    private Tile[,] MapTile;
    public int height;
    public int width;
    public Map(int height, int width)
    {
        this.height = height;
        this.width = width;
        MapTile = new Tile[height, width];
        this.fill(Tile.wall);

    }
    public Tile getTile(int x, int z)
    {
        return MapTile[z, x];
    }
    public void setTile(int x, int z, Tile value)
    {
        this.MapTile[z, x] = value;
    }
    public void fill(Tile value)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                MapTile[i, j] = value;
            }
        }
    }
}

public class Walker
{
    Vector2Int dir, position, mapSize;

    public Walker(Vector2Int mapSize, Vector2Int position)
    {
        this.mapSize = mapSize;
        this.position = position;
    }

    public void RngDirection()
    {
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                dir = new Vector2Int(0, 1);
                break;
            case 1:
                dir =new Vector2Int(0, -1);
                break;
            case 2:
                dir = new Vector2Int(1, 0);
                break;
            case 3:
                dir = new Vector2Int(-1, 0);
                break;
            default:
                dir = new Vector2Int(0, 0);
                break;
        }
    }

    public Vector2Int Walk()
    {
        if ((position + dir).x > 2 && (position + dir).x < mapSize.x - 2 && (position + dir).y > 2 && (position + dir).y < mapSize.y - 2)
            position += dir;
        return position;
    }

    public Vector2Int getPos()
    {
        return position;
    }
}

public class Generator
{
    List<Walker> walkers = new List<Walker>();

    int width, height, walkerCount, curWalkers;
    float walkerSpawnRate, walkerDieRate, mapSmooth, changeDirRate;

    public Generator(int width, int height)
    {
        this.width = width;
        this.height = height;
        walkerCount = 10;
        walkerSpawnRate = 0.05f;
        walkerDieRate = 0.05f;
        mapSmooth = 0.2f;
        changeDirRate = 0.6f;
    }

    //void addNewWalker(Walkerslist* root)
    //{
    //    walkers.add(new Walker(rng, Vector2Int(width, height), curPointer->walker->getPos()));
    //}

    //void deleteWalker()
    //{

    //}

    public void GenerateBoxes(ref Map map)
    {
        this.walkers.Add(new Walker(new Vector2Int(width, height), new Vector2Int(width / 2, height / 2)));
        curWalkers = 1;
        int floorCount = 1;
        map.setTile(height/2, width/2, Tile.box);
        int iterations = 0;
        // in process
        do
        {
            foreach (Walker walker in walkers)
            {
                if (Random.Range(0f, 1.0f) < walkerSpawnRate && curWalkers < walkerCount)
                {
                    this.walkers.Add(new Walker(new Vector2Int(width, height), walker.getPos()));
                    curWalkers++;
                }
            }
            foreach (Walker walker in walkers)
            {
                Vector2Int pos = walker.Walk();
                Tile generatedTile = Tile.wall;
                int rand = Random.Range(0, 11);
                if (rand < 7)
                    generatedTile = Tile.box;
                else
                    generatedTile = Tile.empty;
                if (map.getTile(pos.y, pos.x) == Tile.wall)
                {
                    floorCount++;
                }
                map.setTile(pos.y, pos.x, generatedTile);

            }
            foreach (Walker walker in walkers)
            {
                if (Random.Range(0f, 1.0f) < changeDirRate)
                {
                    walker.RngDirection();
                }
            }
            foreach (Walker walker in walkers)
            {
                if (Random.Range(0f, 1.0f) < walkerDieRate && curWalkers > 1)
                {
                    walkers.Remove(walker);
                    curWalkers--;
                }
            }
            float check = (float)floorCount / (float)(width * height);
            if ((float)floorCount / (float)(width * height) > mapSmooth)
                break;
            iterations++;
        } while (iterations < 1000);
    }


    public void GeneratePillars(ref Map map)
    {
        for (int x = 0; x < width - 1; x++)
            for (int z = 0; z < height - 1; z++)
            {
                Tile thisTile = map.getTile(x, z);
                if (thisTile == Tile.box || thisTile == Tile.empty)
                {
                    if (map.getTile(x + 1, z) == Tile.wall)
                        map.setTile(x + 1, z, Tile.pillar);

                    if (map.getTile(x - 1, z) == Tile.wall)
                        map.setTile(x - 1, z, Tile.pillar);

                    if (map.getTile(x, z + 1) == Tile.wall)
                        map.setTile(x, z + 1, Tile.pillar);

                    if (map.getTile(x, z - 1) == Tile.wall)
                        map.setTile(x, z - 1, Tile.pillar);
                }
            }
    }
    public void GenerateMap(ref Map map)
    {
        map.fill(Tile.wall);
        GenerateBoxes(ref map);
        GeneratePillars(ref map);
    }
}