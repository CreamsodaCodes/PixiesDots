using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float zoomSpeed = 8f;

    int i = 0;
    private void Update()
    {
        
        // Camera movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (i<1000)
        {
         horizontal = 0.01f; 
         vertical = 0.01f;  
        }
        if (2000>i&&i>1000)
        {
           horizontal = -0.01f; 
            vertical = -0.01f;   
        }
        if (2000<i)
        {
            i = 0;
        }
        i++;
        Vector3 direction = new Vector3(horizontal, vertical, 0f) * moveSpeed * Time.deltaTime*Camera.main.orthographicSize;
        transform.Translate(direction, Space.World);
        // Camera zoom
        float zoomInput = Input.GetAxis("Zoom");
        float zoomAmount = zoomInput * zoomSpeed * Time.deltaTime;
        Camera.main.orthographicSize -= zoomAmount;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 1000f);
    }
}