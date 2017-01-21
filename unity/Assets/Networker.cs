using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Networker : MonoBehaviour {
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

        Thread.Sleep(1000);

        WaveformActionObject evt_pluggedin = new WaveformActionObject();
        evt_pluggedin._event = "load-level";
        evt_pluggedin.level = "puzzle-entry";
        evt_pluggedin.amplitude = 5.0f;
        evt_pluggedin.frequency = 5.0f;
        evt_pluggedin.phase = 5.0f;
        ws.SendString(evt_pluggedin.getJSON());

        Thread.Sleep(1000);

        WaveformActionObject evt_update = new WaveformActionObject();
        evt_update.level = "puzzle-entry-wave";
        evt_update.amplitude = 5.0f;
        evt_update.frequency = 5.0f;
        evt_update.phase = 5.0f;
        ws.SendString(evt_update.getJSON());

        Thread.Sleep(1000);

        WaveformActionObject evt_correct = new WaveformActionObject();
        evt_correct.level = "puzzle-entry-correct";
        evt_correct.amplitude = 5.0f;
        evt_correct.frequency = 5.0f;
        evt_correct.phase = 5.0f;
        ws.SendString(evt_correct.getJSON());

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
