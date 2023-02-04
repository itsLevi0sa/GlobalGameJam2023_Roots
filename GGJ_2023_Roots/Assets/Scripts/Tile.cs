using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Spawner spawner;
    public Root root;
    [HideInInspector] public PlayerController playerController;
    public RootSide rootSide;
    public Color normalColor, highlightColor;
    public MeshRenderer tileMeshRenderer;

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

    public void HighlightOn()
    {
        tileMeshRenderer.material.color = highlightColor;
    }
    public void HighlightOff()
    {
        tileMeshRenderer.material.color = normalColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
