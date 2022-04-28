using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public enum AvatarType
{
    Generic_Avatar = 0,
    NFT_Avatar = 1
}

public class ProgramManager : MonoBehaviourPunCallbacks
{

    /* Player Info Panel */
    public GameObject playerInfoPanel;

    /* Player Prefab */
    public GameObject player;

    /* NPC Prefab */
    //public GameObject[] NFT_NPC = new GameObject[6];
    //public GameObject[] Generic_NPC = new GameObject[6];

    public GameObject genericAvatarHolder;
    public GameObject nftAvatarHolder;

    public GameObject[] backgroundPositions;

    public AvatarType _avatarType;   // Generic Avatar or NFT Avatar
    public static AvatarType avatarType;
    public int _nowTrial;
    public static int nowTrial;


    //public NPCAnimation npcAnimation;

    //private bool isPrefab;

    private void Start()
    {
        avatarType = _avatarType;
        nowTrial = _nowTrial;

        if (avatarType == 0)
        {
            genericAvatarHolder.SetActive(true);
            nftAvatarHolder.SetActive(false);
        }
        else
        {
            genericAvatarHolder.SetActive(false);
            nftAvatarHolder.SetActive(true);
        }

        /*
        foreach (GameObject npc in NFT_NPC)
        {
            GameObject instanceNPC = Instantiate(npc);
            instanceNPC.tag = npc.name;
        }
        */

        /* Player Setting (Prefab in /Resource) */
        float posX = -0.904f;
        float posY = -5.246f;
        float posZ = 1.49f;
        //GameObject getPlayerPrefab = Resources.Load<GameObject>("NFT_Models/" + PhotonNetwork.NickName);
        GameObject temp = PhotonNetwork.Instantiate(PhotonNetwork.NickName, new Vector3(posX, posY, posZ), Quaternion.Euler(0, -90, 0));
        Instantiate(player, new Vector3(posX, posY, posZ), Quaternion.Euler(0, -90, 0));


        /* Background Environment Model Setting */
        GameObject[] tempNPCModels = Resources.LoadAll<GameObject>("NFT_Models/NPC/");
        GameObject[] backgroundModels = new GameObject[backgroundPositions.Length];
        
        for(int i = 0; i < backgroundPositions.Length; i++)
        {
            if(tempNPCModels[i].name != PhotonNetwork.NickName)
            {
                backgroundModels[i] = Instantiate(tempNPCModels[i], backgroundPositions[i].transform.position, backgroundPositions[i].transform.rotation);

                if(i == 0 || i == 1 || i == 10 || i == 11)
                {
                    Animator animator = backgroundModels[i].GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BackgroundAnimation/Background_Talking_Animator");
                    animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
                    animator.applyRootMotion = true;
                }
                
                else
                {
                    Animator animator = backgroundModels[i].GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BackgroundAnimation/Background_Based_Animator");
                    animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
                    animator.applyRootMotion = true;
                }
                
            }
            
        }
        /* Talk Animation */
        backgroundModels[0].GetComponent<Animator>().SetBool("Talk 2", true);
        backgroundModels[1].GetComponent<Animator>().SetBool("Talk 3", true);

        backgroundModels[10].GetComponent<Animator>().SetBool("Talk 3", true);
        backgroundModels[11].GetComponent<Animator>().SetBool("Talk 2", true);

        /* Base Animation */
        backgroundModels[2].GetComponent<Animator>().SetBool("SwingDancing", true);
        backgroundModels[3].GetComponent<Animator>().SetBool("SwingDancing 2", true);
        backgroundModels[4].GetComponent<Animator>().SetBool("Hold", true);
        backgroundModels[5].GetComponent<Animator>().SetBool("Stroll", true);
        backgroundModels[6].GetComponent<Animator>().SetBool("SwingDancing", true);
        backgroundModels[7].GetComponent<Animator>().SetBool("HipHop", true);
        backgroundModels[8].GetComponent<Animator>().SetBool("Hold", true);
        backgroundModels[9].GetComponent<Animator>().SetBool("Stroll", true);

        //float randomPosX = Random.Range(-3.0f, 3.0f);
        //float randomPosY = -5.246f;
        //float randomPosZ = Random.Range(-3.0f, 3.0f);
        //GameObject player = PhotonNetwork.Instantiate(PhotonNetwork.NickName, new Vector3(randomPosX, randomPosY, randomPosZ), Quaternion.Euler(0, -90, 0));
        //npcAnimation = player.AddComponent<NPCAnimation>();
        //npcAnimation.settingNPC();
    }
}
