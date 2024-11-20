using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPun
{
    private Animator playerAnimator;
    PhotonView PV;

    private void Start()
    {
        
        PV = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        Move();

    }

    private void Move()
    {
        float move_ver = 0;
        float move_hor = 0;
        if (Input.GetAxis("Vertical") != 0)
        {
            move_ver = Input.GetAxis("Vertical");
            playerAnimator.SetFloat("inputValue_Vertical", move_ver);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            move_hor = Input.GetAxis("Horizontal");
            playerAnimator.SetFloat("inputValue_Horizontal", move_hor);
        }

        if (Input.GetAxis("Vertical_key") != 0)
        {
            move_ver = Input.GetAxis("Vertical_key");
            playerAnimator.SetFloat("inputValue_Vertical", move_ver);
        }

        if (Input.GetAxis("Horizontal_key") != 0)
        {
            move_hor = Input.GetAxis("Horizontal_key");
            playerAnimator.SetFloat("inputValue_Horizontal", move_hor);
        }

        playerAnimator.SetFloat("inputValue_Vertical", move_ver);
        playerAnimator.SetFloat("inputValue_Horizontal", move_hor);
    }
}
