using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvWritingManager : MonoBehaviour
{
    // 디렉토리 저장 경로
    static string path;

    // trialNumber => ProgramManager.cs
    public static int trialNumber;
    public static string avatarType;

    static CsvFileWriter emotionWriter;
    static List<string> emotionColumns;

    public static string avatarEmotion;
    public static string selectedEmotion;

    public static int currentCount = 0;
    public static int sameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(ProgramManager.playerNickname);
        path = "Assets/CSV_files/DID_Study/" + ProgramManager.participantsIndex + "번 피험자/" + avatarType + "/";

        // 폴더 유무 확인
        DirectoryInfo di = new DirectoryInfo(path);
        if (!di.Exists)
            di.Create();

        emotionWriter = new CsvFileWriter(path + trialNumber +"_SelectedEmotion.csv");
        emotionColumns = new List<string>() { "Avatar's_Emotion", "Selected_Emotion", "Accuray" }; 

        emotionWriter.WriteRow(emotionColumns);
        emotionColumns.Clear();
    }

    public static void SetEmotion(string _avatarEmotion)
    {
        avatarEmotion = _avatarEmotion;
    }

    public static void WritingEmotion()
    {
        ++currentCount;
        Debug.Log(selectedEmotion + " 선택");
        emotionColumns.Add(avatarEmotion);
        emotionColumns.Add(selectedEmotion);

        if(avatarEmotion.Equals(selectedEmotion)) ++sameCount;
        
        emotionColumns.Add(((float)(sameCount) / (float)(currentCount)).ToString());

        emotionWriter.WriteRow(emotionColumns);
        emotionColumns.Clear();

        CanvasOnOff.DisableCanvas();
    }

    private void OnApplicationQuit()
    {
        emotionWriter.Dispose();
    }

    public static void Button1() { selectedEmotion = "Fear"; WritingEmotion(); }
    public static void Button2() { selectedEmotion = "Anger"; WritingEmotion(); }
    public static void Button3() { selectedEmotion = "Sadness"; WritingEmotion(); }
    public static void Button4() { selectedEmotion = "Joy"; WritingEmotion(); }
    public static void Button5() { selectedEmotion = "Disgust"; WritingEmotion(); }
    public static void Button6() { selectedEmotion = "Surprise"; WritingEmotion(); }
}
