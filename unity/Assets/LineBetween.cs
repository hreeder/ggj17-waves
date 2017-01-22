using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetween : MonoBehaviour {

	public Transform start, end;
	private LineRenderer line;


	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer>();
		line.SetPosition(0, start.position);
		line.SetPosition(1, end.position);
	}
	
	// Update is called once per frame
	void Update () {
		line.SetPosition(0, start.position);
		line.SetPosition(1, end.position);
		
	}
}
