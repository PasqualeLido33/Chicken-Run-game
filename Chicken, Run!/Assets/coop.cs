using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coop : MonoBehaviour
{

    [SerializeField] Collider2D coopCollider;
    public GameManager gameManager;


    private void OnTriggerEnter2D(Collider2D coop)
    {
        if (coop.CompareTag("Player"))
        {
            gameManager.level++; print(gameManager.level);
        }
    }
}
