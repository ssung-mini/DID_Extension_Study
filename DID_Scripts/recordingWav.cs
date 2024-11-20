using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class recordingWav : MonoBehaviour
{
	public static AudioClip recordClip;

    private void Start()
    {
		
    }

    public static void StartRecordMicrophone()
	{
		recordClip = Microphone.Start(Microphone.devices[0], true, 100, 44100);
		Debug.Log("오디오 녹음 시작");
	}

	public static void StopRecordMicrophone(string path)
	{
		int lastTime = Microphone.GetPosition(null);

		if (lastTime == 0)
			return;
		else
		{
			Microphone.End(Microphone.devices[0]);

			float[] samples = new float[recordClip.samples];

			recordClip.GetData(samples, 0);

			float[] cutSamples = new float[lastTime];

			Array.Copy(samples, cutSamples, cutSamples.Length - 1);

			recordClip = AudioClip.Create("Notice", cutSamples.Length, 1, 44100, false);

			recordClip.SetData(cutSamples, 0);

			SavWav.Save(path, recordClip);
			Debug.Log("오디오 클립 저장");
		}
	}
	
}
