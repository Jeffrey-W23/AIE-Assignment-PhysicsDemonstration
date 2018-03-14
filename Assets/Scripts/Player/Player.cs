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
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private rigidbody
    private Rigidbody m_rbRigidBody;

    // private vector3 for the move direction
    private Vector3 m_v3MoveDirection;







    private bool m_bJump;
    public int forceConst = 500;
    private CapsuleCollider m_cPlayerCollider;
    private bool m_bCrouch;
    public float m_fCrouchHeight;
    public CrouchTrigger m_gCrouchTrigger;
    public CameraController m_cCamera;
    


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






        //
        float y = Camera.main.transform.eulerAngles.y;
        float z = Camera.main.transform.eulerAngles.z;

        //
        Vector3 Dir = new Vector3(0, y, z);
        transform.rotation = Quaternion.Euler(Dir);






        // Apply axis values to the move direction variable
        m_v3MoveDirection = (fHor * transform.right + fVer * transform.forward).normalized;






        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            if (!m_bCrouch)
            {
                Crouch();
                //m_gCrouchTrigger.GetComponent<SphereCollider>().isTrigger = true;
            }
        }
        else
        {
            if (m_bCrouch)
            {
                StopCrouching();
                //m_gCrouchTrigger.GetComponent<SphereCollider>().isTrigger = false;
            }
        }





        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded())
        {
            m_bJump = true;
        }




        
        RaycastHit rhHitInfo;

        if (Input.GetMouseButtonDown(0))
            {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out rhHitInfo, 2))
            {
                //print("There is something in front of the object!");
                if (rhHitInfo.rigidbody)
                    rhHitInfo.rigidbody.velocity = fwd * 10;
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


        if (m_bJump)
        {
            m_bJump = false;
            m_rbRigidBody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }
    }

    //--------------------------------------------------------------------------------------
    // Movement: 
    //--------------------------------------------------------------------------------------
    void Movement()
    {




        //Vector3 v3VelFix = new Vector3(0, m_rbRigidBody.velocity.y, 0);



        // update the player volocity by move direction, walkspeed and deltatime.
        m_rbRigidBody.AddForce(m_v3MoveDirection * m_fWalkSpeed, ForceMode.Acceleration);




        //m_rbRigidBody.velocity += v3VelFix;
    }

    bool IsGrounded()
    {

        Debug.Log("IsGrounded");
        Ray rRay = new Ray(transform.position - new Vector3(0, m_cPlayerCollider.height * 0.4f, 0), Vector3.down);
        RaycastHit rhHitInfo;

        int layerMask = (LayerMask.GetMask("Ground"));

        if (Physics.Raycast(rRay, out rhHitInfo, 0.5f, layerMask))
        {
            Debug.Log(rhHitInfo.collider.name);
            return true;
        }
        Debug.DrawRay(rRay.origin, Vector3.down);
        Debug.Log(rRay.origin.ToString() + " " + rRay.direction.ToString());
        return false;
    }






    void Crouch()
    {
        m_cPlayerCollider.height -= m_fCrouchHeight;
        m_cPlayerCollider.center -= new Vector3(0, m_fCrouchHeight / 2, 0);

        m_cCamera.offset.y -= m_fCrouchHeight;

        m_bCrouch = true;
    }

    void StopCrouching()
    {
        if (m_gCrouchTrigger.m_bCanStand)
        {
            m_bCrouch = false;
            m_cPlayerCollider.height += m_fCrouchHeight;
            m_cPlayerCollider.center += new Vector3(0, m_fCrouchHeight / 2, 0);
            m_cCamera.offset.y += m_fCrouchHeight;
        }
    }
}



//https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie