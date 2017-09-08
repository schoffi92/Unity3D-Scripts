using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VR
{
     public class CameraFollowYRotation : MonoBehaviour {
        public Camera cam;
        public GameObject canvasParent;
        public float timeMultiplier = 0.05f;

        // Update is called once per frame
        void Update () {
            Vector3 rot = canvasParent.transform.eulerAngles;
            float min = Calc.Angle.GetMinRotation(rot.y, cam.transform.eulerAngles.y);
            rot.x = 0;
            rot.z = 0;

            rot.y += min * timeMultiplier * Time.deltaTime * 100f;

            canvasParent.transform.rotation = Quaternion.Euler( rot );
        }
      }
}
