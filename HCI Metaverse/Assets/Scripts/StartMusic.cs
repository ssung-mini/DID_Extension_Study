using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //������� �����ص�
        backmusic.Play();
        DontDestroyOnLoad(BackgroundMusic); //������� ��� ���
    }
}
