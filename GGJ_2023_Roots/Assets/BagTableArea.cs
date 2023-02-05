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
        BagManager.Instance.isNearBagTable = true;
        if (other.name == "Player 1")
        {
            BagManager.Instance.PickedUpBag(1);
        }
        else
        {
            BagManager.Instance.PickedUpBag(2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bagPickupAnimator.GetComponent<Animator>().enabled = false;

        bagHighlight.GetComponent<Animator>().Play("BagNotHighlighted", -1, 0.0f);
        BagManager.Instance.isNearBagTable = false;
        if (other.name == "Player 1")
        {
            BagManager.Instance.DestroyP1UI();
        }
        else
        {
            BagManager.Instance.DestroyP2UI();
        }
    }
}
