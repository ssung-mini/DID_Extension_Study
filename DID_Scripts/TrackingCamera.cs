using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TrackingCamera : MonoBehaviour
{
    PhotonView PV;
    // Update is called once per frame

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }
        //Vector3 position = new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y - 1.65f), (Camera.main.transform.position.z));

        Quaternion quaternion = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

        transform.rotation = quaternion;
        
        //transform.SetPositionAndRotation(position, quaternion);
    }
}
