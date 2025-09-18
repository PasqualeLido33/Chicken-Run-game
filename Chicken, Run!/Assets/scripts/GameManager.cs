using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    private bool waitingForRestart = false;

    void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    public void PlayerRespawned()
    {
        Debug.Log("Respawn");
        // Ferma il tempo
        Time.timeScale = 0f;
        waitingForRestart = true;

        // Mostra messaggio
        messageText.text = "Press SPACE to run!";
        messageText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (waitingForRestart && Input.GetKeyDown(KeyCode.Space))
        {
            // Riprendi il gioco
            Time.timeScale = 1f;
            waitingForRestart = false;

            // Nascondi messaggio
            messageText.gameObject.SetActive(false);
        }
    }
}