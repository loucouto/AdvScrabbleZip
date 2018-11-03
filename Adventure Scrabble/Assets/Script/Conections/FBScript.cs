using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FBScript : MonoBehaviour {
	
	public static string userName;
	//public static GameObject gImage;
	public static Texture2D gTexture;

	void Awake ()
	{
		if (!FB.IsInitialized) {
			FB.Init(InitCallback, OnHideUnity);
		} else {
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}

		if (FB.IsLoggedIn) {
			FB.API ("/me?fields=name", HttpMethod.GET, DispName);
			FB.API("me/picture?type=square&height=30&width=30", HttpMethod.GET, GetPicture);
		} else {
			Debug.Log("Please login to continue");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0; //pause
		} else {
			Time.timeScale = 1; //resume
		}
	}

	public void LoginWithFB(){
		var perms = new List<string>(){"public_profile"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	public void LogoutFromFB(){
		FB.LogOut (); 
	}
		
	private void AuthCallback (ILoginResult result) {
		if (result.Error != null) {
			Debug.Log (result.Error);

		} 
		else 
		{
			FB.API ("/me?fields=name", HttpMethod.GET, DispName);
			FB.API("me/picture?type=square&height=30&width=30", HttpMethod.GET, GetPicture);
		}
	}

	void DispName(IResult result){
		if (result.Error != null) {
			Debug.Log(result.Error);
		} 
		else 
		{
			userName = result.ResultDictionary ["name"].ToString();
		}
	}

	private void GetPicture(IGraphResult result)
	{
		//http://stackoverflow.com/questions/19756453/how-to-get-users-profile-picture-with-facebooks-unity-sdk
		if (result.Error == null && result.Texture != null) {       
			gTexture = new Texture2D (0, 0);
			gTexture = result.Texture;
			UIManagerMenu.typeConnection = "facebook";

			/*
			GameObject play = GameObject.Find ("ButtonLocal");
			AudioSource click = play.GetComponent<AudioSource>();

			if (soundActive == true) 
			{
				click.Play ();
			} 
			else 
			{
				if (click.isPlaying == true) 
				{
					click.Stop();
				}
			}*/

			GameObject imageLog = GameObject.Find ("ImageLogIn");
			Animator imageLog_anim = imageLog.GetComponent<Animator> ();
			imageLog_anim.Play ("LeaveLogin");	

			SceneManager.LoadScene ("Scenes/Map");
		} 
	}


}
