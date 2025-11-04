using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float restTime = 30.0f;
    private float activeTimeRemaining;
    [SerializeField] bool roundActive = true;
    [SerializeField] int roundNum = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeTimeRemaining = restTime;
    }

    // Update is called once per frame
    void Update()
    {
        // This causes issues with the Input System Package
        //if (Input.GetKeyDown(KeyCode.P))
        {
          //  EndActivePhase();
        }

        if (activeTimeRemaining <= 0.0f)
        {
            BeginActivePhase();
        }

        if (!roundActive)
        {
            activeTimeRemaining -= Time.deltaTime;
            if (activeTimeRemaining < 0) activeTimeRemaining = 0.0f;
            int minutes = Mathf.FloorToInt(activeTimeRemaining / 60);
            int seconds = Mathf.FloorToInt(activeTimeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        } 
    }

    public void BeginActivePhase()
    {
        roundActive = true;
    }
    public void EndActivePhase()
    {
        ++roundNum;
        roundActive = false;

        activeTimeRemaining = restTime;

        // Debugging
        Debug.Log("End");
    }
}
