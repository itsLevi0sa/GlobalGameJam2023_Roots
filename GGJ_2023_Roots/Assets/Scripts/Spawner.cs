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
    List<Transform> availableTiles = new List<Transform>();

    private void Start()
    {
        SpawnTiles();
      //  StartCoroutine(SpawnRoots());
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
                Root root = Instantiate(rootPrefab, GetRandomTile(), Quaternion.identity).GetComponent<Root>();
                root.rootSide = RootSide.Left;
                root.spawner = this;
                root.playerController = playerController;
                rootNumber++;
            }
            yield return new WaitForSeconds(spawnRateSeconds);
        }
    }

    Vector3 GetRandomTile()
    {
        
        Transform newTile = availableTiles[Random.Range(0, availableTiles.Count)];
        availableTiles.Remove(newTile);
        return newTile.position;
    }


    Vector3 GetRandomXZ(Vector4 bounds)
    {
        float randomX = Random.Range(bounds.x, bounds.y);
        float randomY = Random.Range(bounds.z, bounds.w);
        return new Vector3(randomX, 0f, randomY);
    }
    

    public void DecreaseNumber()
    {
        rootNumber--;
    }
}
