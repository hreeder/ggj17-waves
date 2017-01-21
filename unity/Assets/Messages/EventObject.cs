using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventObject {
    public string _event;

    public string getJSON()
    {
        return JsonUtility.ToJson(this).Replace("_event", "event");
    }
}