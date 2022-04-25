using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.IO;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string programVersion = "1.0.0";

    /* Player Information */
    public InputField playerName;
    public GameObject playerInfoPanel;

    /* Connection Information */
    public Text connectionInfoText;
    public GameObject connectionInfoPanel;
    private JsonParsing jsonparsing;
    private bool isConnect = false;
    
    public void Connect()
    {
        playerInfoPanel.SetActive(false);

        connectionInfoPanel.SetActive(true);

        PhotonNetwork.GameVersion = programVersion;

        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "Connecting ...";
        
        isConnect = true;
    }

    public override void OnConnectedToMaster()
    {
        connectionInfoText.text = "Online :)";

        PhotonNetwork.NickName = playerName.text;
 
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionInfoText.text = "Offline :(";
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Loading ...";

            jsonparsing.setJsonFilePath("Test_FBX_File.json");
            jsonparsing.parseJsonfile();

            foreach (PlayerInformation playerModel in jsonparsing.playerInfoList.model)
            {
                //PlayerSetting.doCreatePrefab(playerModel.name);
                //NPCSetting.doCreateNPCPrefab(playerModel.name);
            }

            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });    
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Join to the room ...";

        ChangeScene();
    }

    private void ChangeScene()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }

    private void Update()
    {
        if(isConnect)
        {
            if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    private void Start()
    {
        jsonparsing = new JsonParsing();
    }
}
