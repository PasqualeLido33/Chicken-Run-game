using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    public GameManager gameManager;
    bool Died = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Died = true;
            player.transform.position = respawnPoint.position;
        }
    }
    

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Died == true) { gameManager.PlayerRespawned();  Died = false; Debug.Log("Died"); }
    }
}
