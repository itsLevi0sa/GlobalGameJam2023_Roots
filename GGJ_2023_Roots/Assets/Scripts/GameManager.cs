using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int timer;
    public TextMeshProUGUI timerText;
    
    private void Start()
    {
        StartCoroutine(TimerCoroutine());
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
    }
}
