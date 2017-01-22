using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LoadLevelObject : EventObject {
    public string level;

    public LoadLevelObject()
    {
        this._event = "load-level";
    }
}
