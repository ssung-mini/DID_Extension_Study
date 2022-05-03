using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        backmusic.Play();
        DontDestroyOnLoad(BackgroundMusic); //배경음악 계속 재생
    }
}
