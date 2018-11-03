using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UICommon: MonoBehaviour {

	bool open = false;
	int page = 0;
	Button backButton;
	Button nextButton;
	Image pageImage;
	public static bool soundActive = true;
	public static bool musicActive = true;

	public void OpenMenu()
	{
		Sound.GetSound("ButtonMenu");
		GameObject panelMenu = GameObject.Find ("MenuPanel");
		Animator anim = panelMenu.GetComponent<Animator> ();

		if (open == false) 
		{
			anim.Play ("SlideIn");
			open = true;
		} 
		else 
		{
			anim.Play ("SlideOut");
			open = false;
		}

	}

	public void Help()
	{
		Sound.GetSound("ButtonQuestion");
		SetActiveControlsMap (false);
		GameObject imageLog = GameObject.Find ("ImageHelp");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("ShowHelp");	
	}

	public void About()
	{
		Sound.GetSound("ButtonAbout");
		GameObject imageLog = GameObject.Find ("ImageHelp");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveHelp");

		GameObject imageAbout = GameObject.Find ("ImageAbout");
		Animator imageAbout_anim = imageAbout.GetComponent<Animator> ();
		imageAbout_anim.Play ("ShowAbout");
	}

	public void HowPlay()
	{
		Sound.GetSound("HowPlay");
		GameObject imageLog = GameObject.Find ("ImageHelp");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveHelp");

		GameObject imageAbout = GameObject.Find ("ImageHowPlay");
		Animator imageAbout_anim =  imageAbout.GetComponent<Animator> ();
		imageAbout_anim.Play ("ShowHowPlay");
		this.ValueByDefault ();

	}

	public void CloseHelp()
	{
		Sound.GetSound("CloseHelp");
		GameObject imageLog = GameObject.Find ("ImageHelp");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveHelp");	
		SetActiveControlsMap (true);
	}

	public void CloseAboutUs()
	{
		Sound.GetSound("CloseAbout");
		GameObject imageLog = GameObject.Find ("ImageAbout");
		Animator imageLog_anim = imageLog.GetComponent<Animator> ();
		imageLog_anim.Play ("LeaveAbout");
		SetActiveControlsMap (true);
	}

	public void CloseHowToPlay()
	{
		Sound.GetSound("CloseHow");
		GameObject imageAbout = GameObject.Find ("ImageHowPlay");
		Animator imageAbout_anim =  imageAbout.GetComponent<Animator> ();
		imageAbout_anim.Play ("LeaveHowPlay");
		SetActiveControlsMap (true);
	}

	public void NextPage()
	{
		if (page < 6) {
			page++;
			Sound.GetSound ("Next");

			GameObject pageObject = GameObject.Find ("Page");
			pageImage = pageObject.GetComponent<Image> ();
			pageImage.sprite = Resources.Load<Sprite> ("HelpPages/Page" + (page + 1)) as Sprite;
			backButton.interactable = true;

			if (page == 6) 
			{
				GameObject next = GameObject.Find ("Next");
				AudioSource click = next.GetComponent<AudioSource>();
				click.Stop ();
				nextButton.interactable = false;
			}
		} 
	}

	public void BackPage()
	{
		if (page > 0) {
			page--;
			Sound.GetSound("Back");

			GameObject pageObject = GameObject.Find ("Page");
			pageImage = pageObject.GetComponent<Image> ();
			pageImage.sprite = Resources.Load<Sprite> ("HelpPages/Page" + (page + 1)) as Sprite;
			nextButton.interactable = true;

			if (page == 0) 
			{
				GameObject back = GameObject.Find ("Back");
				AudioSource click = back.GetComponent<AudioSource>();
				click.Stop ();
				backButton.interactable = false;
			}
		} 

	}

	public void ConfigureSound()
	{
		Sound.GetSound("ButtonSound");
		GameObject sound = GameObject.Find ("ButtonSound");
		Image soundImage = sound.GetComponent<Image> ();

		if (soundActive == true)
		{
			soundActive = false;
			soundImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonSoundProh") as Sprite;
		} 
		else 
		{
			soundActive = true;
			soundImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonSound") as Sprite;
		}
	}

	public void ConfigureMusic()
	{
		Sound.GetSound("ButtonMusic");
		GameObject music = GameObject.Find ("ButtonMusic");
		Image musicImage = music.GetComponent<Image> ();

		if (musicActive == true)
		{
			musicActive = false;
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusicProh") as Sprite;
			//aud.Stop ();
		} 
		else 
		{
			musicActive = true;
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusic") as Sprite;
			//aud.Play();
		}
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			PlayAudio ("MenuManager", musicActive);
		} else if (SceneManager.GetActiveScene ().name == "Map") {
			PlayAudio ("LoadMap", musicActive);
		} 
		else {
			PlayAudio ("PantallaUno", musicActive);
		}
	}


	public void SetSettings()
	{
		Sound.GetSound("ButtonSettings");
		SetActiveControlsMap (false);
		GameObject imageSettings = GameObject.Find ("ImageSettings");
		Animator animSettings = imageSettings.GetComponent<Animator> ();
		animSettings.Play("SlideSettingsIn");
	}

	public void CloseSettings()
	{
		Sound.GetSound("CloseSettings");
		GameObject imageSettings = GameObject.Find ("ImageSettings");
		Animator animSettings = imageSettings.GetComponent<Animator> ();
		animSettings.Play("SlideSettingsOut");
		SetActiveControlsMap (true);
	}

	public float ChargeTimer()
	{
		bool stop = false;
		int length = UIManagerMenu.g.P.Heart.List_next_life.Count;
		float startTime = 0f;

		if (UIManagerMenu.g.P.Heart.List_next_life.Count > 0) 
		{
			for (int i = 0; i < length && stop == false; i++) 
			{
				if (DateTime.Compare (DateTime.Now, UIManagerMenu.g.P.Heart.List_next_life[0].Date_next_life) < 0) 
				{
					startTime = (float)((UIManagerMenu.g.P.Heart.List_next_life [0].Date_next_life - DateTime.Now).Minutes * 1.0 * 60);
					stop = true;
				} 
				else 
				{
					UIManagerMenu.g.P.Heart.Count_lifes++;
					new Persistence ().DeleteNextLife(1,UIManagerMenu.g.P.Id_player,UIManagerMenu.g.P.Heart.Id_heart,UIManagerMenu.g.P.Heart.List_next_life[0]);
					new Persistence ().UpdateHeart (1, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);
					UIManagerMenu.g.P.Heart.List_next_life.RemoveAt (0);
				}
			}
		} 
		else 
		{
			if (UIManagerMenu.g.P.Heart.IsInfinite == true) 
			{
				if (DateTime.Compare (DateTime.Now, UIManagerMenu.g.P.Heart.Time_infinite) < 0) 
				{
					startTime = (float)((UIManagerMenu.g.P.Heart.Time_infinite - DateTime.Now).TotalHours * 1.0 * 60 * 60);
				} 
				else 
				{
					UIManagerMenu.g.P.Heart.Count_lifes = 5;
					UIManagerMenu.g.P.Heart.IsInfinite= false;
					UIManagerMenu.g.P.Heart.Time_infinite = new DateTime ();

					new Persistence ().UpdateHeart (1, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);
				}
			} 
			else 
			{
				startTime = 1200f;
			}
		}

		return startTime;
	
	}

	public void UpdateMinSec(ref bool pFirstOpen, ref float pStartTime, ref string pSeconds, ref string pMinutes)
	{
		if (pFirstOpen == true)
		{
			pStartTime =  pStartTime + Time.time;
			pFirstOpen = false;
		}

		float t = pStartTime - Time.time;

		pMinutes = ((int)t / 60).ToString ();
		pSeconds = (t % 60).ToString ("f0");

		if (int.Parse (pSeconds) == 60) {
			pSeconds = (int.Parse (pSeconds) - 1).ToString ();
		}

		if (int.Parse (pMinutes) < 10) {
			pMinutes = "0" + pMinutes;
		}

		if (int.Parse (pSeconds) < 10) {
			pSeconds = "0" + pSeconds;
		}
	
	}

	public void PayOneLife()
	{
		UIManagerMenu.g.P.Ingot.Coin_count = UIManagerMenu.g.P.Ingot.Coin_count - 5;
		UIManagerMenu.g.P.Heart.Count_lifes++;
		UIManagerMenu.g.P.Heart.List_next_life.RemoveAt (UIManagerMenu.g.P.Heart.List_next_life.Count - 1);
		new Persistence ().DeleteNextLife (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart.Id_heart, UIManagerMenu.g.P.Heart.List_next_life [0]);
		new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);
		new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
	}

	public float PayExtraInfiniteLife(int pPrice, int pHourLife)
	{
		float startTime = 0f;
		UIManagerMenu.g.P.Ingot.Coin_count = UIManagerMenu.g.P.Ingot.Coin_count - pPrice;
		UIManagerMenu.g.P.Heart.IsInfinite = true;
		UIManagerMenu.g.P.Heart.Time_infinite = DateTime.Now.AddHours (pHourLife);
		UIManagerMenu.g.P.Heart.List_next_life.Clear();
		new Persistence ().DeleteAllNextLife (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart.Id_heart);
		new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);
		new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
		startTime = 3600f * pHourLife;
		return startTime;
	}

	public void SetActiveControls(bool pIsActive)
	{
		LoadMap.isPlay = !pIsActive;
		LoadMap.panel.SetActive (pIsActive);
		LoadMap.backGroundBar.SetActive (pIsActive);
	}

	public void SetActiveControlsMap(bool pIsActive)
	{
		if(SceneManager.GetActiveScene().name == "Map")
		{
			SetActiveControls (pIsActive);
		}
	
	}

	private void ValueByDefault()
	{
		page = 0;
		GameObject buttonBack = GameObject.Find ("Back");
		backButton = buttonBack.GetComponent<Button> ();
		backButton.interactable = false;

		GameObject buttonNext = GameObject.Find ("Next");
		nextButton = buttonNext.GetComponent<Button> ();
	}

	private void PlayAudio(string pObjName, bool pIsActive)
	{
		GameObject obj = GameObject.Find (pObjName);
		AudioSource a = obj.GetComponent<AudioSource> ();

		if (pIsActive == true)
		{
			a.Play ();
		} 
		else 
		{
			a.Stop ();
		}
	}
		
	public void AnimateButtons(bool pIsMainMenu)
	{
		GameObject howToPlay = GameObject.Find ("HowPlay");
		Animator howAnim = howToPlay.GetComponent<Animator>();
		howAnim.Play ("AnimateButton");
		GameObject closeHelp = GameObject.Find ("CloseHelp");
		Animator closeHelpAnim = closeHelp.GetComponent<Animator> ();
		closeHelpAnim.Play ("AnimateButton");
		GameObject buttonAbout = GameObject.Find ("ButtonAbout");
		Animator buttonAboutAnim = buttonAbout.GetComponent<Animator> ();
		buttonAboutAnim.Play("AnimateButton");
		GameObject closeAbout = GameObject.Find ("CloseAbout");
		Animator closeAboutAnim = closeAbout.GetComponent<Animator> ();
		closeAboutAnim.Play("AnimateButton");
		GameObject back = GameObject.Find ("Back");
		Animator backAnim = back.GetComponent<Animator> ();
		backAnim.Play("AnimateButton");
		GameObject next = GameObject.Find ("Next");
		Animator nextAnim = back.GetComponent<Animator> ();
		nextAnim.Play("AnimateButton");
		GameObject closeHow = GameObject.Find ("CloseHow");
		Animator closeHowAnim = back.GetComponent<Animator> ();
		closeHowAnim.Play("AnimateButton");
		GameObject closeSettings = GameObject.Find ("CloseSettings");
		Animator closeSettingsAnim = back.GetComponent<Animator> ();
		closeSettingsAnim.Play("AnimateButton");
		GameObject buttonMenu = GameObject.Find ("ButtonMenu");
		Animator buttonMenuAnim = back.GetComponent<Animator> ();
		buttonMenuAnim.Play("AnimateButton");

		if (pIsMainMenu == true) {

			GameObject closeLogin = GameObject.Find ("CloseLogin");
			Animator closeLoginAnim = closeLogin.GetComponent<Animator> ();
			closeLoginAnim.Play("AnimateButton");
			GameObject buttonFacebook = GameObject.Find ("ButtonFacebook");
			Animator buttonFacebookAnim = buttonFacebook.GetComponent<Animator>();
			buttonFacebookAnim.Play("AnimateButton");
			GameObject buttonGoogle = GameObject.Find ("ButtonGoogle");
			Animator buttonGoogleAnim = buttonGoogle.GetComponent<Animator> ();
			buttonGoogleAnim.Play("AnimateButton");
			GameObject buttonLocal = GameObject.Find ("ButtonLocal");
			Animator buttonLocalAnim = buttonLocal.GetComponent<Animator> ();
			buttonLocalAnim.Play("AnimateButton");
		}
	}

	public void AnimateButtonsMap()
	{
		GameObject closeStadistics = GameObject.Find ("CloseStadistics");
		Animator closeStadisticsAnim = closeStadistics.GetComponent<Animator>();
		closeStadisticsAnim.Play ("AnimateButton");
		GameObject shareStadistics = GameObject.Find ("ShareStadistics");
		Animator shareStadisticsAnim = shareStadistics.GetComponent<Animator>();
		shareStadisticsAnim.Play ("AnimateButton");
		GameObject buttonSales = GameObject.Find ("ButtonSales");
		Animator buttonSalesAnim = buttonSales.GetComponent<Animator>();
		buttonSalesAnim.Play ("AnimateButton");
		GameObject buttonAskForLife = GameObject.Find ("ButtonAskForLife");
		Animator buttonAskAnim = buttonAskForLife.GetComponent<Animator>();
		buttonAskAnim.Play ("AnimateButton");
		GameObject buttonSixHours = GameObject.Find ("ButtonSixHours");
		Animator buttonSixAnim = buttonSixHours.GetComponent<Animator>();
		buttonSixAnim.Play ("AnimateButton");
		GameObject buttonTwoHours = GameObject.Find ("ButtonTwoHours");
		Animator buttonTwoHoursAnim = buttonTwoHours.GetComponent<Animator>();
		buttonTwoHoursAnim.Play ("AnimateButton");
		if (UIManagerMenu.g.P.Heart.Count_lifes < 5) {
			GameObject buttonOneLife = GameObject.Find ("ButtonOneLife");
			Animator buttonOneLifeAnim = buttonOneLife.GetComponent<Animator> ();
			buttonOneLifeAnim.Play ("AnimateButton");
		}
		GameObject buttonCloseLife = GameObject.Find ("ButtonCloseLife");
		Animator closeAnim = buttonCloseLife.GetComponent<Animator>();
		closeAnim.Play ("AnimateButton");
		GameObject shareAchReached = GameObject.Find ("ShareAchReached");
		Animator achReachedAnim = shareAchReached.GetComponent<Animator>();
		achReachedAnim.Play ("AnimateButton");
		GameObject closeAchReached = GameObject.Find ("CloseAchReached");
		Animator closeAchAnim = closeAchReached.GetComponent<Animator>();
		closeAchAnim.Play ("AnimateButtonPlay");
		GameObject buttonCloseReg = GameObject.Find ("ButtonCloseReg");
		Animator closeRegAnim = buttonCloseReg.GetComponent<Animator>();
		closeRegAnim.Play ("AnimateButton");
		GameObject accept = GameObject.Find ("Accept");
		Animator acceptAnim = accept.GetComponent<Animator>();
		acceptAnim.Play ("AnimateButton");
		GameObject buttonCloseMessanger = GameObject.Find ("ButtonCloseMessanger");
		Animator closeMessAnim = buttonCloseMessanger.GetComponent<Animator>();
		closeMessAnim.Play ("AnimateButton");
		GameObject acceptMess1 = GameObject.Find ("AcceptMess1");
		Animator acceptMess1Anim = acceptMess1.GetComponent<Animator>();
		acceptMess1Anim.Play ("AnimateButton");
		GameObject acceptMess2 = GameObject.Find ("AcceptMess2");
		Animator acceptMess2Anim = acceptMess2.GetComponent<Animator>();
		acceptMess2Anim.Play ("AnimateButton");
		GameObject acceptMess3 = GameObject.Find ("AcceptMess3");
		Animator acceptMess3Anim = acceptMess1.GetComponent<Animator>();
		acceptMess3Anim.Play ("AnimateButton");
		GameObject deleteMess1 = GameObject.Find ("DeleteMess1");
		Animator deleteMessAnim1 = deleteMess1.GetComponent<Animator>();
		deleteMessAnim1.Play ("AnimateButton");
		GameObject deleteMess2 = GameObject.Find ("DeleteMess2");
		Animator deleteMessAnim2 = deleteMess1.GetComponent<Animator>();
		deleteMessAnim2.Play ("AnimateButton");
		GameObject deleteMess3 = GameObject.Find ("DeleteMess3");
		Animator deleteMessAnim3 = deleteMess1.GetComponent<Animator>();
		deleteMessAnim3.Play ("AnimateButton");
		GameObject write = GameObject.Find ("Write");
		Animator writeAnim = write.GetComponent<Animator>();
		writeAnim.Play ("AnimateButton");
		GameObject sendPresent = GameObject.Find ("SendPresents");
		Animator sendPresentAnim = sendPresent.GetComponent<Animator>();
		sendPresentAnim.Play ("AnimateButton");
		GameObject askFor = GameObject.Find ("AskFor");
		Animator askForAnim = askFor.GetComponent<Animator>();
		if (LoadMap.isPlay == false) 
		{
			askForAnim.Play ("AnimateButton");
			GameObject heart = GameObject.Find ("Heart");
			Animator heartAnim = heart.GetComponent<Animator> ();
			heartAnim.Play ("AnimateButton");
			GameObject ingot = GameObject.Find ("Ingot");
			Animator ingotAnim = ingot.GetComponent<Animator> ();
			ingotAnim.Play ("AnimateButton");
			GameObject messages = GameObject.Find ("Message");
			Animator messagesAnim = messages.GetComponent<Animator> ();
			messagesAnim.Play ("AnimateButton");
			GameObject stadistics = GameObject.Find ("Stadistics");
			Animator stadisticsAnim = stadistics.GetComponent<Animator>();
			stadisticsAnim.Play ("AnimateButton");
		}
		GameObject buttonPlay = GameObject.Find ("ButtonPlay");
		Animator buttonPlayAnim = buttonPlay.GetComponent<Animator>();
		buttonPlayAnim.Play ("AnimateButton");
		GameObject buttonClosePlay = GameObject.Find ("ButtonClosePlay");
		Animator buttonClosePlayAnim = buttonClosePlay.GetComponent<Animator>();
		buttonClosePlayAnim.Play ("AnimateButton");
		GameObject buttonN1Play = GameObject.Find ("ButtonN1Play");
		Animator buttonN1PlayAnim = buttonN1Play.GetComponent<Animator>();
		buttonN1PlayAnim.Play ("AnimateButton");
		GameObject buttonMePlay = GameObject.Find ("ButtonMePlay");
		Animator buttonMePlayAnim = buttonMePlay.GetComponent<Animator>();
		buttonMePlayAnim.Play ("AnimateButton");
		GameObject buttonAskForPlay = GameObject.Find ("ButtonAskForPlay");
		Animator buttonAskForPlayAnim= buttonAskForPlay.GetComponent<Animator>();
		buttonAskForPlayAnim.Play ("AnimateButton");
		GameObject sendPresents = GameObject.Find ("SendPresents");
		Animator sendPresentsAnim = sendPresents.GetComponent<Animator>();
		sendPresentsAnim.Play ("AnimateButton");
		GameObject askFor2 = GameObject.Find ("AskFor");
		Animator askFor2Anim = askFor.GetComponent<Animator>();
		askFor2Anim.Play ("AnimateButton");
		GameObject buttonExchange = GameObject.Find ("ButtonExchange");
		Animator buttonExchangeAnim = buttonExchange.GetComponent<Animator>();
		buttonExchangeAnim.Play ("AnimateButton");
		GameObject plusSign = GameObject.Find ("PlusSign");
		Animator plusSingAnim = plusSign.GetComponent<Animator>();
		plusSingAnim.Play ("ShowPlusSign");

	}

	public void AnimateButtonsLevel()
	{
		GameObject giveUp = GameObject.Find ("ButtonGiveUp");
		Animator giveUpAnim = giveUp.GetComponent<Animator>();
		giveUpAnim.Play ("AnimateButton");
		GameObject cont = GameObject.Find ("ButtonContinue");
		Animator contAnim = cont.GetComponent<Animator>();
		contAnim.Play ("AnimateButton");
		GameObject yes = GameObject.Find ("ButtonYes");
		Animator yesAnim = yes.GetComponent<Animator>();
		yesAnim.Play ("AnimateButton");
		GameObject no = GameObject.Find ("ButtonNo");
		Animator noAnim = no.GetComponent<Animator>();
		noAnim.Play ("AnimateButton");
		GameObject buttonBonus = GameObject.Find ("ButtonBonus");
		Animator buttonBonusAnim = buttonBonus.GetComponent<Animator>();
		buttonBonusAnim.Play ("AnimateButton");
		GameObject ImageBonus = GameObject.Find ("ImageBonus");
		Animator ImageBonusAnim = ImageBonus.GetComponent<Animator>();
		ImageBonusAnim.Play ("AnimateButton");
		GameObject closeBonus = GameObject.Find ("CloseBonus");
		Animator closeBonusAnim = closeBonus.GetComponent<Animator>();
		closeBonusAnim.Play ("AnimateButton");
		GameObject closeWin = GameObject.Find ("CloseWin");
		Animator closeWinAnim = closeWin.GetComponent<Animator>();
		closeWinAnim.Play ("AnimateButton");
		GameObject buttonNext = GameObject.Find ("ButtonNext");
		Animator buttonNextAnim = buttonNext.GetComponent<Animator>();
		buttonNextAnim.Play ("AnimateButton");
		GameObject buttonPlayAgain = GameObject.Find ("ButtonPlayAgain");
		Animator buttonPlayAgainAnim = buttonPlayAgain.GetComponent<Animator>();
		buttonPlayAgainAnim.Play ("AnimateButton");
		GameObject buttonN1Comp = GameObject.Find ("ButtonN1Comp");
		Animator buttonN1CompAnim = buttonN1Comp.GetComponent<Animator>();
		buttonN1CompAnim.Play ("AnimateButton");
		GameObject buttonMeComp = GameObject.Find ("ButtonMeComp");
		Animator buttonMeCompAnim = buttonMeComp.GetComponent<Animator>();
		buttonMeCompAnim.Play ("AnimateButton");
		GameObject buttonAskForComp = GameObject.Find ("ButtonAskForComp");
		Animator buttonAskForCompAnim = buttonAskForComp.GetComponent<Animator>();
		buttonAskForCompAnim.Play ("AnimateButton");
		GameObject buttonCloseTryAgain = GameObject.Find ("ButtonCloseTryAgain");
		Animator buttonCloseTryAgainAnim = buttonCloseTryAgain.GetComponent<Animator>();
		buttonCloseTryAgainAnim.Play ("AnimateButton");
		GameObject buttonTryAgain = GameObject.Find ("ButtonTryAgain");
		Animator buttonTryAgainAnim = buttonTryAgain.GetComponent<Animator>();
		buttonTryAgainAnim.Play ("AnimateButton");
		GameObject buttonN1Try = GameObject.Find ("ButtonN1Try");
		Animator buttonN1TryAnim = buttonN1Try.GetComponent<Animator>();
		buttonN1TryAnim.Play ("AnimateButton");
		GameObject buttonAskForTry = GameObject.Find ("ButtonAskForTry");
		Animator buttonAskForTryAnim = buttonAskForTry.GetComponent<Animator>();
		buttonAskForTryAnim.Play ("AnimateButton");
		GameObject imageHeartLife = GameObject.Find ("ImageHeartLife");
		Animator imageHeartLifeAnim = buttonAskForTry.GetComponent<Animator>();
		imageHeartLifeAnim.Play ("AnimateButton");
	}

	public void AnimateButtonsMapLevel()
	{
		GameObject closeSales = GameObject.Find ("CloseSales");
		Animator closeSalesAnim = closeSales.GetComponent<Animator>();
		closeSalesAnim.Play ("AnimateButton");
		GameObject buttonNine = GameObject.Find ("ButtonNine");
		Animator buttonNineAnim = buttonNine.GetComponent<Animator>();
		buttonNineAnim.Play ("AnimateButton");
		GameObject buttonUndo = GameObject.Find ("ButtonUndo");
		Animator buttonUndoAnim = buttonUndo.GetComponent<Animator>();
		buttonUndoAnim.Play ("AnimateButton");
		GameObject buttonPerThree = GameObject.Find ("ButtonPerThree");
		Animator buttonPerThreeAnim = buttonPerThree.GetComponent<Animator>();
		buttonPerThreeAnim.Play ("AnimateButton");
		GameObject buttonJocker = GameObject.Find ("ButtonJocker");
		Animator buttonJockerAnim = buttonJocker.GetComponent<Animator>();
		buttonJockerAnim.Play ("AnimateButton");
		GameObject buttonHeart = GameObject.Find ("ButtonHeart");
		Animator buttonHeartAnim = buttonHeart.GetComponent<Animator>();
		buttonHeartAnim.Play ("AnimateButton");
		GameObject buttonTen = GameObject.Find ("ButtonTen");
		Animator buttonTenAnim = buttonTen.GetComponent<Animator>();
		buttonTenAnim.Play ("AnimateButton");
		GameObject buttonTwenty = GameObject.Find ("ButtonTwenty");
		Animator buttonTwentyAnim = buttonTwenty.GetComponent<Animator>();
		buttonTwentyAnim.Play ("AnimateButton");
		GameObject buttonFifty = GameObject.Find ("ButtonFifty");
		Animator buttonFiftyAnim = buttonFifty.GetComponent<Animator>();
		buttonFiftyAnim.Play ("AnimateButton");
		GameObject buttonHundred = GameObject.Find ("ButtonHundred");
		Animator buttonHundredAnim = buttonHundred.GetComponent<Animator>();
		buttonHundredAnim.Play ("AnimateButton");
		GameObject buttonTwoHundredFifty = GameObject.Find ("ButtonTwoHundredFifty");
		Animator TwoHundredFiftyAnim = buttonTwoHundredFifty.GetComponent<Animator>();
		TwoHundredFiftyAnim.Play ("AnimateButton");
		GameObject buttonFiveHundred = GameObject.Find ("ButtonFiveHundred");
		Animator buttonFiveHundredAnim = buttonFiveHundred.GetComponent<Animator>();
		buttonFiveHundredAnim.Play ("AnimateButton");
		GameObject buttonOneThousand = GameObject.Find ("ButtonOneThousand");
		Animator buttonOneThousandAnim = buttonOneThousand.GetComponent<Animator>();
		buttonOneThousandAnim.Play ("AnimateButton");
		GameObject buttonJunior = GameObject.Find ("ButtonJunior");
		Animator buttonJuniorAnim = buttonJunior.GetComponent<Animator>();
		buttonJuniorAnim.Play ("AnimateButton");
		GameObject buttonSenior = GameObject.Find ("ButtonSenior");
		Animator buttonSeniorAnim = buttonSenior.GetComponent<Animator>();
		buttonSeniorAnim.Play ("AnimateButton");
		GameObject buttonExpert = GameObject.Find ("ButtonExpert");
		Animator buttonExpertAnim = buttonExpert.GetComponent<Animator>();
		buttonExpertAnim.Play ("AnimateButton");
		GameObject buttonMaster = GameObject.Find ("ButtonMaster");
		Animator buttonMasterAnim = buttonMaster.GetComponent<Animator>();
		buttonMasterAnim.Play ("AnimateButton");
	}
		
}
