using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 2.0f;
    public float horizontalSpeed = 1.0f;
    

    private Animator playerAnimator;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*if (!photonView.IsMine)
        {
            return;
        }*/

        Rotate();

        Move();

    }

    private void Move()
    {
        float move = Input.GetAxis("Vertical");

        Vector3 moveDistance = move * transform.forward * moveSpeed * Time.deltaTime;
        
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        playerAnimator.SetFloat("inputValue_Walk", move);
    }

    private void Rotate()
    {
        float rotate = Input.GetAxis("Mouse X");

        float turn = rotate * horizontalSpeed;

        transform.Rotate(0, turn, 0);
    }
}
