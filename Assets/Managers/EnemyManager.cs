using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Main { get; private set; }
    private void Awake()
    {
        if (Main != null && Main != this) Destroy(this);
        else Main = this;
    }


}
