using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TESTNAV : MonoBehaviour {

    public Transform target;

    private NavMeshAgent nav;

	// Use this for initialization
	void Start () {
        nav = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(target.position);
	}
}
