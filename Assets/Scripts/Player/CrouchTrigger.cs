using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour {

    //[HideInInspector]
    public bool m_bCanStand;

	// Use this for initialization
	void Awake()
    {
        m_bCanStand = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        m_bCanStand = false;
    }

    private void OnTriggerExit(Collider other)
    {
        m_bCanStand = true;
    }
}