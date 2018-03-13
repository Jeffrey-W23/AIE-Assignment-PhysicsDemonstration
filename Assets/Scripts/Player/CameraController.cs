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

    //
    public Vector3 offset;

    // public camera value for the camera compoent
    public Camera m_cCamera;
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    //
    private float m_fRotationX = 0.0f;
    private float m_fRotationY = 0.0f;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        //
        Cursor.lockState = CursorLockMode.Locked;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        //
        m_fRotationX += Input.GetAxis("Mouse Y") * m_fSensitivityX;
        m_fRotationY += Input.GetAxis("Mouse X") * m_fSensitivityY;

        //
        m_fRotationX = Mathf.Clamp(m_fRotationX, m_fMinimumX, m_fMaximumX);

        //
        m_cCamera.transform.position = transform.position + offset;

        //
        transform.localEulerAngles = new Vector3(0, m_fRotationY, 0);

        //
        m_cCamera.transform.localEulerAngles = new Vector3(-m_fRotationX, m_fRotationY, 0);

        //
        if (Input.GetKey(KeyCode.Escape))
        {
            //
            Cursor.lockState = CursorLockMode.None;

            //
            Cursor.visible = true;
        }
    }

    //--------------------------------------------------------------------------------------
    // Movement: 
    //--------------------------------------------------------------------------------------
    void Movement()
    {

    }
}
