using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour {

	public Transform target;
	private NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		nav = gameObject.AddComponent<NavMeshAgent>();
		nav.speed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
			nav.SetDestination(target.position);
	}
}
