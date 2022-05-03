using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Net;

[System.Serializable]
public class PlayerInformation
{
    public string name;
    public string description;
    public string image;
    public string artist;
    public List<Dictionary<string, string>> attributes; 
    public string fbxURI;
    public string info_level_1;
    public string info_level_2;
    public string info_level_3;

}

public class PlayerInfoList
{
    public List<PlayerInformation> model;
}

[System.Serializable]
public class JsonParsing : MonoBehaviour
{
    private string jsonfilePath;
    public string FBXpath;

    public PlayerInfoList playerInfoList;

    // Json 파일이름 입력 후, filePath 찾기
    public void setJsonFilePath(string jsonfileName)
    {
        this.jsonfilePath = Application.dataPath + "/Resources/" + jsonfileName;
        Debug.Log(jsonfilePath);
    }
    
    // Resources 폴더 내에 있는 json 파일 파싱
    public void parseJsonfile(string playerName)
    {
        if (File.Exists(this.jsonfilePath))
        {
            string getJsonText = File.ReadAllText(this.jsonfilePath);

            playerInfoList = JsonToObject<PlayerInfoList>(getJsonText);

            foreach (PlayerInformation playerInformation in playerInfoList.model)
            {
                FBXpath = Application.dataPath + "/Resources/Models/" + playerInformation.name + ".fbx";
                if (!File.Exists(FBXpath)) downLoadFBX(playerInformation.fbxURI, FBXpath);

                Debug.Log(playerInformation.name);
                if (playerInformation.name.Equals(playerName))
                {
                    ProgramManager.text_Level1 = playerInformation.info_level_1;
                    ProgramManager.text_Level2 = playerInformation.info_level_2;
                    ProgramManager.text_Level3 = playerInformation.info_level_3;  
                }
            }
        }
        else Debug.Log("Can't find " + this.jsonfilePath);
    }

    private void downLoadFBX(string URL, string FilePath)
    {
        
        WebClient webClient = new WebClient();
        webClient.DownloadFile(URL, FilePath);
        Debug.Log("Done");

    }

    T JsonToObject<T>(string jsonData) where T : class
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void parseJsonText()
    {
        if (File.Exists(this.jsonfilePath))
        {
            string getJsonText = File.ReadAllText(this.jsonfilePath);
            
        }
    }
}
