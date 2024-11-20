using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForPC : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.65f;
    [SerializeField] float rotSpeed = 3f;
    Vector3 moveDirection;

    [SerializeField] GameObject origin;

    PhotonView PV;

    public static float x, z;

    // Start is called before the first frame update
    private void Awake()
    {
        PV = transform.parent.GetComponent<PhotonView>();

        if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        x = Input.GetAxis("Horizontal_key");
        z = Input.GetAxis("Vertical_key");
        
            

        moveDirection = new Vector3(x, 0, z);

        origin.transform.Translate(moveDirection * (moveSpeed/10));

        float MouseX = Input.GetAxis("Mouse X");
        origin.transform.Rotate(Vector3.up * rotSpeed * MouseX);
    }
}
