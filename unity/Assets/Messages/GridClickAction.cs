using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClickAction : ActionObject {
    public float x;
    public float y;

    public GridClickAction()
    {
        this.level = "puzzle-map";
    }
}
