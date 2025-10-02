using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public Collider2D Collider;
    public SpriteRenderer Sprite;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D fence;
    [SerializeField] BoxCollider2D antiCheat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("FenceDown", true);
            fence.isTrigger = true;
            Collider.gameObject.SetActive(false);
            Sprite.gameObject.SetActive(false);
            antiCheat.isTrigger = true;
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
