using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Spawner spawner;
    private GameObject root;

    private void Awake()
    {
        spawner = GetComponentInParent<Spawner>();
        root = transform.GetChild(0).gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
