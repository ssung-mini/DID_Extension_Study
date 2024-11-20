using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoToMainScene : MonoBehaviour
{
    FadeScreen fadeScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeScreen = RoomManager.playerPrefab.GetComponentInChildren<Camera>().GetComponentInChildren<FadeScreen>();
    }

    // Update is called once per frame
    public void ChangeScene()
    {
        Debug.Log("MainScene ¿‘¿Â");
        StartCoroutine(FadeOutScene());
    }

    public IEnumerator FadeOutScene()
    {
        fadeScreen.fadeDuration = 3;
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        PhotonNetwork.LoadLevel("MainScene");
    }
}
