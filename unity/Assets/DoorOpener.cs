using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    public Transform door0;
    private Vector3 door0Start;

    public Transform door1;
    private Vector3 door1Start;

    private bool isOpen = false;
    private bool moving = false;
    private float timeDelta = 0.0f;


    private NewtonVR.NVRButton inputInfo;

    // Use this for initialization
    void Start () {
        door0Start = door0.localPosition;
        door1Start = door1.localPosition;

        inputInfo = this.GetComponent<NewtonVR.NVRButton>();
    }

    public void toggle()
    {
        if (isOpen)
        {
            close();
        }
        else
        {
            open();
        }
    }

    void open()
    {
        moving = true;
        isOpen = true;
        timeDelta = 0.0f;
    }
	
    void close()
    {
        isOpen = false;
        moving = true;
        timeDelta = 1.0f;
    }

	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (isOpen)
            {
                door0.localPosition = door0Start - door0.right * (Mathf.Sin(timeDelta) * 0.3f + 0.3f);
                door1.localPosition = door1Start - door1.right * (Mathf.Sin(timeDelta) * 0.3f + 0.3f);
                timeDelta += Time.deltaTime;

                if (timeDelta >= 1.0f)
                    moving = false;
            }
            else
            {
                door0.position = door0Start - door0.right * (Mathf.Sin(timeDelta) * 0.3f + 0.3f);
                door1.position = door1Start - door1.right * (Mathf.Sin(timeDelta) * 0.3f + 0.3f);
                timeDelta -= Time.deltaTime;

                if (timeDelta <= 0.0f)
                    moving = false;
            }
        }
    }
}
