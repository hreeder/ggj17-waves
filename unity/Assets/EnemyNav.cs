using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using UnityEngine.SceneManagement;

public class EnemyNav : MonoBehaviour {

	public Transform target;
	public NavMeshAgent nav;
	public GameObject death;

	// Use this for initialization
	void Start () {
        if (nav == null) {
            nav = gameObject.AddComponent<NavMeshAgent>();
            nav.speed = 0.25f;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(target != null)
			nav.SetDestination(target.position);
    }

	void OnTriggerEnter(Collider collider){
		if(collider.name.Equals("Head")){
			death.SetActive(true);
			SceneManager.LoadScene("FirstRoom");
		}
	}

}
