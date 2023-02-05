using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RootSide
{
    Right, Left
}

public class Spawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public Vector2 tileSize;
    public GameObject rootPrefab;
    public float spawnRateSeconds;
    public int maxPlayerRoots;
    public PlayerController playerController;
    public RootSide rootSide;
    int rootNumber;
    public List<Transform> availableTiles = new List<Transform>();

    private void Start()
    {
        SpawnTiles();
        StartCoroutine(SpawnRoots());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    void SpawnTiles()
    {
        for (int i = 0; i < tileSize.x; i++)
        {
            for (int j = 0; j < tileSize.y; j++)
            {
                GameObject newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector3(i * 2, 0f, j * 2);
                availableTiles.Add(newTile.transform);
                newTile.GetComponent<Tile>().playerController = playerController;
                newTile.GetComponent<Tile>().rootSide = rootSide;
            }
        }
    }

    IEnumerator SpawnRoots()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            if (rootNumber < maxPlayerRoots)
            {
                GetRandomTile().RevealRoot();
                rootNumber++;
            }
            yield return new WaitForSeconds(spawnRateSeconds);
        }
    }

    Tile GetRandomTile()
    {
        Transform newTile = availableTiles[Random.Range(0, availableTiles.Count)];
        availableTiles.Remove(newTile);
        return newTile.GetComponent<Tile>();
    }

    public int GetNumberOfActiveRoots()
    {
        return 1;
    }

    public void DecreaseNumber(Transform tile)
    {
        availableTiles.Add(tile);
        rootNumber--;
    }
}
