// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// CameraController object. Inheriting from MonoBehaviour. The main class for camera movement
// behaviour.
//--------------------------------------------------------------------------------------
public class CameraController : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public floats for the min and max of the camera movement
    public float m_fMinimumX = -60.0f;
    public float m_fMaximumX = 60.0f;
    public float m_fMinimumY = -360.0f;
    public float m_fMaximumY = 360.0f;

    // public floats for the camera sensitivity
    public float m_fSensitivityX = 15.0f;
    public float m_fSensitivityY = 15.0f;

    // public vector3 for the offset of the camera
    public Vector3 m_v3Offset;

    // public camera value for the camera compoent
    public Camera m_cCamera;
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private floats for the camera rotation
    private float m_fRotationX = 0.0f;
    private float m_fRotationY = 0.0f;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Lock the cursor to unity
        Cursor.lockState = CursorLockMode.Locked;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // Get mouse axis
        m_fRotationX += Input.GetAxis("Mouse Y") * m_fSensitivityX;
        m_fRotationY += Input.GetAxis("Mouse X") * m_fSensitivityY;

        // clamp the roation
        m_fRotationX = Mathf.Clamp(m_fRotationX, m_fMinimumX, m_fMaximumX);

        // set the postion of the camera
        m_cCamera.transform.position = transform.position + m_v3Offset;
        transform.localEulerAngles = new Vector3(0, m_fRotationY, 0);
        m_cCamera.transform.localEulerAngles = new Vector3(-m_fRotationX, m_fRotationY, 0);

        // if escape key
        if (Input.GetKey(KeyCode.Escape))
        {
            // unlock the cursor fdrom unity
            Cursor.lockState = CursorLockMode.None;

            // set cursor back to visible
            Cursor.visible = true;
        }
    }
}
