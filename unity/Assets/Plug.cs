using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

	private GameObject attatched;

	// Use this for initialization
	void Start () {
		
	}
	
    public void unplug()
    {
        Rigidbody r = attatched.GetComponent<Rigidbody>();
        r.AddForce(2, 2, 2);
        attatched = null;
    }

	// Update is called once per frame
	void Update () {
        if(attatched != null){
			attatched.transform.position = this.transform.position;
		}
    }

	void OnTriggerEnter(Collider other) {
        if (other.CompareTag("pluggable"))
        {
            attatched = other.gameObject;
        }
        //Destroy(other.gameObject);
    }

}
