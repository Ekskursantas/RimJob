using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private Vector2 mouseTravel;
    private Vector2 smooth;
    private Vector2 lastValidMouseMove;
    private Vector2 mouse;
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;
    public GameObject character;

    private bool cameraLock = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraLock) return;
        mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouse = Vector2.Scale(mouse, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
        smooth.x = Mathf.Lerp(smooth.x, mouse.x, 1f / smoothness);
        smooth.y = Mathf.Lerp(smooth.y, mouse.y, 1f / smoothness);
        mouseTravel += smooth;
        var clampY = Mathf.Clamp(mouseTravel.y, -75f, 75f);
        mouseTravel.y = clampY;
        transform.localRotation = Quaternion.AngleAxis(-mouseTravel.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseTravel.x, character.transform.up);
    }
    
    public void lockCamera()
    {
        cameraLock = true;
    }

    public void unlockCamera()
    {
        cameraLock = false;
    }
}