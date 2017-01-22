using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapLevelObject : LoadLevelObject {
    public int x1, y1, x2, y2;

    public LoadMapLevelObject()
    {
        this.level = "puzzle-map";
    }
}
