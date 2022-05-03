using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y - 1.65f), Camera.main.transform.position.z);

        Quaternion quaternion = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

        transform.SetPositionAndRotation(position, quaternion);
    }
}
