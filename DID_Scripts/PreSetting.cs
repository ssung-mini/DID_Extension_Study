using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreSetting : MonoBehaviour
{
    [Header("실험 세팅 필드")]
    [SerializeField] Dropdown avatarType;
    [SerializeField] Dropdown nowTrial;
    [SerializeField] Dropdown informationLevel;
    [SerializeField] InputField participantsNum;

    private void Start()
    {
        SettingValue.avatarTypeValue = avatarType.value;
        SettingValue.nowTrialValue = (nowTrial.value + 1);
        SettingValue.informationValue = informationLevel.value;

        Debug.Log(SettingValue.avatarTypeValue);
    }

    public void TypeSelect()
    {
        Debug.Log("Avatar Type : " + avatarType.options[avatarType.value].text);
        Debug.Log("Avatar Type : " + avatarType.value.ToString());
        SettingValue.avatarTypeValue = avatarType.value;
    }

    public void TrialSelect()
    {
        Debug.Log("Now Trial : " + nowTrial.options[nowTrial.value].text);
        SettingValue.nowTrialValue = (nowTrial.value + 1);
    }

    public void InformationSelect()
    {
        Debug.Log("Information Level : " + informationLevel.options[informationLevel.value].text);
        SettingValue.informationValue = informationLevel.value;
    }

    public void NumSelect()
    {
        if(participantsNum.text.Equals(""))
        {
            return;
        }
        Debug.Log("Index Number : " + participantsNum.text);
        SettingValue.participantsNumValue = int.Parse(participantsNum.text);
    }
}
