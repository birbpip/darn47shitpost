using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateboi : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        

        // Check if an AudioSource component exists
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the GameObject. Please add one.");
        }
    }

    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        // Rotate the object around the Y-axis
        Quaternion rotation = Quaternion.Euler(0f, 4f, 0f);
        transform.rotation = transform.rotation * rotation;
    }

    // This method is called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the AudioSource is available
        if (audioSource != null && audioSource.clip != null)
        {
            // Play the audio clip
            audioSource.Play();

            // Destroy the GameObject after the audio clip finishes playing
            Destroy(gameObject);
        }
        else
        {
            // If no audio clip is available, destroy immediately
            Destroy(gameObject);
        }
    }
}