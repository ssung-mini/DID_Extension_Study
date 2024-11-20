using Tobii.G2OM;
using UnityEngine;

namespace Tobii.XR.Examples.DevTools
{
    //Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
    public class GazeTarget : MonoBehaviour, IGazeFocusable
    {
        private bool nowGaze = false;
        
        public float gazeTime = 0;

        //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
        public void GazeFocusChanged(bool hasFocus)
        {
            
            if (hasFocus)
            {
                // Gaze 했을 때 bool nowGaze가 true가 되면 Update에서 GazeTime 증가 (float gazeTime += Time.deltaTime 사용)
                nowGaze = true;
            }
            
            else
            {
                // Gaze가 안된다면 bool nowGaze가 false로 바뀌게 함
                nowGaze = false;
            }
        }

        private void Start()
        {
            gazeTime = 0;
        }

        private void Update()
        {
            // if (nowGaze) gazeTime += Time.deltaTime;

            if(CsvWritingManager.nowConversation)
                if (nowGaze) gazeTime += Time.deltaTime;
            
        }
    }
}