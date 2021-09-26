using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Player;
    public float offset = 0.2f;
    public float cameraShakeDuration, cameraShakeMagnitude;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player == null) return;

        Vector3 xyPosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, xyPosition, offset);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            triggerCameraShake();
        }
    }

    private void triggerCameraShake()
    {
        if (Player == null) return;

        StartCoroutine(cameraShake());
    }

    private IEnumerator cameraShake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * cameraShakeMagnitude;
            float y = Random.Range(-1f, 1f) * cameraShakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
