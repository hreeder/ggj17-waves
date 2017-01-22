using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdater : MonoBehaviour {

    private Networker networker;
    public GameObject gman0;
    public GameObject gman1;

    private int step = 0;

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
        if(networker != null && hasStarted){
            LoadMapLevelObject evt_loadMap = new LoadMapLevelObject();
            evt_loadMap._event = "load-level";
            evt_loadMap.level = "puzzle-map";
            evt_loadMap.x1 = (int)(gman0.transform.position.x / 6);
            evt_loadMap.y1 = (int)(gman0.transform.position.z / 6);
            evt_loadMap.x2 = (int)(gman1.transform.position.x / 6);
            evt_loadMap.y2 = (int)(gman1.transform.position.z / 6);
            networker.ws.SendString(evt_loadMap.getJSON());

            hasStarted = true;
        }

        if(step % 10 == 0) {
            LoadMapLevelObject evt_loadMap = new LoadMapLevelObject();
            evt_loadMap.x1 = (int)(gman0.transform.position.x / 6);
            evt_loadMap.y1 = (int)(gman0.transform.position.z / 6);
            evt_loadMap.x2 = (int)(gman1.transform.position.x / 6);
            evt_loadMap.y2 = (int)(gman1.transform.position.z / 6);
            networker.ws.SendString(evt_loadMap.getJSON());

            step = 1;
        }
        step++;
    }
}
