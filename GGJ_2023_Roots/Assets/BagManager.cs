using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    public static BagManager Instance { get; private set; }

    public TextMeshProUGUI bagCounter1;
    public TextMeshProUGUI bagCounter2;
    public int bagsNum1 = 5;
    public int bagsNum2 = 5;
    public GameObject pickedBagWarningPrefab;
    public GameObject noBagsWarningPrefab;
    public GameObject cantCarryMoreBagsWarningPrefab;
    private GameObject pickedBagWarning_p1;
    private GameObject pickedBagWarning_p2;
    private GameObject noBagsWarning_p1;
    private GameObject noBagsWarning_p2;
    private GameObject cantCarryMoreBagsWarning_p1;
    private GameObject cantCarryMoreBagsWarning_p2;
    private GameObject p1UI;
    private GameObject p2UI;
    public Transform p1;
    public Transform p2;
    public bool isNearBagTable_p1 = false;
    public bool isNearBagTable_p2 = false;

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
        bagsNum1 = 5;
        bagsNum2 = 5;
        bagCounter1.text = bagsNum1.ToString();
        bagCounter2.text = bagsNum2.ToString();
    }

    public void Update()
    {
        
        if (pickedBagWarning_p1 != null)
        {
            pickedBagWarning_p1.transform.position = p1.transform.position+ new Vector3(0,2,1);
        }
        if (pickedBagWarning_p2 != null)
        {
            pickedBagWarning_p2.transform.position = p2.transform.position + new Vector3(0, 2, 1);
        }
        if (noBagsWarning_p1 != null)
        {
            noBagsWarning_p1.transform.position = p1.transform.position + new Vector3(0, 2, 1);
        }
        if (noBagsWarning_p2 != null)
        {
            noBagsWarning_p2.transform.position = p2.transform.position + new Vector3(0, 2, 1);
        }
        if (cantCarryMoreBagsWarning_p1 != null)
        {
            cantCarryMoreBagsWarning_p1.transform.position = p1.transform.position + new Vector3(0, 2, 1);
        }
        if (cantCarryMoreBagsWarning_p2 != null)
        {
            cantCarryMoreBagsWarning_p2.transform.position = p2.transform.position + new Vector3(0, 2, 1);
        }
    }

    public void PickedUpBag(int id)
    {
        if (id == 1)
        {
            if (bagsNum1 == 5)
            {
                cantCarryMoreBagsWarning_p1 = Instantiate(cantCarryMoreBagsWarningPrefab, p1.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                StartCoroutine(DestroyP1UI());
                return;
            }
            bagsNum1 = 5;
            bagCounter1.text = bagsNum1.ToString();
            if (pickedBagWarning_p1 != null)
            {
                return;
            }
            pickedBagWarning_p1 = Instantiate(pickedBagWarningPrefab, p1.position+new Vector3(0,2,0), Quaternion.Euler(90, 0, 0));
            StartCoroutine(DestroyP1UI());
        }
        else
        {
            if (bagsNum2 == 5)
            {
                cantCarryMoreBagsWarning_p2 = Instantiate(cantCarryMoreBagsWarningPrefab, p2.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                StartCoroutine(DestroyP2UI());
                return;
            }
            bagsNum2 = 5;
            bagCounter2.text = bagsNum2.ToString();
            if (pickedBagWarning_p2 != null)
            {
                return;
            }
            pickedBagWarning_p2 = Instantiate(pickedBagWarningPrefab, p2.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
            StartCoroutine(DestroyP2UI());
        }  
    }

    IEnumerator DestroyP1UI()
    {
        yield return new WaitForSeconds(1.5f);
        if (pickedBagWarning_p1 != null)
        {
            Destroy(pickedBagWarning_p1);
        }
        if (cantCarryMoreBagsWarning_p1 != null)
        {
            Destroy(cantCarryMoreBagsWarning_p1);
        }
        if (noBagsWarning_p1 != null)
        {
            Destroy(noBagsWarning_p1);
        }
    }

    IEnumerator DestroyP2UI()
    {
        yield return new WaitForSeconds(1.5f);
        if (pickedBagWarning_p2 != null)
        {
            Destroy(pickedBagWarning_p2);
        }
        if (cantCarryMoreBagsWarning_p2 != null)
        {
            Destroy(cantCarryMoreBagsWarning_p2);
        }
        if (noBagsWarning_p2 != null)
        {
            Destroy(noBagsWarning_p2);
        }
    }

    public void UsedBag(int id)
    {
        if (id == 1)
        {
            if (bagsNum1 == 0)
            {
                noBagsWarning_p1 = Instantiate(noBagsWarningPrefab, p1.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                StartCoroutine(DestroyP1UI());
                return;
            }
            bagsNum1 = bagsNum1 - 1;
            bagCounter1.text = bagsNum1.ToString();
            
            if (noBagsWarning_p1 != null)
            {
                return;
            }
            
        }
        else
        {
            if (bagsNum2 == 0)
            {
                noBagsWarning_p2 = Instantiate(noBagsWarningPrefab, p2.position + new Vector3(0, 2, 0), Quaternion.Euler(90, 0, 0));
                StartCoroutine(DestroyP2UI());
                return;
            }
            bagsNum2 = bagsNum2 + 1;
            bagCounter2.text = bagsNum2.ToString();
            if (noBagsWarning_p2 != null)
            {
                return;
            }
        }
    }
}
