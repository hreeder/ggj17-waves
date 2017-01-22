using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
            if (incoming != null)
            {
                EventObject incEvt = JsonUtility.FromJson<EventObject>(incoming);
                
                switch (incEvt._event)
                {
                    case "action":
                        // Deserialize the entire action
                        HandleIncomingAction(incoming);
                        break;
                    case "play-sound":
                        PlaySoundObject playSound = JsonUtility.FromJson<PlaySoundObject>(incoming);
                        // trigger playSound.file
                        break;
                    default:
                        Debug.Log("Received Unknown Event via WebSocket - " + incEvt._event);
                        break;
                }
            }
            yield return 0;
        }
        ws.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void HandleIncomingAction (string message) {
        ActionObject incAction = JsonUtility.FromJson<ActionObject>(message);
        switch (incAction.level)
        {
            case "puzzle-map-click":
                GridClickAction gca = JsonUtility.FromJson<GridClickAction>(message);
                Debug.Log("Grid clicked at " + gca.x + ", " + gca.y);
                GameObject target = GameObject.Find("Target");
                if (target != null)
                {
                    target.transform.position = new Vector3(gca.x, 1, gca.y);

                    GameObject gman0 = GameObject.Find("Gman");
                    gman0.GetComponent<EnemyNav>().target = target.transform;
                    GameObject gman1 = GameObject.Find("Gman (1)");
                    gman1.GetComponent<EnemyNav>().target = target.transform;
                }
                break;
            default:
                Debug.Log("Received Unknown Action - " + incAction.level);
                break;
        }
    }
}
