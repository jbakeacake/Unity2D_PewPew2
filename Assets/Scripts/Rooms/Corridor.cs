using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    public Room parentRoom;
    public TriggerDelegate roomTriggerDelegate;

    private RoomManager manager;
    
    void Awake()
    {
        this.manager = FindObjectOfType<RoomManager>();
        this.roomTriggerDelegate.collideWithPlayer += (other) =>
        {
            if (other.CompareTag("Player")) 
            {
                manager.tryGenerateAdjoiningRoom(parentRoom, new Vector2Int((int)transform.up.x, (int)transform.up.y));
            }
        };
    }
}
