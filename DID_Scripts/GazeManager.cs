using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR.Examples.DevTools;

public class GazeManager : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _verified;
    [SerializeField] private GameObject _name;
    [SerializeField] private GameObject _sex;
    [SerializeField] private GameObject _birth;
    [SerializeField] private GameObject _crime;
    [SerializeField] private GameObject _country;
    [SerializeField] private GameObject _work;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void enableCollider()
    {
        _head.SetActive(true);
        _body.SetActive(true);
        _verified.SetActive(true);
        _name.SetActive(true);
        _sex.SetActive(true);
        _birth.SetActive(true);
        _crime.SetActive(true);
        _country.SetActive(true);
        _work.SetActive(true);
    }

    public void disableCollider()
    {
        _head.SetActive(false);
        _body.SetActive(false);
        _verified.SetActive(false);
        _name.SetActive(false);
        _sex.SetActive(false);
        _birth.SetActive(false);
        _crime.SetActive(false);
        _country.SetActive(false);
        _work.SetActive(false);
    }

    public void SetConversationTime()
    {
        
    }
    

    public float RemainderTime(float _conversationTime)
    {
        float remainderTime = 0;

        float headGaze = _head.GetComponent<GazeTarget>().gazeTime;
        float bodyGaze = _body.GetComponent<GazeTarget>().gazeTime;
        if(!((AvatarType)SettingValue.avatarTypeValue == 0))
        {
            float verifiedGaze = _verified.GetComponent<GazeTarget>().gazeTime;
            float nameGaze = _name.GetComponent<GazeTarget>().gazeTime;
            float sexGaze = _sex.GetComponent<GazeTarget>().gazeTime;
            float birthGaze = _birth.GetComponent<GazeTarget>().gazeTime;
            float crimeGaze = _crime.GetComponent<GazeTarget>().gazeTime;
            float countryGaze = _country.GetComponent<GazeTarget>().gazeTime;
            float workGaze = _work.GetComponent<GazeTarget>().gazeTime;
            remainderTime = _conversationTime - (headGaze + bodyGaze + verifiedGaze + nameGaze + sexGaze + birthGaze + crimeGaze + countryGaze + workGaze);
        }
        
        else remainderTime = _conversationTime - (headGaze + bodyGaze);


        return remainderTime;
    }

    public float getHeadGaze()
    {
        return _head.GetComponent<GazeTarget>().gazeTime;
    }

    public float getBodyGaze()
    {
        return _body.GetComponent<GazeTarget>().gazeTime;
    }

    public float getVerifiedGaze()
    {
        return _verified.GetComponent<GazeTarget>().gazeTime;
    }

    public float getNameGaze()
    {
        return _name.GetComponent<GazeTarget>().gazeTime;
    }

    public float getSexGaze()
    {
        return _sex.GetComponent<GazeTarget>().gazeTime;
    }

    public float getBirthGaze()
    {
        return _birth.GetComponent<GazeTarget>().gazeTime;
    }

    public float getCrimeGaze()
    {
        return _crime.GetComponent<GazeTarget>().gazeTime;
    }

    public float getCountryGaze()
    {
        return _country.GetComponent<GazeTarget>().gazeTime;
    }

    public float getWorkGaze()
    {
        return _work.GetComponent<GazeTarget>().gazeTime;
    }
}
