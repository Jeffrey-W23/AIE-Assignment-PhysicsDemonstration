// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// CrouchTrigger object. Inheriting from MonoBehaviour. Script for checking if the 
// player can stand
//--------------------------------------------------------------------------------------
public class CrouchTrigger : MonoBehaviour {

    //public bool for if the player can stand
    public bool m_bCanStand;

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Set default values
        m_bCanStand = true;
	}

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update ()
    {
		
	}

    //--------------------------------------------------------------------------------------
    // OnTriggerEnter: Unity on Trigger enter function.
    //
    // Param:
    //      cOther: Collider value for collision information of the trigger object
    //--------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider cOther)
    {
        // set can stand to false
        m_bCanStand = false;
    }

    //--------------------------------------------------------------------------------------
    // OnTriggerExit: Unity on Trigger exit function.
    //
    // Param:
    //      cOther: Collider value for collision information of the trigger object 
    //--------------------------------------------------------------------------------------
    private void OnTriggerExit(Collider cOther)
    {
        // Set can stand to true
        m_bCanStand = true;
    }
}