using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour {

	public static string typeConnection = "";
	public static AudioSource aud;
	public static bool musicActive = true;
	public static Game g;
	public static int IdMap;

	// Use this for initialization
	void Start () {
		GameObject load = GameObject.Find ("MenuManager");
		aud = load.GetComponent<AudioSource> ();
		aud.Play ();
		g = new Persistence ().GetGame ();
		//only probe. After, I have to quit it.
		GameObject charged = GameObject.Find ("ChargedText");
		Text chargedText = charged.GetComponent<Text> ();
		chargedText.text = "Charged Data";
	}
	
	// Update is called once per frame
	void Update () {
		GameObject title = GameObject.Find ("AdventureLogo");
		Animator titleAnim = title.GetComponent<Animator>();
		titleAnim.Play ("AnimateTitle");
		GameObject button = GameObject.Find ("ButtonPlay");
		Animator buttonAnim = button.GetComponent<Animator>();
		buttonAnim.Play ("AnimateButton");
		new UICommon ().AnimateButtons (true);
	}

	public void Play()
	{
		Sound.GetSound("ButtonPlay");
		GameObject imageLog = GameObject.Find ("ImageLogIn");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("ShowLogin");	
	}

	public void LocalConnexion()
	{
		typeConnection = "local";
		this.Connect ();
	}

	public void FacebookConnexion()
	{
		typeConnection = "facebook";
		this.Connect ();
	}

	public void GoogleConnexion()
	{
		typeConnection = "google";
		this.Connect ();
	}
		
	public void CloseLogIn()
	{
		Sound.GetSound("CloseLogin");
		GameObject imageLog = GameObject.Find ("ImageLogIn");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveLogin");		
	}
		
	public void Exit()
	{
		Sound.GetSound ("ButtonDeparture");
		Application.Quit ();
	}
		
	private void Connect()
	{
		IdMap = 0;
		Sound.GetSound("ButtonLocal");
		GameObject imageLog = GameObject.Find ("ImageLogIn");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveLogin");	
		SceneManager.LoadScene ("Scenes/Map");
	}

	public void ResetValues()
	{
		//Reset to the default values to testing several times.
		Sound.GetSound("ResetValues");
	
		for (int i = 0; i < 3; i++) 
		{
			g.P.ListBonus[i].Count_bonus = 3;
			new Persistence ().UpdateBonus (1, 1, g.P.ListBonus [i]);
		}

		g.P.Heart.Count_lifes = 5;
		g.P.Heart.IsInfinite = false;
		
		new Persistence().UpdateHeart(1,1,g.P.Heart);

		g.P.Ingot.Coin_count = 33;

		new Persistence ().UpdateIngot (1, 1, g.P.Ingot);
		new Persistence ().DeleteAllNextLife (1, 1, 1);
		new Persistence().DeleteAllRegLevel(1,1,1);
		new Persistence().DeleteAllRegMap(1,1);

		g = new Persistence ().GetGame ();

		//only probe. After, I have to quit it.
		GameObject charged = GameObject.Find ("ChargedText");
		Text chargedText = charged.GetComponent<Text> ();
		chargedText.text = "The Data has been reset succesfully";
	}

}
