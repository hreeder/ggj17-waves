using UnityEngine;
using System.Collections;
using System;

public class EchoTest : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		WebSocket w = new WebSocket(new Uri("ws://localhost:8888/ws/game"));
		yield return StartCoroutine(w.Connect());
        MessageObject e = new MessageObject();
        e.current_level = "intro";
        e.message = "connected";
		w.SendString(JsonUtility.ToJson(e));

		int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				Debug.Log ("Received: "+reply);
				//w.SendString("Hi there"+i++);
			}
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}
}
