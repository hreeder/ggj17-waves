using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
    }

	void OnTriggerEnter(Collider other) {
        if (other.CompareTag("pluggable"))
        {
            HingeJoint hinge = other.gameObject.AddComponent<HingeJoint>();
            hinge.breakForce = 100;
            hinge.connectedBody = GetComponent<Rigidbody>();
        }
    }

}
