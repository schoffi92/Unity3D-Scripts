using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VR
{
    public class CameraEvent : MonoBehaviour
    {

        public const string VRINTERACTIVE_TAG = "VRInteractive";
        public Camera camera;

        public string objName;
        protected ObjectEvent objEvent;

        public float maxDistance = 100f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {

                if (hit.collider.tag == VRINTERACTIVE_TAG)
                {
                    ObjectEvent e;

                    if (objName != hit.collider.gameObject.name)
                    {
                        e = hit.collider.GetComponent<ObjectEvent>();
                    }
                    else
                    {
                        e = objEvent;
                    }

                    if (e != null)
                    {
                        if (objName != hit.collider.gameObject.name)
                        {
                            //Debug.Log("Raycast Over: '" + objName + "'");
                            RaycastOver();

                            //Debug.Log("Raycast Enter: '" + hit.collider.gameObject.name + "'");
                            e.RaycastEnter();
                            objEvent = e;
                            objName = hit.collider.gameObject.name;
                        }

                        if (Input.GetMouseButtonDown(0))
                        {
                            //Debug.Log("Clicked: '" + hit.collider.gameObject.name + "' == '" + objName + "'");
                            e.Clicked();
                        }


                    }
                }
                else
                {
                    RaycastOver();
                }
            }
            else
            {
                RaycastOver();
            }

        }

        void RaycastOver()
        {
            objName = "";

            if (objEvent != null)
            {
                objEvent.RaycastOver();
                objEvent = null;
            }
        }

        public ObjectEvent GetActiveObject()
        {
            return objEvent;
        }
    }
}
