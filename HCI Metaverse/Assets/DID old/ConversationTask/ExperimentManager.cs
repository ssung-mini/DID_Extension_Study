using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    GameObject settingReceiver;
    public GameObject genericAvatarHolder;
    public GameObject realisticAvatarHolder;

    public int avatarType;
    
    public int nowTrial;

    // Start is called before the first frame update
    void Start()
    {
        //settingReceiver = GameObject.Find("SettingReceiver");
                
        /*if (settingReceiver == null)
        {
            //avatarType = 0; // 0: Generic 1: Realistic
            genericAvatarHolder.SetActive(true);
            realisticAvatarHolder.SetActive(false);
        }
        else
        {*/
            //avatarType = settingReceiver.GetComponent<SettingReceiver>().avatarType;
            if (avatarType == 0)
            {
                genericAvatarHolder.SetActive(true);
                realisticAvatarHolder.SetActive(false);
            }
            else
            {
                genericAvatarHolder.SetActive(false);
                realisticAvatarHolder.SetActive(true);
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
