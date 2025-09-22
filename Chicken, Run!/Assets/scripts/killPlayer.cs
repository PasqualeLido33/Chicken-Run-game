using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    public bool Died = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Died = true;
            print(Died);
            
        }
    }
    

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Died) { gameManager.PlayerRespawned(); }
    }
}
