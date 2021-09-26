using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHelper
{
    public static void lookAtCursor(Transform origin, Camera playerCamera)
    {
        Vector2 mouseScreenPosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)origin.position).normalized;

        origin.up = Vector2.Lerp(origin.up, direction, 8.0f * Time.deltaTime);
    }

    public static void lookAtTarget(Transform origin, Transform target)
    {
        if (target == null) return;
        Vector2 direction = (target.position - origin.position).normalized;
        origin.up = Vector2.Lerp(origin.up, direction, 8.0f * Time.deltaTime);
    }

    public static void swapSides(Transform actor, Transform center, Camera playerCamera)
    {

        Vector2 mouseScreenPosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        float offset = -5.0f * (mouseScreenPosition.x.CompareTo(center.position.x));
        Vector2 finalLocalPosition = mouseScreenPosition.x < center.position.x + offset
            ? Vector2.right
            : -Vector2.right;
        finalLocalPosition.y = actor.transform.localPosition.y;
        actor.localPosition = Vector2.Lerp(actor.localPosition, finalLocalPosition, 8.0f * Time.deltaTime);
    }
}
