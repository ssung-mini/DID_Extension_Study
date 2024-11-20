using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public enum AvatarType
{
    Generic_Avatar = 0,
    NFT_Avatar = 1
}

public enum InformationLevel
{
    Level_1 = 0, 
    Level_2 = 1, 
    Level_3 = 2
}

public class ProgramManager : MonoBehaviourPunCallbacks
{
    /* Player Prefab */
    public GameObject player;
    public GameObject infoTextPrefab;
    public static GameObject playerPrefab;

    private RootMotion.FinalIK.VRIK vrIK;
    private RootMotion.FinalIK.IKSolverVR vrIKSolver;

    /* NPC Prefab */
    //public GameObject[] NFT_NPC = new GameObject[6];
    //public GameObject[] Generic_NPC = new GameObject[6];

    public GameObject genericAvatarHolder_1; public GameObject genericAvatarHolder_2;
    public GameObject nftAvatarHolder_1; public GameObject nftAvatarHolder_2;

    public GameObject[] backgroundPositions;

    /* Player Info Panel */
    public int _participantsIndex;
    public static int participantsIndex;

    public InformationLevel _informationLevel;
    public static InformationLevel informationLevel;
    public AvatarType _avatarType;   // Generic Avatar or NFT Avatar
    public static AvatarType avatarType;
    
    public int _nowTrial;
    public static int nowTrial;

    public static string text_Level1;
    public static string text_Level2;
    public static string text_Level3;

    public static string playerNickname;


    //public NPCAnimation npcAnimation;

    //private bool isPrefab;

    private void Start()
    {
        informationLevel = _informationLevel;
        avatarType = _avatarType;
        nowTrial = _nowTrial;
        participantsIndex = _participantsIndex;

        informationLevel = (InformationLevel)SettingValue.informationValue;
        avatarType = (AvatarType)SettingValue.avatarTypeValue;
        nowTrial = SettingValue.nowTrialValue;
        participantsIndex = SettingValue.participantsNumValue;

        playerNickname = PhotonNetwork.NickName;

        CsvWritingManager.trialNumber = nowTrial;
        CsvWritingManager.avatarType = avatarType.ToString();
        Debug.Log(avatarType.ToString());

        genericAvatarHolder_1.SetActive(false);
        genericAvatarHolder_2.SetActive(false);
        nftAvatarHolder_1.SetActive(false);
        nftAvatarHolder_2.SetActive(false);

        if (avatarType == 0)
        {
            if(nowTrial == 1) genericAvatarHolder_1.SetActive(true);
            else if (nowTrial == 2) genericAvatarHolder_2.SetActive(true);
        }
        else
        {
            if (nowTrial == 1) nftAvatarHolder_1.SetActive(true);
            else if (nowTrial == 2) nftAvatarHolder_2.SetActive(true);
        }

        /*
        foreach (GameObject npc in NFT_NPC)
        {
            GameObject instanceNPC = Instantiate(npc);
            instanceNPC.tag = npc.name;
        }
        */

        /* Player Setting (Prefab in /Resource) */
        float posX = Random.Range(-0.5f, 1.2f); //float posX = -0.904f;
        float posY = -5.24f;
        float posZ = Random.Range(-0.2f, 2f); //float posZ = 1.49f;

        //GameObject getPlayerPrefab = Resources.Load<GameObject>("NFT_Models/" + PhotonNetwork.NickName);
        GameObject temp = PhotonNetwork.Instantiate(playerNickname, new Vector3(posX, posY, posZ), Quaternion.Euler(0, -90, 0));
        vrIK = temp.GetComponent<RootMotion.FinalIK.VRIK>();
        vrIKSolver = vrIK.solver;

        playerPrefab = PhotonNetwork.Instantiate(player.name, new Vector3(posX, posY, posZ), Quaternion.Euler(0, -90, 0));
        vrIKSolver.spine.headTarget = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1);
        vrIKSolver.leftArm.target = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
        vrIKSolver.rightArm.target = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0);

        Debug.Log("Guest Mode : " + playerNickname.Equals("Guest"));

        if(!playerNickname.Equals("Guest"))
        {
            GameObject infoText = PhotonNetwork.Instantiate(infoTextPrefab.name, new Vector3(posX, (posY - 0.05f), posZ), Quaternion.Euler(0, -270, 0));

            if ((int)informationLevel == 0)
            {
                infoText.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text_Level1);
            }
            else if ((int)informationLevel == 1)
            {
                infoText.transform.GetChild(1).GetComponent<TextMeshPro>().SetText(text_Level2);

                infoText.transform.GetChild(0).gameObject.SetActive(false);
                infoText.transform.GetChild(1).gameObject.SetActive(true);
                infoText.transform.GetChild(2).gameObject.SetActive(false);
            }

            else
            {
                infoText.transform.GetChild(2).GetComponent<TextMeshPro>().SetText(text_Level3);

                infoText.transform.GetChild(0).gameObject.SetActive(false);
                infoText.transform.GetChild(1).gameObject.SetActive(false);
                infoText.transform.GetChild(2).gameObject.SetActive(true);
            }
            infoText.transform.parent = temp.transform;
        }
        



        /*        Background Environment Setting         */

        


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

    private void Update()
    {
        
    }

    
}
