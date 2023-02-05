using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    public int rootLevel;
    public float lvl1Life, lvl2Life;
    public GameObject bagObject;
    public GameObject canvas;
    public Image fillImage;
    public float collectTime;
    public bool playerNear;
    public Spawner spawner;
    public RootSide rootSide;
    public PlayerController playerController;
    private float timer;
    public GameObject root1Obj, root2Obj, root3Obj;

    enum Phase
    {
        First, Second, Third
    }

    private Phase phase;
    
    
    private void OnEnable()
    {
        GameEvents.OnInteract += HandlePlayerInteraction;
    }

    private void OnDisable()
    {
        GameEvents.OnInteract -= HandlePlayerInteraction;
    }

    void HandlePlayerInteraction(Player playerType)
    {
        if(playerType == Player.P1 && rootSide == RootSide.Left && playerNear)
            StartCoroutine(CircleFill());
        else if (playerType == Player.P2 && rootSide == RootSide.Right && playerNear)
            StartCoroutine(CircleFill());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        switch (phase)
        {
            case Phase.First:
                if (timer >= lvl1Life)
                {
                    phase = Phase.Second;
                    timer = 0f;
                    root1Obj.SetActive(false);
                    root2Obj.SetActive(true);
                }
                break;
            case Phase.Second:
                if (timer >= lvl2Life)
                {
                    phase = Phase.Third;
                    timer = 0f;
                    root2Obj.SetActive(false);
                    root3Obj.SetActive(true);
                }
                break;
        }
    }


    IEnumerator CircleFill()
    {
        canvas.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < collectTime)
        {
            fillImage.fillAmount = elapsedTime / collectTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvas.SetActive(false);
        playerController.GetBag();
        GetComponentInParent<Tile>().HighlightOff();
        Picked();
    }


    void Picked()
    {
        spawner.DecreaseNumber(transform.parent);
        Destroy(gameObject);
    }


    public void HighlightOn()
    {
        
    }

    public void HighlightOff()
    {
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playerNear = true;
            GetComponentInParent<Tile>().HighlightOn();
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playerNear = false;
            StopAllCoroutines();
            fillImage.fillAmount = 0f;
            canvas.SetActive(false);
            GetComponentInParent<Tile>().HighlightOff();
        }
    }
}