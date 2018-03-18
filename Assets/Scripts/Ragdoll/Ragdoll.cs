// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Ragdoll object. Inheriting from MonoBehaviour Animator the ragdoll object
//--------------------------------------------------------------------------------------
[RequireComponent(typeof(Animator))] // Require animator component
public class Ragdoll : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // public array of rigidbodies
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    // public getter and setter for if the Ragdoll state
    public bool RagdollOn
    {
        get
        {
            // return animator state
            return !m_aAnimator.enabled;
        }
        set
        {
            // set animate state
            m_aAnimator.enabled = !value;

            // set iskinematic for each rigidbody
            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;
        }
    }
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private Animator object for the animator of the ragdoll
    private Animator m_aAnimator = null;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Start ()
    {
        // get animator component
        m_aAnimator = GetComponent<Animator>();

        //
        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
	}

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update ()
    {
         
	}
        
}
