using UnityEngine;

public interface IHittable 
{
    public void Hit();
}
public class TempEnemy : MonoBehaviour, IHittable
{
    public void Hit()
    {
        Debug.Log($"{gameObject.name} got hit!");
    }
}
