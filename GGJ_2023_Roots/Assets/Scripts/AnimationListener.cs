using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    public PlayerController playerController;
    
    public void Throw()
    {
        playerController.ThrowFromHand();
    }
}
