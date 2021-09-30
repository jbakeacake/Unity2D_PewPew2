using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    public Door.DOOR_STATE currentDoorState;
    public Door[] doors;

    void Start()
    {
        currentDoorState = Door.DOOR_STATE.CLOSED;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
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
