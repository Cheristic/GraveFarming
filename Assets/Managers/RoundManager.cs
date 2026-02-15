using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class RoundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] float restTime = 30.0f;
    [SerializeField] int roundNum = 1;

    bool roundActive;
    void Start()
    {
        roundActive = false;
        // start rest period
        StartCoroutine(RestPeriodTimer());
        roundText.text = string.Format("{0:0}", roundNum);
    }

    void Update()
    {
        // This causes issues with the Input System Package
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (roundActive) BeginRestPhase();
            else
            {
                timerText.text = string.Format("00:00");
                BeginActivePhase();
            }
        }
    }

    IEnumerator RestPeriodTimer()
    {
        float timeRemaining = restTime;
        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null; // this waits until next frame
            timeRemaining -= Time.deltaTime;
        }

        BeginActivePhase();
    }

    public void BeginActivePhase()
    {
        StopAllCoroutines(); // stops the above coroutine if active
        roundActive = true;
    }
    public void BeginRestPhase()
    {
        ++roundNum;
        roundActive = false;

        roundText.text = string.Format("{0:00}", roundNum);

        // Debugging
        Debug.Log("End");

        StartCoroutine(RestPeriodTimer());
    }
}
