using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

    public Camera target;

    void LateUpdate()
    {
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward,
                         target.transform.rotation * Vector3.up);
    }
}
