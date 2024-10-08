using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    public delegate void JumpHandle();
    public JumpHandle JumpEvent;
    [SerializeField]
    Player player;
    public bool iceBrock = false;

   
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Slorp") || other.gameObject.CompareTag("DamageObject") || other.gameObject.CompareTag("ItemBox"))
        {
            JumpEvent.Invoke();
            player.OffJump();
        }
        
        
    }
}