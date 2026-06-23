using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    [Header("Collision Info")]
    public readonly static float EnemyCollisionRadius = .1f;
    private void Awake()
    {
        Instance = this;
    }

}