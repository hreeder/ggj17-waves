using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveformActionObject : LoadLevelObject{
    public float amplitude;
    public float frequency;
    public float phase;

    public WaveformActionObject()
    {
        this._event = "action";
    }
}
