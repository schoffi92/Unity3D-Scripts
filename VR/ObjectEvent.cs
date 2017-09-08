using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VR {
    public class ObjectEvent : MonoBehaviour
    {

        public UnityEvent onVRClick;

        public UnityEvent onVRRaycastOver;

        public UnityEvent onVRRaycastEnter;

        public void Clicked()
        {
            onVRClick.Invoke();
        }

        public void RaycastEnter()
        {
            onVRRaycastEnter.Invoke();
        }

        public void RaycastOver()
        {
            onVRRaycastOver.Invoke();
        }
    }
}
