using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    public const string PLAYERPREF_FPS_KEY = "dbg_fps";
	
    // FPS Counter
	public bool loadFPSSettings = true;
	
    public bool fpsCounterEnabled = true;
	
	public bool fpsDisplayEnabled = false;

    public float fpsCounterTime = 0f;

    public float fpsCounterTimeMax = 1f;

    public float fpsCounterValue = 0f;

    public Text fpsCounterText;

    /**
     * Variables For Low FPS Event
     */
    public float minFPSValue = 60f;

    public UnityEvent onFPSDropDown;

    private void Start()
    {
		if (fpsCounterText != null)
		{
			fpsCounterText.text = "";
		}
		
		if (loadFPSSettings && PlayerPrefs.HasKey(PLAYERPREF_FPS_KEY))
		{
			fpsDisplayEnabled = (PlayerPrefs.GetInt(PLAYERPREF_FPS_KEY) == 1);
		}
    }

    public void ToggleFPSDisplay()
    {
		PlayerPrefs.SetInt(PLAYERPREF_FPS_KEY, (!fpsDisplayEnabled ? 1 : 0));
        fpsDisplayEnabled = !fpsDisplayEnabled;
		
		if (fpsCounterText != null)
		{
        	fpsCounterText.text = "";
		}
    }

    // Update is called once per frame
    void Update () {

        if (fpsCounterEnabled)
        {
            fpsCounterTime += Time.deltaTime;
            fpsCounterValue++;

            if (fpsCounterTime >= fpsCounterTimeMax)
            {
                float fps = (Mathf.Floor(fpsCounterValue / fpsCounterTimeMax));

                if (fpsCounterText != null && fpsDisplayEnabled)
                {
                    fpsCounterText.text = fps.ToString();
                }

                // Checks Low FPS
                if (minFPSValue > fps)
                {
                    onFPSDropDown.Invoke();
                }

                fpsCounterTime -= fpsCounterTimeMax;
                fpsCounterValue = 0f;
            }
        }

    }
}
