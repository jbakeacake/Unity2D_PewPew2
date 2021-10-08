using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    public Room parentRoom;
    public RoomGenerateTrigger roomTrigger;
    public Door.DOOR_STATE currentDoorState;
    public Door[] doors;

    private RoomManager manager;
    
    void Awake()
    {
        this.roomTrigger.collideWithPlayer += () => manager.tryGenerateAdjoiningRoom(parentRoom, new Vector2Int((int)transform.up.x, (int)transform.up.y));
    }
    void Start()
    {
        this.currentDoorState = Door.DOOR_STATE.OPEN;
        this.manager = FindObjectOfType<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentDoorState = currentDoorState == Door.DOOR_STATE.OPEN ? Door.DOOR_STATE.CLOSED : Door.DOOR_STATE.OPEN;
        }

        slideDoors();
    }

    private void slideDoors()
    {
        foreach (Door d in this.doors)
        {
            d.slide(currentDoorState);
        }
    }
}
