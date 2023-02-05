using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagTableArea : MonoBehaviour
{
    public GameObject bagPickupAnimator;
    public GameObject bagHighlight;

    private void Start()
    {
        bagPickupAnimator.GetComponent<Animator>().enabled = false;
        bagHighlight.GetComponent<Animator>().enabled = false;
        bagHighlight.GetComponent<Image>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bagPickupAnimator.GetComponent<Animator>().enabled = true;
        bagPickupAnimator.GetComponent<Animator>().Play("BagPickupPoint", - 1, 0.0f);
        bagHighlight.GetComponent<Image>().enabled = true;
        bagHighlight.GetComponent<Animator>().enabled = true;
        bagHighlight.GetComponent<Animator>().Play("BagHighlight", -1, 0.0f);
        
        //------for debug----------------------------
        if (other.name == "Player 1")
        {
            BagManager.Instance.isNearBagTable_p1 = true;
            BagManager.Instance.PickedUpBag(1);
        }
        else
        {
            BagManager.Instance.isNearBagTable_p2 = true;
            BagManager.Instance.PickedUpBag(2);
        }
        //----------------------------------------------------
    }
    private void OnTriggerExit(Collider other)
    {
        bagPickupAnimator.GetComponent<Animator>().enabled = false;

        bagHighlight.GetComponent<Animator>().Play("BagNotHighlighted", -1, 0.0f);
       
        if (other.name == "Player 1")
        {
            BagManager.Instance.isNearBagTable_p1 = false;
        }
        else
        {
            BagManager.Instance.isNearBagTable_p2 = false;
        }
    }
}
