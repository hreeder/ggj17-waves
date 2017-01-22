using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CableHandler : MonoBehaviour {

    [System.Serializable]
    public class MyEventType : UnityEvent{}
    public MyEventType onConnect;
    public MyEventType onCut;

    private bool isCut = false;
    private GameObject connected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        connected.transform.position = this.GetComponent<BoxCollider>().center + this.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isCut)
        {
            if (other.CompareTag("weapon"))
            {
                Debug.Log(other.name);
                isCut = true;
                GameObject cableCut = this.transform.FindChild("Cable_Cut").gameObject;
                GameObject cableSolid = this.transform.FindChild("Cable_Solid").gameObject;

                cableCut.SetActive(isCut);
                cableSolid.SetActive(!isCut);
                onCut.Invoke();
            }
        }
        else
        {
            if (other.CompareTag("hacker"))
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.GetComponent<NewtonVR.NVRInteractableItem>().enabled = false;
                connected = other.gameObject;

                onConnect.Invoke();
            }
        }
    }
}
