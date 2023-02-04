using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Spawner spawner;
    public Root root;
    [HideInInspector] public PlayerController playerController;
    public RootSide rootSide;

    private void Awake()
    {
        spawner = GetComponentInParent<Spawner>();
    }

    private void Start()
    {
        root.playerController = playerController;
        root.spawner = spawner;
    }

    public void RevealRoot()
    {
        root.rootSide = rootSide;
        root.gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
