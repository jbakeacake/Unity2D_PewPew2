using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StretchOpen : MonoBehaviour
{
    public TriggerDelegate triggerDelegate;
    public float speed;
    private Vector3 originalPosition;
    
    void Start()
    {
        this.originalPosition = transform.position;

        // Start in the closed position
        transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y);
        transform.localScale = new Vector3(transform.localScale.x, 0f);

        this.triggerDelegate.collideWithPlayer += onPlayerEnter;
    }

    private void onPlayerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    
    void Update()
    {

        if (Mathf.Approximately((this.originalPosition - transform.position).magnitude, 0.0f))
        {
            Debug.Log("CLOSE ENOUGH?!");
            return;
        }
        
        float previousYPosition = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, this.originalPosition, speed * Time.deltaTime);
        float deltaY = transform.position.y - previousYPosition;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + deltaY);
    }
}
