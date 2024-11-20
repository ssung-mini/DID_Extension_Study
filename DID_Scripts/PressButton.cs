using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class PressButton : MonoBehaviour
{
    
    //Stores what kind of characteristics we¡¯re looking for with our Input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    //Stores the InputDevice that we¡¯re Targeting once we find it in InitializeHand()
    private InputDevice _targetDevice;
    
    public static bool isRecording;

    [SerializeField] PhotonView PV;

    void Start()
    {
        InitializeInput();
    }

    private void InitializeInput()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we¡¯re looking for
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {

            _targetDevice = devices[0];
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }
        //Since our target device might not register at the start of the scene, we continously check until one is found.
        if (!_targetDevice.isValid)
        {
            
        }
        else
        {
            UpdateInput();
        }
    }

    private void UpdateInput()
    {
        //This will get the value for our trigger from the target device and output a flaot into triggerValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue))
        {
            if (isRecording == false)
                isRecording = true;
            else isRecording = false;
        }

        //This will get the value for our grip from the target device and output a flaot into gripValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue))
        {
            if (isRecording == false)
                isRecording = true;
            else isRecording = false;
        }
    }
}