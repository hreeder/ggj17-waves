using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollisionHandler : MonoBehaviour {

    public Collider blade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void release()
    {
        Destroy(gameObject.GetComponent<FixedJoint>());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.GetComponent<FixedJoint>() == null && collision.contacts[0].thisCollider == blade)
        {
            FixedJoint fixJoint = this.gameObject.AddComponent<FixedJoint>();
            if(collision.collider.GetComponent<Rigidbody>() != null)
                fixJoint.connectedBody = collision.collider.gameObject.GetComponent<Rigidbody>();
            fixJoint.breakForce = 150;
        }
    }
}
