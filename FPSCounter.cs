using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    // FPS Counter
    public bool fpsCounterEnabled = false;

    public float fpsCounterTime = 0f;

    public float fpsCounterTimeMax = 1f;

    public float fpsCounterValue = 0f;

    public Text fpsCounterText;

    /**
     * Variables For Low FPS Event
     */
    public float minFPSValue = 60f;

    public delegate void FPSDropDown(float fps);
    public event FPSDropDown OnFPSDropDown;

	
	// Update is called once per frame
	void Update () {

        if (fpsCounterEnabled)
        {
            fpsCounterTime += Time.deltaTime;
            fpsCounterValue++;

            if (fpsCounterTime >= fpsCounterTimeMax)
            {
                float fps = (Mathf.Floor(fpsCounterValue / fpsCounterTimeMax));

                if (fpsCounterText != null)
                {
                    fpsCounterText.text = fps.ToString();
                }

                // Checks Low FPS
                if (minFPSValue > fps && OnFPSDropDown != null)
                {
                    OnFPSDropDown(fps);
                }

                fpsCounterTime -= fpsCounterTimeMax;
                fpsCounterValue = 0f;
            }
        }

    }
}
