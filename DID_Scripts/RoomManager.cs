using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using RootMotion;

public class RoomManager : MonoBehaviour
{
    /* Player Prefab */
    public GameObject player;
    public GameObject infoTextPrefab;
    
    public static GameObject playerPrefab;

    private RootMotion.FinalIK.VRIK vrIK;
    private RootMotion.FinalIK.IKSolverVR vrIKSolver;

    public Canvas canvasUI;
    
    public InformationLevel _informationLevel;
    public static InformationLevel informationLevel;

    public static string text_Level1;
    public static string text_Level2;
    public static string text_Level3;

    public Transform spawnPosition;

    public static string playerNickname;

    // Start is called before the first frame update
    void Start()    
    {
        
        
        

        informationLevel = _informationLevel;
        informationLevel = (InformationLevel)SettingValue.informationValue;

        playerNickname = PhotonNetwork.NickName;

        GameObject temp = PhotonNetwork.Instantiate(playerNickname, spawnPosition.position, Quaternion.Euler(0, 0, 0));
        vrIK = temp.GetComponent<RootMotion.FinalIK.VRIK>();
        vrIKSolver = vrIK.solver;

        playerPrefab = PhotonNetwork.Instantiate(player.name, spawnPosition.position, Quaternion.Euler(0, 0, 0));

        vrIKSolver.spine.headTarget = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1);
        vrIKSolver.leftArm.target = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
        vrIKSolver.rightArm.target = playerPrefab.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0);

        canvasUI.worldCamera = playerPrefab.GetComponentInChildren<Camera>();

        if (!playerNickname.Equals("Guest"))
        {
            GameObject infoText = PhotonNetwork.Instantiate(infoTextPrefab.name, 
                spawnPosition.position, 
                Quaternion.Euler(0, 0, 0));

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
    }
}
