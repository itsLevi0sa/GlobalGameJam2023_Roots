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
        Picked();
    }


    void Picked()
    {
        spawner.DecreaseNumber();
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
        }
    }
}