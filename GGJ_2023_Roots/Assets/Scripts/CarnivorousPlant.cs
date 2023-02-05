using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorousPlant : MonoBehaviour
{
    public AudioSource hitaudioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            hitaudioSource.Play();
            other.GetComponent<PlayerController>().GetDamage();
            GetComponentInParent<Root>().ReturnToLvl1();
        }
    }
}
