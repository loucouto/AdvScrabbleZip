using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound: MonoBehaviour{

	public static void GetSound(string pObjName)
	{
		GameObject obj = GameObject.Find (pObjName);
		AudioSource a = obj.GetComponent<AudioSource> ();

		if (UICommon.soundActive == true) 
		{
			a.Play ();
		} 
		else 
		{
			if (a.isPlaying == true) 
			{
				a.Stop();
			}
		}

	}

	public static void StopSound(string pObjName)
	{
		GameObject obj = GameObject.Find (pObjName);
		AudioSource a = obj.GetComponent<AudioSource> ();

		if (a.isPlaying == true) 
		{
			a.Stop();
		}
	}
}
