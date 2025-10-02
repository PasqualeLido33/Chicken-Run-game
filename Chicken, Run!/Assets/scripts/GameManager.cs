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
    [SerializeField] Animator fence_animator;
    [SerializeField] Rigidbody2D playerbody;
    [SerializeField] BoxCollider2D antiCheat;

    private bool waitingForRestart = false;

    public float slowDuration = 0.5f; // quanto dura lo slow motion
    public float slowFactor = 0.5f;

    public int level = 1;
    void Start()
    {
        messageText.gameObject.SetActive(false);
        allDeathZones = FindObjectsOfType<killPlayer>();
    }

    public void PlayerRespawned()
    {

        EggCollider.gameObject.SetActive(true);
        EggSprite.gameObject.SetActive(true);
        antiCheat.isTrigger = false;
        fence_animator.SetBool("FenceDown", false);
        fence.isTrigger = false;


        Debug.Log("Respawn");
        
        // Aspetta il tempo rallentato
        StartCoroutine(DeathSlowMotion());

      



    }

    private IEnumerator DeathSlowMotion()
    {
        // Attiva slow motion
        slowFactor = 0.5f;
        Time.timeScale = slowFactor;
        playerbody.drag = 10000;
        messageText.text = "Press SPACE to run!";
        messageText.gameObject.SetActive(true);
        foreach (killPlayer dz in allDeathZones)
        {
            dz.Died = true;
        }

        // Aspetta "slowDuration" secondi reali (indipendente dal timeScale)
        yield return new WaitForSecondsRealtime(slowDuration);

        // Ripristina tempo normale
        Time.timeScale = 1f;

        
        player.transform.position = respawnPoint.position;

        
        waitingForRestart = true;
        
        
        foreach (killPlayer dz in allDeathZones)
        {
            dz.Died = false;
        }
        
    }

    

    void Update()
    {
        
        
            

        
        if (waitingForRestart && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            // Nascondi messaggio

            messageText.gameObject.SetActive(false);
            // Riprendi il gioco

            Time.timeScale = 1f;
            slowFactor = 1f;
            playerbody.drag = 0;
            

            waitingForRestart = false;
            

            
            
        }
    }
}