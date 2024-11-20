using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingValue : MonoBehaviour
{
    public GameObject settingValue;

    public static int avatarTypeValue;
    public static int nowTrialValue;
    public static int informationValue;
    public static int participantsNumValue;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(settingValue);
    }
}
