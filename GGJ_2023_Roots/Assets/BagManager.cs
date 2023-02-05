using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    public static BagManager Instance { get; private set; }

    public TextMeshProUGUI bagCounterL;
    public TextMeshProUGUI bagCounterR;
    private int bagsNumL = 0;
    private int bagsNumR = 0;
    public GameObject pickedBagWarningPrefab;
    public GameObject noBagsWarningPrefab;
    private GameObject pickedBagWarning_p1;
    private GameObject pickedBagWarning_p2;
    private GameObject noBagsWarning_p1;
    private GameObject noBagsWarning_p2;
    private Transform p1UIpos;
    private Transform p2UIpos;
    public Transform p1;
    public Transform p2;
    public bool isNearBagTable = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        bagCounterL.text = bagsNumL.ToString();
        bagCounterR.text = bagsNumR.ToString();
    }

    public void Update()
    {
        p1UIpos = p1;
    }

    public void PickedUpBag(int id)
    {
        if (id == 1)
        {
            pickedBagWarning_p1 = Instantiate(pickedBagWarningPrefab, p1.position+new Vector3(0,2,0), Quaternion.Euler(90, 0, 0));
        }
        else
        {
            pickedBagWarning_p2 = Instantiate(pickedBagWarningPrefab, p2.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
        }  
    }

    public void DestroyP1UI()
    {
        if (pickedBagWarning_p1 != null)
        {
            Destroy(pickedBagWarning_p1);
        }
    }

    public void DestroyP2UI()
    {
        if (pickedBagWarning_p2 != null)
        {
            Destroy(pickedBagWarning_p2);
        }
    }

}
