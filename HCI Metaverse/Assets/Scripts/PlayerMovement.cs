using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPun
{
    private Animator playerAnimator;

    private void Start()
    {
        

        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        /*if (!photonView.IsMine)
        {
            return;
        }*/

        Move();

    }

    private void Move()
    {
        float move = Input.GetAxis("Vertical");
        playerAnimator.SetFloat("inputValue_Walk", move);
    }
}
