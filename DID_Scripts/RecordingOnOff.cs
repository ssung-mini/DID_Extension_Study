using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingOnOff : MonoBehaviour
{
    public static bool isRecording;
    
    private void Awake()
    {
        isRecording = false;
    }

    public void RecordingStart()
    {
        isRecording = true;
    }

    public void RecordingEnd()
    {
        isRecording = false;
    }
}
