using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum Type
    {
        SoulPiece,
        GravePiece
    }

    [SerializeField] Type type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        PlayerManager.Instance.ModifyResource(this.type, 1);
    }

}
