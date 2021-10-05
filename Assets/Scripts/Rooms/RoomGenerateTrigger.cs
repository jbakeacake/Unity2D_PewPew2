using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerateTrigger : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler collideWithPlayer;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) 
        {
            collideWithPlayer();
        }
    }
}
