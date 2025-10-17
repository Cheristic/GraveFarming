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

    public void GoToGame()
    {

    }

    public void ReturnToMainMenu()
    {

    }
}
