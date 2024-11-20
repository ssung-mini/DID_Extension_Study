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
                // Gaze ���� �� bool nowGaze�� true�� �Ǹ� Update���� GazeTime ���� (float gazeTime += Time.deltaTime ���)
                nowGaze = true;
            }
            
            else
            {
                // Gaze�� �ȵȴٸ� bool nowGaze�� false�� �ٲ�� ��
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