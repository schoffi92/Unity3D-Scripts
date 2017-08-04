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

	
	// Update is called once per frame
	void Update () {

        if (fpsCounterEnabled)
        {
            fpsCounterTime += Time.deltaTime;
            fpsCounterValue++;

            if (fpsCounterTime >= fpsCounterTimeMax)
            {
                fpsCounterText.text = (Mathf.Floor(fpsCounterValue / fpsCounterTimeMax)).ToString();

                fpsCounterTime -= fpsCounterTimeMax;
                fpsCounterValue = 0f;
            }
        }

    }
}
