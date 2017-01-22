using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdater : MonoBehaviour {

    private Networker networker = null;
    public GameObject gman0;
    public GameObject gman1;


    bool hasStarted = false;

	// Use this for initialization
	void Start () {
    }

    public void onConnected(Networker networker)
    {
        this.networker = networker;
    }

    // Update is called once per frame
    void Update() {
        if(networker != null && !hasStarted){
            LoadMapLevelObject evt_loadMap = new LoadMapLevelObject();
            evt_loadMap._event = "load-level";
            evt_loadMap.level = "puzzle-map";
            evt_loadMap.x1 = (int)Mathf.Floor(gman0.transform.position.x / 6.0f)+1;
            evt_loadMap.y1 = (int)Mathf.Floor(gman0.transform.position.z / 6.0f)+1;
            evt_loadMap.x2 = (int)Mathf.Floor(gman1.transform.position.x / 6.0f)+1;
            evt_loadMap.y2 = (int)Mathf.Floor(gman1.transform.position.z / 6.0f)+1;
            networker.ws.SendString(evt_loadMap.getJSON());

            hasStarted = true;
        }
        if(hasStarted) {
            LoadMapLevelObject evt_loadMap = new LoadMapLevelObject();
            evt_loadMap.x1 = (int)Mathf.Floor(gman0.transform.position.x / 6.0f)+1;
            evt_loadMap.y1 = (int)Mathf.Floor(gman0.transform.position.z / 6.0f)+1;
            evt_loadMap.x2 = (int)Mathf.Floor(gman1.transform.position.x / 6.0f)+1;            
            evt_loadMap.y2 = (int)Mathf.Floor(gman1.transform.position.z / 6.0f)+1;
            networker.ws.SendString(evt_loadMap.getJSON());
        }
        
    }
}
