using System;
using UnityEngine;

// Highest level global script meant to manage the game as a whole and scene transitions
public class GameManager : MonoBehaviour
{
    public static GameManager Main { get; private set; }
    private void Awake()
    {
        if (Main != null && Main != this) Destroy(this);
        else Main = this;
        DontDestroyOnLoad(this);
    }

    internal bool GameIsActive = true;

    public void GoToGame()
    {
        GameIsActive = true;
    }

    public static event Action OnEndGame;
    public void EndGame()
    {
        Debug.Log("you lost :(");
        GameIsActive = false;
        OnEndGame?.Invoke();
    }

    public void ReturnToMainMenu()
    {

    }
}
