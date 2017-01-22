using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundObject : EventObject {
    public string file;

    public PlaySoundObject()
    {
        this._event = "play-sound";
    }
}
