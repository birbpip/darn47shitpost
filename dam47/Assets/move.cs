using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    // Public variable to set the speed of movement
    public float speed = 5f;

    // Public variable to set the target
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // Check if the target is not null
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // Normalize the direction to keep the speed constant

            // Rotate to look at the target
            transform.LookAt(target);

            // Move towards the target
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}