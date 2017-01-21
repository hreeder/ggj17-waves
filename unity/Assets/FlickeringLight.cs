using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    private Light light;

	// Use this for initialization
	void Start () {
        light = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        light.intensity = Mathf.Clamp(light.intensity + Random.Range(-0.01f, 0.01f), 0.9f, 1.2f);
	}
}
