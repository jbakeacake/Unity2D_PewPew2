using System.Collections;
using UnityEngine;

public class ExitRoom : Room
{
    public Transform exitOrigin;
    public GameObject portalPrefab;

    protected override void onPlayerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPlayerEntered)
            {
                hasPlayerEntered = true;
                StartCoroutine(pauseThenPerform());
            }

            roomManager.miniMapController.setActiveCoordinate(mapCoordinate);
        }
    }

    private IEnumerator pauseThenPerform()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(portalPrefab, exitOrigin);
    }
}