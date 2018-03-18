// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Player object. Inheriting from MonoBehaviour. The main player class for player movement
//--------------------------------------------------------------------------------------
public class Player : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public float value for the walking speed.
    public float m_fWalkSpeed;

    // public int for jumping force
    public int m_nForceConst = 500;

    // public float for the crouching height
    public float m_fCrouchHeight;

    // public gameobject for the crouch trigger
    public CrouchTrigger m_gCrouchTrigger;

    // public gameobject for the camera controller object
    public CameraController m_cCamera;
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private rigidbody
    private Rigidbody m_rbRigidBody;

    // private vector3 for the move direction
    private Vector3 m_v3MoveDirection;
    
    // private bool for if the player can jump
    private bool m_bJump;

    // private gameobject for the collider of the player
    private CapsuleCollider m_cPlayerCollider;

    // private bool for if the player can crouch
    private bool m_bCrouch;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Get the rigidbody component
        m_rbRigidBody = GetComponent<Rigidbody>();

        // Get the capsule collider of the player
        m_cPlayerCollider = GetComponent<CapsuleCollider>();

        // get the camera component
        m_cCamera = GetComponent<CameraController>();
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // Get the horizontal and vertical axis
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");

        // get the camera eulerAngles
        float y = Camera.main.transform.eulerAngles.y;
        float z = Camera.main.transform.eulerAngles.z;

        // Set rotation
        Vector3 Dir = new Vector3(0, y, z);
        transform.rotation = Quaternion.Euler(Dir);

        // Apply axis values to the move direction variable
        m_v3MoveDirection = (fHor * transform.right + fVer * transform.forward).normalized;
        
        // If ctrl or c is held
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            // if not crouching
            if (!m_bCrouch)
            {
                // Start crouch
                Crouch();
            }
        }
        else
        {
            // if coruching
            if (m_bCrouch)
            {
                // Stop coruching
                StopCrouching();
            }
        }

        // if space bar is pressed and the player is grounded
        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded())
        {
            // can jump bool is true
            m_bJump = true;
        }

        // new ray cast hit
        RaycastHit rhHitInfo;

        // if the left mouse is clicked
        if (Input.GetMouseButtonDown(0))
            {

            // Get the forward vector for the player
            Vector3 v3Forward = transform.TransformDirection(Vector3.forward);

            // if the forward vector collides with an object
            if (Physics.Raycast(transform.position, v3Forward, out rhHitInfo, 2))
            {
                //apply velocity to item if it is a rigidbody
                if (rhHitInfo.rigidbody)
                    rhHitInfo.rigidbody.velocity = v3Forward * 10;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // FixedUpdate: 
    //--------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // Run the movement function
        Movement();

        // Can the player jump?
        if (m_bJump)
        {
            // player cant jump
            m_bJump = false;

            // Add force to the player to jump
            m_rbRigidBody.AddForce(0, m_nForceConst, 0, ForceMode.Impulse);
        }
    }

    //--------------------------------------------------------------------------------------
    // Movement: 
    //--------------------------------------------------------------------------------------
    void Movement()
    {
        // update the player volocity by move direction, walkspeed and deltatime.
        m_rbRigidBody.AddForce(m_v3MoveDirection * m_fWalkSpeed, ForceMode.Acceleration);
    }

    //--------------------------------------------------------------------------------------
    // IsGrounded: Check if the player is on the ground.
    //
    // Return:
    //      bool: bool value for if the player is grounded or not.
    //--------------------------------------------------------------------------------------
    bool IsGrounded()
    {
        // Cast a ray down from the player at the ground
        Debug.Log("IsGrounded");
        Ray rRay = new Ray(transform.position - new Vector3(0, m_cPlayerCollider.height * 0.4f, 0), Vector3.down);
        RaycastHit rhHitInfo;

        // Set the layermask
        int nLayerMask = (LayerMask.GetMask("Ground"));

        // Is the ray colliding with the ground?
        if (Physics.Raycast(rRay, out rhHitInfo, 0.5f, nLayerMask))
        {
            // Return true and debug log the collider name
            Debug.Log(rhHitInfo.collider.name);
            return true;
        }
        
        // Draw the ray cast and print ray information in the console
        Debug.DrawRay(rRay.origin, Vector3.down);
        Debug.Log(rRay.origin.ToString() + " " + rRay.direction.ToString());
        
        // return false if not grounded
        return false;
    }

    //--------------------------------------------------------------------------------------
    // Crouch: Start the player crouching.
    //--------------------------------------------------------------------------------------
    void Crouch()
    {
        // Set the height and collider of the player for crouch
        m_cPlayerCollider.height -= m_fCrouchHeight;
        m_cPlayerCollider.center -= new Vector3(0, m_fCrouchHeight / 2, 0);

        // Set camera postion for crouch
        m_cCamera.m_v3Offset.y -= m_fCrouchHeight;

        // player is crouching 
        m_bCrouch = true;
    }

    //--------------------------------------------------------------------------------------
    // StopCrouching: Stop the player crouching.
    //--------------------------------------------------------------------------------------
    void StopCrouching()
    {
        // Can the player stand
        if (m_gCrouchTrigger.m_bCanStand)
        {
            // stop crouching and set player height back
            m_bCrouch = false;
            m_cPlayerCollider.height += m_fCrouchHeight;
            m_cPlayerCollider.center += new Vector3(0, m_fCrouchHeight / 2, 0);
            m_cCamera.m_v3Offset.y += m_fCrouchHeight;
        }
    }
}

// Tutorial used to get started with the player controller.
//https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie