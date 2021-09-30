using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DOOR_STATE
    {
        OPEN,
        CLOSED
    }

    public float doorSpeed = 3.0f;
    public Transform left, right;
    private Vector2 leftClosePosition, rightClosePosition;
    private Vector2 leftOpenPosition, rightOpenPosition;
    void Start()
    {
        this.leftClosePosition = left.localPosition;
        this.rightClosePosition = right.localPosition;
        this.leftOpenPosition = new Vector2(this.leftClosePosition.x - left.localScale.x, left.localPosition.y);
        this.rightOpenPosition = new Vector2(this.rightClosePosition.x + right.localScale.x, right.localPosition.y);
    }

    public void slide(DOOR_STATE state)
    {
        if (state == DOOR_STATE.OPEN)
        {
            left.localPosition = Vector2.Lerp(left.localPosition, leftOpenPosition, doorSpeed * Time.deltaTime);
            right.localPosition = Vector2.Lerp(right.localPosition, rightOpenPosition, doorSpeed * Time.deltaTime);
        }
        else
        {
            left.localPosition = Vector2.Lerp(left.localPosition, leftClosePosition, doorSpeed * Time.deltaTime);
            right.localPosition = Vector2.Lerp(right.localPosition, rightClosePosition, doorSpeed * Time.deltaTime);
        }
    }
}
