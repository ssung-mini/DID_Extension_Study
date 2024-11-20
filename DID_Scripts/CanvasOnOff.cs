using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOnOff : MonoBehaviour
{
    [Header("UI ÇÊµå")]
    public GameObject _selectHolder;
    public GameObject _pressHolder;
    public GameObject _recordingHolder;
    public static GameObject selectHolder;
    public static GameObject pressHolder;
    public static GameObject recordingHolder;

    private void Awake()
    {
        selectHolder = _selectHolder;
        pressHolder = _pressHolder;
        recordingHolder = _recordingHolder;
    }

    public static void EnableSelectCanvas()
    {
        selectHolder.SetActive(true);
    }

    public static void DisableSelectCanvas()
    {
        selectHolder.SetActive(false);
    }

    public static void EnablePressCanvas()
    {
        pressHolder.SetActive(true);
    }

    public static void DisablePressCanvas()
    {
        pressHolder.SetActive(false);
    }

    public static void EnableRecordingCanvas()
    {
        recordingHolder.SetActive(true);
    }

    public static void DisableRecordingCanvas()
    {
        recordingHolder.SetActive(false);
    }
}
