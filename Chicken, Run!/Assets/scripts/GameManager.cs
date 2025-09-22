using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Collider2D EggCollider;
    public SpriteRenderer EggSprite;
    public TextMeshProUGUI messageText;
    public Transform respawnPoint;
    private killPlayer[] allDeathZones;
    [SerializeField] BoxCollider2D fence;
    [SerializeField] Animator animator;


    private bool waitingForRestart = false;

    public float slowDuration = 1.5f; // quanto dura lo slow motion
    public float slowFactor = 0.2f;

    int level = 1;
    void Start()
    {
        messageText.gameObject.SetActive(false);
        allDeathZones = FindObjectsOfType<killPlayer>();
    }

    public void PlayerRespawned()
    {

        EggCollider.gameObject.SetActive(true);
        EggSprite.gameObject.SetActive(true);
        animator.SetBool("FenceDown", false);
        fence.isTrigger = false;


        Debug.Log("Respawn");

        // Aspetta il tempo rallentato
        StartCoroutine(DeathSlowMotion());

      



    }

    private IEnumerator DeathSlowMotion()
    {
        // Attiva slow motion
        Time.timeScale = slowFactor;
        

        // Aspetta "slowDuration" secondi reali (indipendente dal timeScale)
        yield return new WaitForSecondsRealtime(slowDuration);

        // Ripristina tempo normale
        Time.timeScale = 0f;


        // Mostra messaggio di restart
        foreach (killPlayer dz in allDeathZones)
        {
            dz.Died = false;
        }
            player.transform.position = respawnPoint.position;
        waitingForRestart = true;
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