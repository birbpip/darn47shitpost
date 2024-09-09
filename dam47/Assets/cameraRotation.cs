using System.Collections;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
   

    private float xRotation = 0f;
    private float yRotation = 0f;

    private bool isTimeStopped = false;
    private float originalTimeScale;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store original time scale
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        // Check if time is not stopped and handle camera rotation
        if (!isTimeStopped)
        {
            HandleCameraRotation();
        }

        // Check for input to trigger time stop
        if (Input.GetKeyDown(KeyCode.F) && !isTimeStopped)
        {
            StartCoroutine(StopTimeForHalfSecond());
        }

        
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 10f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -10f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }

    IEnumerator StopTimeForHalfSecond()
    {
        // Set time scale to 0 to pause time
        Time.timeScale = 0f;
        isTimeStopped = true;

        // Wait for half a second
        yield return new WaitForSecondsRealtime(0.5f);

        // Restore original time scale
        Time.timeScale = originalTimeScale;
        isTimeStopped = false;
    }
}