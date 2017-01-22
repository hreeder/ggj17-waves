using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class WavePuzzle : MonoBehaviour {


    [System.Serializable]
    public class MyEventType : UnityEvent{}
    public MyEventType onCompleted;


    public Transform snap0;
    public Transform snap1;

    public float amplitude = 0.0f;
    public float frequency = 0.0f;
    public float phase = 0.0f;

    private bool isComplete = false;

    private Networker networker;

    public bool allowUpdate = false;


    private enum SLIDER
    {
        AMPLITUDE = 0,
        FREQUENCY = 1,
        PHASE = 2
    }

    struct SliderInfo
    {
        public bool flipped;
        public float min;
        public float max;

        public SliderInfo(bool flipped, float min, float max)
        {
            this.flipped = flipped;
            this.min = min;
            this.max = max;
        }
    }

    private SliderInfo[] sliderInfo = new SliderInfo[3];

    private NewtonVR.NVRSlider ampSlider;
    private NewtonVR.NVRSlider freSlider;
    private NewtonVR.NVRSlider phaSlider;

    public float correctAmplitude = 1.0f;
    public float correctFrequency = 1.0f;
    public float correctPhase = 4.0f;

    private bool startedLevel;

    private int sliderUnused = 0;

    void Awake(){        

        sliderInfo[0] = new SliderInfo(false, 1.0f, 8.0f); // Amplitude
        sliderInfo[1] = new SliderInfo(false, 1.0f, 8.0f); // Frequency
        sliderInfo[2] = new SliderInfo(false, 1.0f, 8.0f); // Phase
        
        correctAmplitude = Mathf.Round(Random.Range(sliderInfo[0].min, sliderInfo[0].max) * 10.0f) / 10.0f;
        correctFrequency = Mathf.Round(Random.Range(sliderInfo[1].min, sliderInfo[1].max) * 10.0f) / 10.0f;
        correctPhase = Mathf.Round(Random.Range(sliderInfo[2].min, sliderInfo[2].max) * 10.0f) / 10.0f;

        sliderUnused = 2;//Random.Range(0, 3);
        switch(sliderUnused){
            case 0:
                this.transform.FindChild("Amplitude").gameObject.SetActive(false);
                amplitude = correctAmplitude;
                this.transform.FindChild("Frequency").position = snap0.transform.position;
                this.transform.FindChild("Phase").position = snap1.transform.position;

                break;
            case 1:
                this.transform.FindChild("Frequency").gameObject.SetActive(false);
                frequency = correctFrequency;
                this.transform.FindChild("Phase").position = snap0.transform.position;
                this.transform.FindChild("Amplitude").position = snap1.transform.position;
                break;
            case 2:
                this.transform.FindChild("Phase").gameObject.SetActive(false);
                phase = correctPhase;
                this.transform.FindChild("Amplitude").position = snap0.transform.position;
                this.transform.FindChild("Frequency").position = snap1.transform.position;
                break;
        }

    }

    // Use this for initialization
    void Start () {
        startedLevel = false;

        ampSlider = this.transform.FindChild("Amplitude").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();
        freSlider = this.transform.FindChild("Frequency").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();
        phaSlider = this.transform.FindChild("Phase").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();


        for (int i = 0; i < sliderInfo.Length; i++)
        {
            sliderInfo[i].flipped = Random.Range(0, 100) < 50;
        }

    }

    public void networkerConnected(Networker networker){
        this.networker = networker;

        WaveformActionObject evt_pluggedin = new WaveformActionObject();
        evt_pluggedin._event = "load-level";
        evt_pluggedin.level = "puzzle-entry";
        evt_pluggedin.amplitude = this.correctAmplitude;
        evt_pluggedin.frequency = this.correctFrequency;
        evt_pluggedin.phase = this.correctFrequency;
        networker.ws.SendString(evt_pluggedin.getJSON());
    }

    public void tellHacker()
    {
        if(networker == null || isComplete || !allowUpdate)
            return;

        WaveformActionObject evt_update = new WaveformActionObject();
        evt_update.level = "puzzle-entry-wave";
        evt_update.amplitude = this.amplitude;
        evt_update.frequency = this.frequency;
        evt_update.phase = this.phase;
        networker.ws.SendString(evt_update.getJSON());


        if (Mathf.Abs(this.amplitude - this.correctAmplitude)/correctAmplitude < 0.2f
            && Mathf.Abs(this.frequency - this.correctFrequency) / correctFrequency < 0.2f
            && Mathf.Abs(this.phase - this.correctPhase) / correctPhase < 0.2f)
        {
            WaveformActionObject evt_correct = new WaveformActionObject();
            evt_correct.level = "puzzle-entry-correct";
            evt_correct.amplitude = this.correctAmplitude;
            evt_correct.frequency = this.correctFrequency;
            evt_correct.phase = this.correctPhase;
            networker.ws.SendString(evt_correct.getJSON());

            // End puzzle
            this.isComplete = true;

            onCompleted.Invoke();

            int numChildren = this.gameObject.transform.childCount;
            for(int i = 0; i < numChildren; i++){
                Transform child = this.gameObject.transform.GetChild(i);
                if(child.gameObject.activeSelf){
                    Transform slider = child.Find("Slider");
                    if(slider != null){
                        Rigidbody rb = slider.GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.FreezeAll;
                    }
                }
            }
        }
        
    }

    private float sliderVal(SLIDER s)
    {
        float result = 0.0f;
        switch (s)
        {
            case SLIDER.AMPLITUDE:
                if(sliderUnused == 0)
                    return correctAmplitude;
                result = sliderInfo[0].min + (sliderInfo[0].max - sliderInfo[0].min) * Mathf.Clamp((sliderInfo[0].flipped ? (1 - ampSlider.CurrentValue) : ampSlider.CurrentValue), 0.0f, 1.0f);
                result = Mathf.Round(result * 10.0f) / 10.0f;
                return result;
            case SLIDER.FREQUENCY:
                if(sliderUnused == 1)
                    return correctFrequency;
                result = sliderInfo[1].min + (sliderInfo[1].max - sliderInfo[1].min) * Mathf.Clamp((sliderInfo[1].flipped ? (1 - freSlider.CurrentValue) : freSlider.CurrentValue), 0.0f, 1.0f);
                result = Mathf.Round(result * 10.0f) / 10.0f;
                return result;
            case SLIDER.PHASE:
                if(sliderUnused == 2)
                    return correctPhase;
                result = sliderInfo[2].min + (sliderInfo[2].max - sliderInfo[2].min) * Mathf.Clamp((sliderInfo[2].flipped ? (1 - phaSlider.CurrentValue) : phaSlider.CurrentValue), 0.0f, 1.0f);
                result = Mathf.Round(result * 10.0f) / 10.0f;
                return result;
        }
        return 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        amplitude = sliderVal(SLIDER.AMPLITUDE);
        frequency = sliderVal(SLIDER.FREQUENCY);
        phase = sliderVal(SLIDER.PHASE);
        tellHacker();
    }

    void OnTriggerStay(Collider collider){
        if(collider.name.Equals("NVRPlayer")){
            allowUpdate = true;
            if(networker != null){
                WaveformActionObject evt_pluggedin = new WaveformActionObject();
                evt_pluggedin._event = "load-level";
                evt_pluggedin.level = "puzzle-entry";
                evt_pluggedin.amplitude = this.correctAmplitude;
                evt_pluggedin.frequency = this.correctFrequency;
                evt_pluggedin.phase = this.correctFrequency;
                networker.ws.SendString(evt_pluggedin.getJSON());
            }
        }
    }

    void OnTriggerExit(Collider collider){
        if(collider.name.Equals("NVRPlayer")){
            allowUpdate = false;
        }
    }

}
