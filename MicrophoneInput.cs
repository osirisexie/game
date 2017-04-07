using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MicrophoneInput
{
	private const int FREQUENCY = 44100;    // Wavelength, I think.
	private const int SAMPLECOUNT = 1024;   // Sample Count.
	private const float REFVALUE = 0.1f;    // RMS value for 0 dB.

	public int clamp = 160;            // Used to clamp dB (I don't really understand this either).

	private float rmsValue;            // Volume in RMS
	private float dbValue;             // Volume in DB

	private float[] samples;           // Samples

	private AudioSource audio;
	private PlayerProfile player;

	public MicrophoneInput (PlayerProfile gamePlayer)
	{
		player = gamePlayer;
	}

	public void StartMicListener() {
		samples = new float[SAMPLECOUNT];
		audio = player.gameObject.GetComponent<AudioSource> ();
		audio.clip = Microphone.Start("Built-in Microphone", true, 999, 44100);
		while (!(Microphone.GetPosition("Built-in Microphone") > 0)) {
		} audio.Play();
	}
		
	public float AnalyzeSound() {
		if (!audio.isPlaying) {
			StartMicListener();
		}
		audio.GetOutputData(samples, 0);

		float sum = 0;
		for (int i = 0; i < SAMPLECOUNT; i++){
			sum += Mathf.Pow(samples[i], 2);
		}

		rmsValue = Mathf.Sqrt(sum / SAMPLECOUNT);
		dbValue = 20 * Mathf.Log10(rmsValue / REFVALUE);
		return dbValue;
	}




}

