using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int timer;
    public TextMeshProUGUI timerText;
    public Spawner spawnerLeft, spawnerRight;
    public GameObject gameOverScreen;
    public TextMeshProUGUI winnerText;
    public AudioClip timerClip;
    public AudioSource audioSource;
    
    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    private void Update()
    {
        
    }

    IEnumerator TimerCoroutine()
    {
        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1f);
            if (timer < 10)
            {
                timerText.text = "0:0" + timer.ToString();
            }
            else
                timerText.text = "0:" + timer.ToString();
        }
        GameOver();
    }

    private bool threedone, twodone, onedone;
    void GameOver()
    {
        spawnerLeft.StopSpawning();
        spawnerRight.StopSpawning();
        gameOverScreen.SetActive(true);
        int p1Score = 24 - spawnerLeft.availableTiles.Count;
        int p2Score = 24 - spawnerRight.availableTiles.Count;
        if (p1Score < p2Score)
        {
            winnerText.text = "Player 1 wins!";
        }
        else if (p2Score < p1Score)
        {
            winnerText.text = "Player 2 wins!";
        }
        else if (p1Score == p2Score)
        {
            winnerText.text = "Tie!";
        }

        if (timer == 3 && !threedone)
        {
            threedone = true;
            audioSource.PlayOneShot(timerClip);
        }

        if (timer == 2 && !twodone)
        {
            twodone = true;
            audioSource.PlayOneShot(timerClip);
        }

        if (timer == 1 && !onedone)
        {
            onedone = true;
            audioSource.PlayOneShot(timerClip);
        }
    }
}
