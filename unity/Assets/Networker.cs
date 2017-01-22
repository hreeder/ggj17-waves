using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Networker : MonoBehaviour {

    [System.Serializable]
    public class MyEventType : UnityEvent{}
    public MyEventType onConnected;

    public WebSocket ws;

	// Use this for initialization
	IEnumerator Start () {
        this.ws = new WebSocket(new Uri("ws://localhost:8888/ws/game"));
        yield return StartCoroutine(ws.Connect());

        EventObject evt = new EventObject();
        evt._event = "start-game";

        string evt_str = evt.getJSON();
        Debug.Log(evt_str);
        ws.SendString(evt_str);

        onConnected.Invoke();

        // Receiver Loop
        while (true)
        {
            string incoming = ws.RecvString();
            yield return 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
