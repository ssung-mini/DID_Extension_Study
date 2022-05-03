using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOnOff : MonoBehaviour
{
    public GameObject _canvasHolder;
    public static GameObject canvasHolder;

    private void Awake()
    {
        canvasHolder = _canvasHolder;
    }

    public static void EnableCanvas()
    {
        canvasHolder.SetActive(true);
    }

    public static void DisableCanvas()
    {
        canvasHolder.SetActive(false);
    }
}
