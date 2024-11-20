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
    private static int allTrialNum;    // 전체 트라이얼 횟수 (1~4)
    public static string avatarType;

    static CsvFileWriter emotionWriter;
    static List<string> emotionColumns;

    public static string avatarEmotion;
    public static string selectedEmotion;

    public static int currentCount = 0;
    public static int sameCount = 0;

    private static float timeRealGaze = 0;     // 응시하고 있는 시간이 0.2초 이상인지 아닌지 확인, Saccadic movement 잡기 위한 시간 체크.
    private static Vector3 previousRealGaze;
    public GameObject gazePosition;
    public EyeDataManager eyeDataManager;
    public GameObject Camera;

    static CsvFileWriter[] FilteredRealGazePosition = new CsvFileWriter[6];
    static List<string> FilteredRealGazecolumns;

    static CsvFileWriter[] FilteredEyeMovement = new CsvFileWriter[6];
    static List<string> FilteredEyeColumns;

    static CsvFileWriter[] HeadMovement = new CsvFileWriter[6];
    static List<string> HeadColumns;

    private static int positionNum;
    private static float headGaze, bodyGaze, verifiedGaze, nameGaze, sexGaze, birthGaze, crimeGaze, countryGaze, workGaze, remainder;

    public static float conversationTime = 0;
    public static float evaluationTime = 0;
    public static float recordingTime = 0;
    public static bool isEnded = true;
    public static bool nowConversation = false;
    public static bool nowEvaluation = false;
    public static bool nowRecording = false;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(ProgramManager.playerNickname);
        /*path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + 
            "/DID_Study/" + ProgramManager.participantsIndex + "번 피험자/" + avatarType + "/Trial_" + trialNumber + "/";*/
        allTrialNum = 1;

        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
            "/DID_Study/" + ProgramManager.participantsIndex + "번 피험자/" + "Trial_" + allTrialNum + "/";

        // 폴더 유무 확인
        DirectoryInfo di = new DirectoryInfo(path);
        
        while(di.Exists)    // ex) trial 1 폴더가 이미 있으면 trial 2 폴더를 생성하게끔 설정 (1, 2 존재 -> 3 생성)
        {
            ++allTrialNum;
            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
                "/DID_Study/" + ProgramManager.participantsIndex + "번 피험자/" + "Trial_" + allTrialNum + "/";
            di = new DirectoryInfo(path);
        }
        
        // 폴더가 없으면 폴더 생성
        if (!di.Exists)
            di.Create();

        emotionWriter = new CsvFileWriter(path + "SelectedEmotion.csv");
        emotionColumns = new List<string>() { "AvatarNumber", "Avatar's_Emotion", "Selected_Emotion", "Accuray", "FaceGaze", "BodyGaze", 
            "verifiedGaze", "nameGaze", "sexGaze", "birthGaze", "crimeGaze", "countryGaze", "workGaze", 
            "Remainder", "RecordingTime", "EvaluationTime" }; 

        emotionWriter.WriteRow(emotionColumns);
        emotionColumns.Clear();

        for ( int i = 0; i < 6; i++ )
        {
            FilteredRealGazePosition[i] = new CsvFileWriter(path + (i + 1) +"_RealGazePosition(Filtered).csv");
            FilteredEyeMovement[i] = new CsvFileWriter(path + (i + 1) + "_EyeMovement(Filtered).csv");
            HeadMovement[i] = new CsvFileWriter(path + (i + 1) + "_HeadMovement.csv");

            FilteredRealGazecolumns = new List<string>() { "CurrentTime", "RealGazePos_x", "RealGazePos_y", "RealGazePos_z" };
            FilteredEyeColumns = new List<string>() { "CurrentTime", "GazePos_x", "GazePos_y" };
            HeadColumns = new List<string>() { "CurrentTime", "HeadRot_x", "HeadRot_y", "HeadRot_z" };

            FilteredRealGazePosition[i].WriteRow(FilteredRealGazecolumns);
            FilteredRealGazecolumns.Clear();
            FilteredEyeMovement[i].WriteRow(FilteredEyeColumns);
            FilteredEyeColumns.Clear();
            HeadMovement[i].WriteRow(HeadColumns);
            HeadColumns.Clear();
        }
        
    }

    private void Update()
    {
        if (!isEnded)
        {
            if(nowConversation && !nowRecording && !nowEvaluation)
            {
                Check_Timer();
                WritingEyeData(positionNum - 1);
                WritingHeadData(positionNum - 1);
            }

            if(!nowConversation && nowRecording && !nowEvaluation)
            {
                Check_RecordingTime();
            }

            if(nowEvaluation)
            {
                Check_EvaluateTime();
            }
            
        }

    }

    public static float GetConversationTime()
    {
        return conversationTime;
    }

    public static void SetPositionNumber(int _positionNum)
    {
        positionNum = _positionNum;
    }

    public static void SetValue(string _avatarEmotion, int _positionNum, float _headGaze, float _bodyGaze, 
        float _verifiedGaze, float _nameGaze, float _sexGaze, float _birthGaze, float _crimeGaze, float _countryGaze, float _workGaze, float _remainder)
    {
        avatarEmotion = _avatarEmotion;
        positionNum = _positionNum;
        headGaze = _headGaze;
        bodyGaze = _bodyGaze;
        verifiedGaze = _verifiedGaze;
        nameGaze = _nameGaze;
        sexGaze = _sexGaze;
        birthGaze = _birthGaze;
        crimeGaze = _crimeGaze;
        countryGaze = _countryGaze;
        workGaze = _workGaze;
        remainder = _remainder;
    }

    public static void WritingEmotion()
    {
        ++currentCount;
        Debug.Log(selectedEmotion + " 선택");
        emotionColumns.Add(positionNum.ToString());
        emotionColumns.Add(avatarEmotion);
        emotionColumns.Add(selectedEmotion);

        if(avatarEmotion.Equals(selectedEmotion)) ++sameCount;
        
        emotionColumns.Add(((float)(sameCount) / (float)(currentCount)).ToString());

        emotionColumns.Add(headGaze.ToString());
        emotionColumns.Add(bodyGaze.ToString());
        emotionColumns.Add(verifiedGaze.ToString());
        emotionColumns.Add(nameGaze.ToString());
        emotionColumns.Add(sexGaze.ToString());
        emotionColumns.Add(birthGaze.ToString());
        emotionColumns.Add(crimeGaze.ToString());
        emotionColumns.Add(countryGaze.ToString());
        emotionColumns.Add(workGaze.ToString());
        emotionColumns.Add(remainder.ToString());

        emotionColumns.Add(recordingTime.ToString());
        emotionColumns.Add(evaluationTime.ToString());

        emotionWriter.WriteRow(emotionColumns);
        emotionColumns.Clear();

        CanvasOnOff.DisableSelectCanvas();

        if(currentCount == 6)
        {
            StaticCoroutine.DoCoroutine(GameClearQuit());
        }
    }

    /*public static void WritingGazeTime(int positionNum, float headGaze, float bodyGaze, float infoGaze, float remainder)
    {
        using (var writer = new CsvFileWriter(path + positionNum + "_GazeTime.csv"))
        {
            List<string> columns = new List<string>() { "Emotion", "faceGaze", "bodyGaze", "infoGaze", "remainder" };
            writer.WriteRow(columns);
            columns.Clear();
            columns.Add(avatarEmotion);
            columns.Add(headGaze.ToString());  
            columns.Add(bodyGaze.ToString());
            columns.Add(infoGaze.ToString());
            columns.Add(remainder.ToString());

            writer.WriteRow(columns);
            columns.Clear();

            Debug.Log("Complete save GazeTime.csv");

        }
    }*/

    void WritingEyeData(int i)
    {
        // FilteredData Writing
        gazePosition.GetComponent<Transform>().position = eyeDataManager.realGazePosition;                  // Head rotation + Eye movement 결합된 현재 실제로 보고 있는 곳의 위치.
        Vector2 xyNow = new Vector2(gazePosition.transform.position.x, gazePosition.transform.position.y);  // 화면상 현재 보고 있는 점의 위치.
        Vector2 xyPrevious = new Vector2(previousRealGaze.x, previousRealGaze.y);                           // 화면상 바로 이전 frame에서 봤던 점의 위치.
        var distance = Vector2.Distance(xyNow, xyPrevious);                                                 // 두 점간의 거리. 
        //Debug.Log("Distance: " + distance);
        if (distance < 1.0f)
        {
            timeRealGaze += Time.deltaTime;
            if (timeRealGaze > 0.2f)
            {
                timeRealGaze = 0.0f;

                // Filtered RealGazePosition (Saccadiv movement 제외된 현제 보고있는 위치의 world position)
                FilteredRealGazecolumns.Add(conversationTime.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.x.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.y.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.z.ToString());

                FilteredRealGazePosition[i].WriteRow(FilteredRealGazecolumns);
                FilteredRealGazecolumns.Clear();

                // Filtered EyeMovement (Saccadic movement 제외된 현재 보고있는 HMD 렌즈상의 위치)
                FilteredEyeColumns.Add(conversationTime.ToString());
                FilteredEyeColumns.Add(eyeDataManager.gazePosition.x.ToString());
                FilteredEyeColumns.Add(eyeDataManager.gazePosition.y.ToString());

                FilteredEyeMovement[i].WriteRow(FilteredEyeColumns);
                FilteredEyeColumns.Clear();
            }
        }
    }

    void WritingHeadData(int i)
    {
        // Head의 Rotation 기록 (Head rotation 값은 inspector 값 그대로 가져옴)
        var headRotation = Camera.transform.rotation.eulerAngles;

        HeadColumns.Add(conversationTime.ToString());
        HeadColumns.Add(headRotation.x.ToString());
        HeadColumns.Add(headRotation.y.ToString());
        HeadColumns.Add(headRotation.z.ToString());

        HeadMovement[i].WriteRow(HeadColumns);
        HeadColumns.Clear();
    }

    // Timer 설정
    private void Check_Timer()
    {
        conversationTime += Time.deltaTime;
    }

    private void Check_EvaluateTime()
    {
        evaluationTime += Time.deltaTime;
    }

    private void Check_RecordingTime()
    {
        recordingTime += Time.deltaTime;
    }

    public static void End_Timer()
    {
        Debug.Log("End");
        isEnded = true;
    }

    public static void Start_Timer()
    {
        isEnded = false;
        Debug.Log("Start");

        conversationTime = 0;
        evaluationTime = 0;
        recordingTime = 0;
    }

    public static string GetRecordingPath()
    {
        return path + positionNum + "_recordingVoice.wav";
    }

    private void OnApplicationQuit()
    {
        emotionWriter.Dispose();
        for(int i = 0; i < 6; i++)
        {
            FilteredEyeMovement[i].Dispose();
            FilteredRealGazePosition[i].Dispose();
            HeadMovement[i].Dispose();
        }
    }

    // 5초 후 종료 코루틴
    public static IEnumerator GameClearQuit()
    {
        yield return new WaitForSecondsRealtime(5);
        GameQuit();
    }

    // 게임 종료
    public static void GameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void LateUpdate()
    {
        previousRealGaze = gazePosition.transform.position;
    }

    public static void Button1() { selectedEmotion = "Fear"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
    public static void Button2() { selectedEmotion = "Anger"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
    public static void Button3() { selectedEmotion = "Sadness"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
    public static void Button4() { selectedEmotion = "Joy"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
    public static void Button5() { selectedEmotion = "Disgust"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
    public static void Button6() { selectedEmotion = "Surprise"; nowEvaluation = false; End_Timer(); WritingEmotion(); }
}
