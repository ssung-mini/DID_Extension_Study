using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private GameObject NPC_1;
    private GameObject NPC_2;
    private GameObject NPC_3;
    private GameObject NPC_4;

    public void settingNPC()
    {
        NPC_1 = GameObject.FindGameObjectWithTag("NPC_1");
        NPC_2 = GameObject.FindGameObjectWithTag("NPC_2");
        NPC_3 = GameObject.FindGameObjectWithTag("NPC_3");
        NPC_4 = GameObject.FindGameObjectWithTag("NPC_4");
    }

    public void animationNPC()
    {
        if (transform.position.z < NPC_1.transform.position.z - 1.5f && transform.position.z >= NPC_1.transform.position.z - 2.5f  &&
            transform.position.x < NPC_1.transform.position.x + 1 && transform.position.x >= NPC_1.transform.position.x - 1)
        {

            NPC_1.GetComponent<Animator>().SetBool("NPC_W_Anim", true);
            NPC_1.GetComponent<AudioSource>().Play();
        }

        else if (transform.position.z < NPC_2.transform.position.z - 1.5f && transform.position.z >= NPC_2.transform.position.z - 2.5f &&
            transform.position.x < NPC_2.transform.position.x + 1 && transform.position.x >= NPC_2.transform.position.x - 1)
        {

            NPC_2.GetComponent<Animator>().SetBool("NPC_M_Anim", true);
            NPC_2.GetComponent<AudioSource>().Play();
        }

        else if (transform.position.z > NPC_3.transform.position.z + 1.5f && transform.position.z <= NPC_3.transform.position.z + 2.5f &&
            transform.position.x < NPC_3.transform.position.x + 1 && transform.position.x >= NPC_3.transform.position.x - 1)
        {

            NPC_3.GetComponent<Animator>().SetBool("NPC_W_Anim", true);
            NPC_3.GetComponent<AudioSource>().Play();
        }

        else if (transform.position.z > NPC_4.transform.position.z + 1.5f && transform.position.z <= NPC_4.transform.position.z + 2.5f &&
            transform.position.x < NPC_4.transform.position.x + 1 && transform.position.x >= NPC_4.transform.position.x - 1)
        {

            NPC_4.GetComponent<Animator>().SetBool("NPC_M_Anim", true);
            NPC_4.GetComponent<AudioSource>().Play();
        }

        else
        {
            NPC_1.GetComponent<Animator>().SetBool("NPC_W_Anim", false);
            NPC_2.GetComponent<Animator>().SetBool("NPC_M_Anim", false);
            NPC_3.GetComponent<Animator>().SetBool("NPC_W_Anim", false);
            NPC_4.GetComponent<Animator>().SetBool("NPC_M_Anim", false);

            NPC_1.GetComponent<AudioSource>().Stop();
            NPC_2.GetComponent<AudioSource>().Stop();
            NPC_3.GetComponent<AudioSource>().Stop();
            NPC_4.GetComponent<AudioSource>().Stop();
        }
    }
}
