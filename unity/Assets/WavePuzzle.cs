using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WavePuzzle : MonoBehaviour {


    public float amplitude = 0.0f;
    public float frequency = 0.0f;
    public float phase = 0.0f;

    private bool isComplete = false;

    private Networker networker;
    

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

    private float correctAmplitude = 1.0f;
    private float correctFrequency = 1.0f;
    private float correctPhase = 4.0f;

    private bool startedLevel;

    // Use this for initialization
    void Start () {
        startedLevel = false;

        ampSlider = this.transform.FindChild("Amplitude").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();
        freSlider = this.transform.FindChild("Frequency").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();
        phaSlider = this.transform.FindChild("Phase").FindChild("Slider").GetComponent<NewtonVR.NVRSlider>();

        sliderInfo[0] = new SliderInfo(false, 1.0f, 4.0f); // Amplitude
        sliderInfo[1] = new SliderInfo(false, 10.0f, 100.0f); // Frequency
        sliderInfo[2] = new SliderInfo(false, 1.0f, 4.0f); // Phase

        correctAmplitude = Random.Range(sliderInfo[0].min * 1.1f, sliderInfo[0].max * 0.9f);
        correctFrequency = Random.Range(sliderInfo[1].min * 1.1f, sliderInfo[1].max * 0.9f);
        correctPhase = Random.Range(sliderInfo[2].min * 1.1f, sliderInfo[2].max * 0.9f);

        // networker = this.gameObject.transform.Find("Networker").GetComponent<Networker>();

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
        if(networker == null || isComplete)
            return;

        WaveformActionObject evt_update = new WaveformActionObject();
        evt_update.level = "puzzle-entry-wave";
        evt_update.amplitude = this.amplitude;
        evt_update.frequency = this.frequency;
        evt_update.phase = this.phase;
        networker.ws.SendString(evt_update.getJSON());



        if (Mathf.Abs(this.amplitude - this.correctAmplitude)/correctAmplitude < 0.1f
            && Mathf.Abs(this.frequency - this.correctFrequency) / correctFrequency < 0.1f
            && Mathf.Abs(this.phase - this.correctPhase) / correctPhase < 0.1f)
        {
            WaveformActionObject evt_correct = new WaveformActionObject();
            evt_correct.level = "puzzle-entry-correct";
            evt_correct.amplitude = this.correctAmplitude;
            evt_correct.frequency = this.correctFrequency;
            evt_correct.phase = this.correctPhase;
            networker.ws.SendString(evt_correct.getJSON());

            this.isComplete = true;
            int numChildren = this.gameObject.transform.childCount;
            for(int i = 0; i < numChildren; i++){
                Transform child = this.gameObject.transform.GetChild(i);
                Transform slider = child.Find("Slider");
                Rigidbody rb = slider.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        
    }

    private float sliderVal(SLIDER s)
    {
        switch (s)
        {
            case SLIDER.AMPLITUDE:
                return sliderInfo[0].min + (sliderInfo[0].max - sliderInfo[0].min) * Mathf.Clamp((sliderInfo[0].flipped ? (1 - ampSlider.CurrentValue) : ampSlider.CurrentValue), 0.0f, 1.0f);
            case SLIDER.FREQUENCY:
                return sliderInfo[1].min + (sliderInfo[1].max - sliderInfo[1].min) * Mathf.Clamp((sliderInfo[1].flipped ? (1 - freSlider.CurrentValue) : freSlider.CurrentValue), 0.0f, 1.0f);
            case SLIDER.PHASE:
                return sliderInfo[2].min + (sliderInfo[2].max - sliderInfo[2].min) * Mathf.Clamp((sliderInfo[2].flipped ? (1 - phaSlider.CurrentValue) : phaSlider.CurrentValue), 0.0f, 1.0f);
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
}
