using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDelegate : MonoBehaviour
{
    public delegate void EventHandler(Collider2D other);
    public event EventHandler collideWithPlayer;
    private void OnTriggerEnter2D(Collider2D other) {
        collideWithPlayer(other);
    }
}
