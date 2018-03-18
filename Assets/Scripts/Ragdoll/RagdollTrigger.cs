// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// RagdollTrigger object. Inheriting from MonoBehaviour. The Ragdoll trigger
//--------------------------------------------------------------------------------------
public class RagdollTrigger : MonoBehaviour {

    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    void Start ()
    {
		
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
    void OnTriggerEnter(Collider cOther)
    {
        // Get ragdoll component
        Ragdoll rRagdoll = cOther.gameObject.GetComponentInParent<Ragdoll>();

        // if not null turn on the ragdoll
        if (rRagdoll != null)
            rRagdoll.RagdollOn = true;
    }
}
