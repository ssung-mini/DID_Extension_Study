using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTracking : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.forward = Vector3.ProjectOnPlane(-(Camera.main.transform.position - transform.position), Vector3.up).normalized;
    }
}
