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
    public GameObject playerPrefab;

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

        // Player 생성 (Resource 폴더에 Prefab 파일이 존재해야 함)
        float posX = -0.904f;
        float posY = -5.246f;
        float posZ = 1.49f;
        GameObject player = PhotonNetwork.Instantiate(PhotonNetwork.NickName, new Vector3(posX, posY, posZ), Quaternion.Euler(0, -90, 0));


        GameObject[] backgroundModels = Resources.LoadAll<GameObject>("NFT_Models/");
        /* 피험자들의 아바타를 배경요소로 배치
        for(int i = 0; i < backgroundModels.Length; i++)
        {
            Instantiate(backgroundModels[i], backgroundPositions[i].transform.position, backgroundPositions[i].transform.rotation);
        }
        */

        //float randomPosX = Random.Range(-3.0f, 3.0f);
        //float randomPosY = -5.246f;
        //float randomPosZ = Random.Range(-3.0f, 3.0f);
        //GameObject player = PhotonNetwork.Instantiate(PhotonNetwork.NickName, new Vector3(randomPosX, randomPosY, randomPosZ), Quaternion.Euler(0, -90, 0));
        //npcAnimation = player.AddComponent<NPCAnimation>();
        //npcAnimation.settingNPC();
    }

    private void Update()
    {
        //StartCoroutine(ViewPlayerList());

        /*
        if(Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        */

        //npcAnimation.animationNPC();
    }

    /*IEnumerator ViewPlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }

        int scrollView_y = 0;
        for (int i = 0; i < players.Length; i++)
        {
            GameObject playerInstance = Instantiate(playerInfoPanel, new Vector3(0, scrollView_y, 0), Quaternion.identity);
            playerInstance.transform.SetParent(GameObject.Find("Viewport").transform.Find("Content").transform);

            scrollView_y -= 55;

            playerInstance.transform.Find("Player Text").GetComponent<Text>().text = players[i].NickName;

        }

        yield return new WaitForSeconds(1.0f);
    }*/
}
