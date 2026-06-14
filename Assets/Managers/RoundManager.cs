using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }   
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] GameObject skipRestButton;
    [SerializeField] float restTime = 30.0f;
    [SerializeField] float activeMaxTime = 60.0f;

    int roundNum = 0;

    internal bool roundActive;
    void Start()
    {
        Instance = this;
        BeginNextRound();
    }

    void Update()
    {
#if UNITY_EDITOR
        // This causes issues with the Input System Package
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (roundActive) BeginNextRound();
            else
            {
                timerText.text = string.Format("00:00");
                BeginActivePhase();
            }
        }
#endif
    }
    public static event Action TriggerRestPhase;
    public void BeginNextRound()
    {
        StopAllCoroutines();

        ++roundNum;
        roundText.text = string.Format("{0:00}", roundNum);
        skipRestButton.SetActive(true);

        roundActive = false;
        TriggerRestPhase?.Invoke();

        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        float timeRemaining = roundActive ? activeMaxTime : restTime;
        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null; // this waits until next frame
            timeRemaining -= Time.deltaTime;
        }

        if (roundActive) BeginNextRound();
        else BeginActivePhase();
    }

    public static event Action TriggerActivePhase;
    public void BeginActivePhase()
    {
        StopAllCoroutines(); // stops the above coroutine if active
        skipRestButton.SetActive(false);
        roundActive = true;
        TriggerActivePhase.Invoke();

        StartCoroutine(Timer());
    }

}
