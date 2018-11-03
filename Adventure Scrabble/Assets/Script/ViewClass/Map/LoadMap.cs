using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadMap : MonoBehaviour {

	public static GameObject panel;
	public static GameObject backGroundBar;
	public static GameObject buttonOneLife;
	public static GameObject textOneLife;
	public static GameObject imageOneLife;
	public static GameObject backLevelsMap;
	public static GameObject mapRegPanel;
	public static GameObject achievementGrid;
	public static bool isPlay = false;
	private static bool IsCheckJocker = false;
	public static bool first_open = true;
	public static float startTime;
	private static float restHeigh = 0;
	private static int indexAch = 0;
	private static int indexMap = 0;
	private static int indexRegLevel = 0;
	private static int indexAchReached = 1;
	public static int levelGo;
	public static AudioSource a;
	public static List<Achievement> listCompleteAch = new List<Achievement>();
	Animator anim;
	private string minutes = "";
	private string seconds = "";
	GameObject picture;
	GameObject levelStart;

	// Use this for initialization
	void Start () {

		GameObject cursorBar = GameObject.Find ("CursorBar");
		BoxCollider2D cursorBox = cursorBar.GetComponent<BoxCollider2D> ();
		cursorBox.enabled = true;

		GameObject load = GameObject.Find ("LoadMap");
		GameObject music = GameObject.Find ("ButtonMusic");
		GameObject sound = GameObject.Find ("ButtonSound");
		a = load.GetComponent<AudioSource> ();
		Image musicImage = music.GetComponent<Image> ();
		Image soundImage = sound.GetComponent<Image> ();

		if (UIManagerMenu.musicActive == true) 
		{
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusic") as Sprite;
			a.Play();
		}
		else 
		{
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusicProh") as Sprite;

			if (a.isPlaying == true) 
			{
				a.Stop ();
			}
		}

		if (UICommon.soundActive == true) 
		{
			soundImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonSound") as Sprite;
		}
		else 
		{
			soundImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonSoundProh") as Sprite;
		}

		buttonOneLife = GameObject.Find ("ButtonOneLife");
		textOneLife = GameObject.Find ("TextOneLife");
		imageOneLife = GameObject.Find ("ImageOneLife");
		backLevelsMap = GameObject.Find ("BackLevelsMap");
		mapRegPanel = GameObject.Find ("MapRegPanel");
		achievementGrid = GameObject.Find ("AchievementsGrid");

		panel = GameObject.Find ("Panel");
		backGroundBar = GameObject.Find ("BackGroundBar");

		if (UIManagerMenu.g.P.Name == "") 
		{
			if (UIManagerMenu.typeConnection == "facebook") 
			{
				UIManagerMenu.g.P = new Player ();
				UIManagerMenu.g.P.Name = FBScript.userName;
				ChargeData ();
				UIManagerMenu.g.P.ListAchievements[0].Count++;
			} 
			else 
			{
				new UICommon ().SetActiveControls (false);
				GameObject imageReg = GameObject.Find ("ImageRegister");
				Animator imageReg_anim = imageReg.GetComponent<Animator> ();
				imageReg_anim.Play ("ShowRegister");
			}
		} 
		else 
		{
			ChargeData ();
		}
			
		if (UIManager.goNext == true) 
		{
			if (UIManagerMenu.g.P.ListRegMap.Count == 0) {
				RegisterMap rm = new RegisterMap ();
				rm.Map = new Map ();
				rm.Map.Id_map = UIManagerMenu.IdMap + 1;
				rm.Score_map += PieceDrag.total_score_gral;
				new Persistence ().InsertRegMap (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, rm);
			} 
			else 
			{
				UIManagerMenu.g.P.ListRegMap[UIManagerMenu.IdMap].Score_map += PieceDrag.total_score_gral;
				new Persistence ().UpdateRegMap (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListRegMap[UIManagerMenu.IdMap]);
			}

			UIManagerMenu.g.P.ListRegMap = new Persistence ().ListRegisterMap (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player);

			RegisterLevel regLevel = new RegisterLevel ();
			regLevel.Level = new Level ();
			regLevel.Level.Id_level = levelGo;
			regLevel.Score = PieceDrag.total_score_gral;
			regLevel.Count_stars = UIManager.countStars;

			new Persistence ().InsertRegLevel (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListRegMap [UIManagerMenu.IdMap].Id_register_map, UIManagerMenu.IdMap + 1,regLevel);

			UIManagerMenu.g = new Persistence ().GetGame ();
			this.GoToNextLevel ();
			UIManager.goNext = false;
		
		}
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPlay == false) {
			GameObject timeLife = GameObject.Find ("TimeLife");
			Text timeLifeText = timeLife.GetComponent<Text> ();

			GameObject timeLifeCero = GameObject.Find ("TextNextLife");
			Text timeLifeTextCero = timeLifeCero.GetComponent<Text> ();

			GameObject countLife = GameObject.Find("CountLife");
			Text countLifeText = countLife.GetComponent<Text> ();

			if (UIManagerMenu.g.P.Heart.IsInfinite == true) 
			{
				if(startTime >= 3600f)
				{
					timeLifeText.text = Convert.ToInt32(startTime/3600).ToString() + " hs";
					countLifeText.text = "∞";
					timeLifeTextCero.text = Convert.ToInt32(startTime/3600).ToString() + " hs";
				}
				else
				{
					if (minutes == "00" && seconds == "00") 
					{
						UIManagerMenu.g.P.Heart.Count_lifes = 5;
						UIManagerMenu.g.P.Heart.IsInfinite = false;
						UIManagerMenu.g.P.Heart.Time_infinite = new DateTime ();

						new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);

						countLifeText.text = UIManagerMenu.g.P.Heart.Count_lifes.ToString();
					}
					else 
					{
						countLifeText.text = "∞";

						new UICommon ().UpdateMinSec (ref first_open, ref startTime, ref seconds, ref minutes);

						timeLifeText.text = minutes + ":" + seconds;
						timeLifeTextCero.text = minutes + ":" + seconds;
					}
				}
			}
			else if (UIManagerMenu.g.P.Heart.Count_lifes == 5 && UIManagerMenu.g.P.Heart.IsInfinite == false) {

				timeLifeText.text = "Full";
				timeLifeTextCero.text = "Full";
			} 
			else
			{
				if (minutes == "00" && seconds == "00") {
					UIManagerMenu.g.P.Heart.Count_lifes++;
					new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart); 
					new Persistence ().DeleteNextLife (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart.Id_heart, UIManagerMenu.g.P.Heart.List_next_life [0]);
					UIManagerMenu.g.P.Heart.List_next_life.RemoveAt (0);
					if (UIManagerMenu.g.P.Heart.Count_lifes < 5) {
						startTime = Time.time + 1200f;
						minutes = "";
						seconds = "";
					} 
						
					//countLifeText.text = UIManagerMenu.g.P.Heart.Count_lifes.ToString();
				}
				else 
				{
					new UICommon ().UpdateMinSec (ref first_open, ref startTime,ref seconds,ref minutes);

					timeLifeText.text = minutes + ":" + seconds;
					timeLifeTextCero.text = minutes + ":" + seconds;  
				}
				countLifeText.text = UIManagerMenu.g.P.Heart.Count_lifes.ToString();
			}


			GameObject selectedLevel = GameObject.Find ("SelectedLevel");
			Animator selectedLevelanim = selectedLevel.GetComponent<Animator> ();
			selectedLevelanim.Play ("ShowSelectedLevel");
		}

		new UICommon ().AnimateButtons (false);
		new UICommon ().AnimateButtonsMap ();
		new UICommon ().AnimateButtonsMapLevel ();
	}
		
	private void ChargeData()
	{
	    picture = GameObject.Find ("Photo");
		Image photo = picture.GetComponent<Image> ();

		if (UIManagerMenu.typeConnection == "facebook") 
		{
			photo.sprite = Sprite.Create (FBScript.gTexture, new Rect (0, 0, 30, 30), new Vector2 (0, 0));
		} 
		else 
		{
			photo.sprite = Resources.Load<Sprite> ("Profiles/Fox") as Sprite;
		}

		UIManagerMenu.g.P.Picture = photo.sprite;

		GameObject circleIngot = GameObject.Find ("CountIngot");
		Text ingotText = circleIngot.GetComponent<Text> ();
		ingotText.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString ();

		startTime = new UICommon ().ChargeTimer ();

		this.GoToNextLevel ();
		//GetWonPrize ();
	}
		
	private void GetWonPrize()
	{
		for (int i = 0; i < 71; i++)
		{
			if (i > 0 && i < 7) 
			{
				if (UIManagerMenu.g.P.ListRegMap.Count > 0) {
					UIManagerMenu.g.P.ListAchievements[i].Count = UIManagerMenu.g.P.ListRegMap [0].ListRegLevel.Count;
				}
			} 
			else if (i == 7) 
			{
				if ((levelGo - 1) > 1) {
					UIManagerMenu.g.P.ListAchievements [i].Count += UIManagerMenu.g.P.ListStateLevel [levelGo - 1].Times_lost;
				}
			}
			else if(i>38 && i<47)
			{
				UIManagerMenu.g.P.ListAchievements [i].Count = GetWonLevelsAtOnce();
			}
			else if(i>66 && i<71)
			{
				UIManagerMenu.g.P.ListAchievements [i].Count = GetWonFollowedLevels ();
			}
			//TODO podio else if

			UIManagerMenu.g.P.ListAchievements [i].Percentaje = (double)((UIManagerMenu.g.P.ListAchievements [i].Count / UIManagerMenu.g.P.ListAchievements [i].Max_count) * 100);
			if(UIManagerMenu.g.P.ListAchievements [i].Percentaje >= 100 && UIManagerMenu.g.P.ListAchievements [i].Is_earned == false)
			{
				UIManagerMenu.g.P.ListAchievements[i].Is_earned = true;
				UIManagerMenu.g.P.Ingot.Coin_count += UIManagerMenu.g.P.ListAchievements[i].Prize;
				listCompleteAch.Add (UIManagerMenu.g.P.ListAchievements[i]);
			}
		}
	
		UpdatePrizeIngot ();
	}
		
	private int GetWonLevelsAtOnce()
	{
		int val = 0;
		for (int i = 0; i < UIManagerMenu.g.P.ListStateLevel.Count; i++) 
		{
			if (UIManagerMenu.g.P.ListStateLevel [i].Id_state == 1)
			{
				val++;
			}
		}

		return val;
	}

	private int GetWonFollowedLevels()
	{	
		int val = 1;
		if (levelGo > 1) {
			int i = (levelGo - 1);
			while (i >= 0 && UIManagerMenu.g.P.ListStateLevel [i].Id_state == 1) {
				val++;
				i--;
			}
		}
		return val;
	}

	private void UpdatePrizeIngot()
	{
		new Persistence ().UpdateListAchievements (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListAchievements);

		if (listCompleteAch.Count > 0) {
			new Persistence ().UpdateIngot (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
			GameObject countIngot = GameObject.Find ("CountIngot");
			Text countIngotText = countIngot.GetComponent<Text> ();
			countIngotText.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString ();

			ShowPrize (listCompleteAch [0]);
		}
	}

	IEnumerator CoRoutineShowExplosion()
	{
		GameObject explosion = GameObject.Find ("Explosion");
		Image explosionImage = explosion.GetComponent<Image> ();
		Animator explosionAnimation = explosion.GetComponent<Animator> ();
		explosionImage.enabled = true;
		explosionAnimation.Play ("ShowExplosion");

		yield return new WaitForSeconds (1f);

		explosionImage.enabled = false;
	}

	public void AceptRegister()
	{
		Sound.GetSound("Accept");
		GameObject boxText = GameObject.Find ("TextPlayer");
		Text playerText = boxText.GetComponent<Text> ();

		if (playerText.text == "") 
		{
			GameObject message = GameObject.Find ("ErrorMessage");
			Text errorText = message.GetComponent<Text> ();
			errorText.text = "You must to enter a player name";
		} 
		else 
		{
			UIManagerMenu.g.P.Name = playerText.text;  
			GameObject imageReg = GameObject.Find ("ImageRegister");
			Animator imageReg_anim = imageReg.GetComponent<Animator> ();
			imageReg_anim.Play ("LeaveRegister");
			new Persistence ().UpdatePlayer (UIManagerMenu.g.Id_game, UIManagerMenu.g.P);
			this.ChargeData ();
			new UICommon ().SetActiveControls (true);

			/*GameObject selectLevel = GameObject.Find ("SelectedLevel");
			RectTransform rectPost = selectLevel.GetComponent<RectTransform> ();
			Vector2 postSelect = new Vector2 (-458.7f, -347.9f);
			rectPost.anchoredPosition = postSelect;*/
		}

	}

	public void ShowLevel()
	{
		GameObject cursorBar = GameObject.Find ("CursorBar");
		BoxCollider2D cursorBox = cursorBar.GetComponent<BoxCollider2D> ();
		cursorBox.enabled = false;

		Sound.GetSound("Level" + LoadMap.levelGo);
		new UICommon ().SetActiveControls (false);

		if (UIManagerMenu.g.P.Heart.Count_lifes > 0) {
			isPlay = true;
			GameObject textLevel = GameObject.Find ("TextLevel");
			levelStart = GameObject.Find ("ImageLevelStart");
			Text textLevelText = textLevel.GetComponent<Text>();
			textLevelText.text = "Level" + LoadMap.levelGo;
			anim = levelStart.GetComponent<Animator> ();
			anim.Play ("ShowLevel");
		}
		else 
		{
			GameObject ImageLife = GameObject.Find ("ImageLife");
			Animator ImageLife_anim = ImageLife.GetComponent<Animator> ();
			ImageLife_anim.Play ("ShowLife");
		}
	}
	public void Play()
	{
		Sound.GetSound("ButtonPlay");
		GameObject checkJocker = GameObject.Find ("CheckJoker");
		Toggle checkJockerToggle = checkJocker.GetComponent<Toggle> ();
		if(checkJockerToggle.isOn == true)
		{
			IsCheckJocker = true;
		}
		else
		{
			IsCheckJocker = false;
		}
		ListPieces.first_time = false;
		ListPieces.first_back = false;
		LoadMap.first_open = true;
		anim.Play ("LeaveLevel");
		SceneManager.LoadScene ("Scenes/PantallaUno");
	}

	public void LookForSales()
	{
		this.ShowSales();
	}

	public void CloseLevel()
	{
		StartCoroutine ("CoRoutineLeaveLevel");
	}
 
	public void CloseLife()
	{
		Sound.GetSound("ButtonCloseLife");
		GameObject ImageLife = GameObject.Find ("ImageLife");
		Animator ImageLife_anim = ImageLife.GetComponent<Animator> ();
		ImageLife_anim.Play ("LeaveLife");
		new UICommon ().SetActiveControls (true);
	}

	public void CloseReg()
	{
		Sound.GetSound("ButtonCloseReg");
		GameObject imageReg = GameObject.Find ("ImageRegister");
		Animator imageReg_anim = imageReg.GetComponent<Animator> ();
		imageReg_anim.Play ("LeaveRegister");
		SceneManager.LoadScene ("Scenes/MainMenu");
	}

	public void CloseSales()
	{
		Sound.GetSound("CloseSales");
		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("LeaveSales");
		new UICommon ().SetActiveControls (true);
	}

	public void GoOut()
	{
		Sound.GetSound("ButtonDeparture");
		new Persistence ().UpdateListBonus (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);
		new Persistence ().UpdateIngot (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
		new Persistence ().UpdatePlayer (UIManagerMenu.g.Id_game, UIManagerMenu.g.P);
		SceneManager.LoadScene ("Scenes/MainMenu");
	}

	public void ShowLife()
	{
		Sound.GetSound("Heart");
		new UICommon ().SetActiveControls (false);
		GameObject imageLife = GameObject.Find ("ImageLife");
		Animator imageLife_anim = imageLife.GetComponent<Animator> ();
		imageLife_anim.Play ("ShowLife");

		if (UIManagerMenu.g.P.Heart.Count_lifes == 5 || UIManagerMenu.g.P.Heart.IsInfinite == true) {

			LoadMap.buttonOneLife.SetActive (false);
			LoadMap.textOneLife.SetActive (false);
			LoadMap.imageOneLife.SetActive (false);
		} 
		else 
		{
			LoadMap.buttonOneLife.SetActive (true);
			LoadMap.textOneLife.SetActive (true);
			LoadMap.imageOneLife.SetActive (true);
		}
	}

	public void ShowSales()
	{
		Sound.GetSound("Ingot");
		new UICommon ().SetActiveControls (false);
		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("ShowSales");

		GameObject countIngotSales = GameObject.Find ("CountIngotSales");
		Text countIngot = countIngotSales.GetComponent<Text> ();
		countIngot.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();
	}

	public void ShowMessage()
	{
		GameObject messanger = GameObject.Find ("ImageMessage");
		Animator anim_messanger = messanger.GetComponent<Animator> ();
		anim_messanger.Play ("ShowMessanger");
	}

	public void CloseMessanger()
	{
		GameObject messanger = GameObject.Find ("ButtonCloseMessanger");
		Animator anim_messanger = messanger.GetComponent<Animator> ();
		anim_messanger.Play ("LeaveMessanger");
	}

	public void PayExtraLife()
	{
		Sound.GetSound("ButtonOneLife");
		if (UIManagerMenu.g.P.Ingot.Coin_count >= 5) 
		{
			new UICommon ().PayOneLife ();

			GameObject ingot = GameObject.Find ("CountIngot");
			Text ingotText = ingot.GetComponent<Text> ();
			ingotText.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();

			StartCoroutine ("CoRoutineLeaveLifes");
		} 
		else 
		{
			this.ShowSales ();
		}

	}

	public void PayExtraInfiniteLifePerTwo()
	{
		Sound.GetSound("ButtonTwoHours");
		if (UIManagerMenu.g.P.Ingot.Coin_count >= 25) {
			
			LoadMap.startTime = new UICommon ().PayExtraInfiniteLife(25,2);

			GameObject imageLife = GameObject.Find ("ImageLife");
			Animator anim = imageLife.GetComponent<Animator> ();
			anim.Play ("LeaveLife");

			StartCoroutine ("CoRoutineLeaveLifes");

			GameObject ingot = GameObject.Find ("CountIngot");
			Text ingotText = ingot.GetComponent<Text> ();
			ingotText.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();
		} 
		else 
		{
			this.ShowSales ();
		}

	}

	public void PayExtraInfiniteLifePerSix()
	{
		Sound.GetSound("ButtonSixHours");
		if (UIManagerMenu.g.P.Ingot.Coin_count >= 60) {
			
			LoadMap.startTime = new UICommon ().PayExtraInfiniteLife(60,6);
		
			GameObject ingot = GameObject.Find ("CountIngot");
			Text ingotText = ingot.GetComponent<Text> ();
			ingotText.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();

			GameObject imageLife = GameObject.Find ("ImageLife");
			Animator anim = imageLife.GetComponent<Animator> ();
			anim.Play ("LeaveLife");

			StartCoroutine ("CoRoutineLeaveLifes");
		} 
		else 
		{
			this.ShowSales ();
		}

	}

	public void ViewStadistics()
	{
		StartCoroutine ("CoRoutineShowStadistics");
	}

	public void ViewMapLevelRegister()
	{
		LoadMap.backLevelsMap.SetActive (true);
		LoadMap.achievementGrid.SetActive (false);
		LoadMap.mapRegPanel.SetActive (false);

		GameObject staText = GameObject.Find ("StadisticsText");
		Text staTextText = staText.GetComponent<Text> ();
		staTextText.text = "Your Stadistics: Levels & Maps";

		GameObject subStaText = GameObject.Find ("SubTitleStadistics");
		Text subStaTextText = subStaText.GetComponent<Text> ();
		subStaTextText.text = "Select a map from the first grid to see the level register.";

		if (UIManagerMenu.g.P.ListRegMap.Count> 0) {
			for (int i = 0; i < 4 && i < UIManagerMenu.g.P.ListRegMap.Count; i++) {
				GameObject mapNumber = GameObject.Find ("TextMapNumber" + (i + 1));
				Text mapNumberText = mapNumber.GetComponent<Text> ();
				mapNumberText.text = UIManagerMenu.g.P.ListRegMap [i].Map.Id_map.ToString ();
				GameObject mapNumberGrid = GameObject.Find ("GridMapNumber" + (i + 1));
				Button mapNumberButton = mapNumberGrid.GetComponent<Button> ();
				mapNumberButton.interactable = true;

				GameObject mapName = GameObject.Find ("TextMapName" + (i + 1));
				Text mapNameText = mapName.GetComponent<Text> ();
				mapNameText.text = UIManagerMenu.g.P.ListRegMap [i].Map.Name.ToString ();
				GameObject mapNameGrid = GameObject.Find ("GridMapName" + (i + 1));
				Button mapNameButton = mapNameGrid.GetComponent<Button> ();
				mapNameButton.interactable = true;
			}
			GameObject barAch = GameObject.Find ("PieceBarLevel");
			SetDefaultBarValues (barAch, 0.0012207f, 193.7f);
			restHeigh = CalculateBar (10, UIManagerMenu.g.P.ListRegMap [0].ListRegLevel.Count, 193.7f, barAch);
			ViewLevelRegister (0, 0);
			indexRegLevel = 0;
			indexMap = 0;
		}

	}

	public void ViewLevelReg()
	{
		string nameButton = EventSystem.current.currentSelectedGameObject.name;
		indexMap = int.Parse(nameButton.Substring (nameButton.Length - 1, nameButton.Length - 1));
		ViewLevelRegister(indexMap,0);
	}

	public void UpLevelRegister()
	{
		if (UIManagerMenu.g.P.ListRegMap.Count > 0) {
			GameObject barRegLevel = GameObject.Find ("PieceBarLevel");
			if (indexRegLevel > 0 && UIManagerMenu.g.P.ListRegMap [indexMap].ListRegLevel.Count > 10) {
				indexRegLevel--;
				ViewLevelRegister (indexMap, indexRegLevel);
				MoveBar (barRegLevel, 10, UIManagerMenu.g.P.ListRegMap [indexMap].ListRegLevel.Count, restHeigh, true);
			}
		}
	}
	public void DownLevelRegister()
	{
		if (UIManagerMenu.g.P.ListRegMap.Count > 0) {
		GameObject barRegLevel = GameObject.Find ("PieceBarLevel");
		if (indexRegLevel < UIManagerMenu.g.P.ListRegMap[indexMap].ListRegLevel.Count - 10 && UIManagerMenu.g.P.ListRegMap[indexMap].ListRegLevel.Count > 10)
		{
			indexRegLevel++;
			ViewLevelRegister(indexMap,indexRegLevel);
			MoveBar(barRegLevel,10,UIManagerMenu.g.P.ListRegMap[0].ListRegLevel.Count,restHeigh, false);
		}
		}
	}

	public void ViewAchievements()
	{
		//new UICommon ().SetActiveControls (false);
		LoadMap.backLevelsMap.SetActive (false);
		LoadMap.achievementGrid.SetActive (true);
		LoadMap.mapRegPanel.SetActive (false);

		GameObject staText = GameObject.Find ("StadisticsText");
		Text staTextText = staText.GetComponent<Text> ();
		staTextText.text = "Your Stadistics: Achievements";

		GameObject subStaText = GameObject.Find ("SubTitleStadistics");
		Text subStaTextText = subStaText.GetComponent<Text> ();
		subStaTextText.text = "Select an achievement from the list for more details.";

		GameObject barAch = GameObject.Find ("PieceBarAchMap");
		SetDefaultBarValues (barAch, -0.5009289f, 457.62f);
		restHeigh = CalculateBar (4, UIManagerMenu.g.P.ListAchievements.Count, 229.5f, barAch);
		ViewAchievementPerIndex (0);
		indexAch = 0;
	}

	public void UpAchievement()
	{
		GameObject barAch = GameObject.Find ("PieceBarAchMap");
		if (indexAch> 0 && UIManagerMenu.g.P.ListAchievements.Count > 4) 
		{
			indexAch--;
			ViewAchievementPerIndex (indexAch);
			MoveBar(barAch,4, UIManagerMenu.g.P.ListAchievements.Count,restHeigh, true);
		}
	}
	public void DownAchievement()
	{
		GameObject barAch = GameObject.Find ("PieceBarAchMap");
		if (indexAch < UIManagerMenu.g.P.ListAchievements.Count - 4 && UIManagerMenu.g.P.ListAchievements.Count > 4) {
			indexAch++;
			ViewAchievementPerIndex (indexAch);
			MoveBar(barAch, 4, UIManagerMenu.g.P.ListAchievements.Count,restHeigh, false);
		}
	}

	public void CloseStadistics()
	{
		StartCoroutine ("CoRoutineLeaveStadistics");
	}

	public void CloseAchReached()
	{
		if (LoadMap.listCompleteAch.Count - 1 > indexAchReached)
		{
			ShowPrize (LoadMap.listCompleteAch [indexAchReached]);
		} 
		else 
		{
			LoadMap.listCompleteAch.Clear ();
			GameObject achievement = GameObject.Find ("ImageAchReached");
			Animation achievementAnim = achievement.GetComponent<Animation> ();
			achievementAnim.Play ("LeaveAchReached");

			new UICommon ().SetActiveControls (false);
		}
	}

	private void ShowPrize(Achievement ach)
	{
		new UICommon ().SetActiveControls (false);

		GameObject pictureAch = GameObject.Find ("PictureAchReached");
		Image pictureAchImage = pictureAch.GetComponent<Image> ();
		RectTransform pictureAchRect = pictureAch.GetComponent<RectTransform> ();
		pictureAchImage.sprite = Resources.Load<Sprite> ("Achievements/" + ach.Image_file) as Sprite;
		pictureAchRect.sizeDelta = new Vector2 ((float)(ach.Width), (float)(ach.Height));

		GameObject title = GameObject.Find ("TitleAchReached");
		Text titleText = title.GetComponent<Text>();
		titleText.text = ach.Title;

		GameObject description = GameObject.Find ("DescriptionAchReached");
		Text descriptionText = description.GetComponent<Text>();
		descriptionText.text = ach.Description;

		GameObject earn = GameObject.Find ("EarnText");
		Text earnText = earn.GetComponent<Text>();
		earnText.text = "You have earned " + ach.Prize.ToString();

		GameObject achievement = GameObject.Find ("ImageAchReached");
		Animation achievementAnim = achievement.GetComponent<Animation> ();
		achievementAnim.Play ("ShowAchReached");
	}

	private void ViewAchievementPerIndex(int pIndex)
	{

		for(int i=0; i<4&& i<UIManagerMenu.g.P.ListAchievements.Count; i++)
		{
			GameObject pictureAch = GameObject.Find ("ImageAch" + (i+1));
			Image imagePictureAch = pictureAch.GetComponent<Image> ();
			RectTransform pictureAchRect = pictureAch.GetComponent<RectTransform> ();
			imagePictureAch.sprite = Resources.Load<Sprite> ("Achievements/" + UIManagerMenu.g.P.ListAchievements [i + pIndex].Image_file) as Sprite;
			pictureAchRect.sizeDelta = new Vector2 ((float)(UIManagerMenu.g.P.ListAchievements [i + pIndex].Width), (float)(UIManagerMenu.g.P.ListAchievements [i+ pIndex].Height));
			Vector2 tempost = new Vector2 ();
			tempost = pictureAchRect.anchoredPosition;
			tempost.x = (float)UIManagerMenu.g.P.ListAchievements[i+ pIndex].Loc_x;
			tempost.y = -4.0f;
			pictureAchRect.anchoredPosition = tempost;

			GameObject titleAch = GameObject.Find ("TitleAch" + (i+1));
			Text titleAchText = titleAch.GetComponent<Text> ();
			titleAchText.text =  UIManagerMenu.g.P.ListAchievements [i + pIndex].Title;

			GameObject descriptionAch = GameObject.Find ("DescriptionAch" + (i+1));
			Text descriptionAchText = descriptionAch.GetComponent<Text> ();
			descriptionAchText.text =  UIManagerMenu.g.P.ListAchievements [i + pIndex].Description;

			if (UIManagerMenu.g.P.ListAchievements [i + pIndex].Percentaje < 100) 
			{
				GameObject percentageAch = GameObject.Find ("PercentageAch" + (i+1));
				Text percentageAchText = percentageAch.GetComponent<Text> ();
				percentageAchText.text =  UIManagerMenu.g.P.ListAchievements [i + pIndex].Percentaje.ToString();

				GameObject prizeAch = GameObject.Find ("IngotTextAch" + (i+1));
				Text prizeAchText = prizeAch.GetComponent<Text> ();
				prizeAchText.text = "X" +  UIManagerMenu.g.P.ListAchievements [i + pIndex].Prize;
			} 
			else 
			{
				GameObject imagePerArch = GameObject.Find ("ImagePerAch" + (i+1));
				imagePerArch.SetActive (false);

				GameObject okAch = GameObject.Find ("OkAch" + (i+1));
				Image ImageOkAch = okAch.GetComponent<Image> ();
				ImageOkAch.enabled = true;
			}

		}

	}

	private float CalculateBar(int pItemsGrid, int pItemsList, float pMaxY, GameObject pBar)
	{
		float diferenceHeight = 0;

		if (pItemsList > pItemsGrid) {
			Canvas.ForceUpdateCanvases ();
			RectTransform barRect = pBar.GetComponent<RectTransform> ();

			float oldHeight = barRect.rect.height;
			float newHeight = barRect.rect.height * ((pItemsGrid * 1.0f) / (pItemsList * 1.0f));
			diferenceHeight = oldHeight - newHeight;

			barRect.sizeDelta = new Vector2 (barRect.rect.width, newHeight);

			Vector2 temPost = new Vector2 ();
			temPost.y = (pMaxY - barRect.anchoredPosition.y) * (1.0f - ((pItemsGrid * 1.0f) / (pItemsList * 1.0f)));
			temPost.x = barRect.anchoredPosition.x;
			barRect.anchoredPosition = temPost;
		}
		return diferenceHeight;
	}

	private void MoveBar(GameObject pBar, int pItemsGrid, int pItemsList ,float pRestHeight, bool pIsUp)
	{
		float restPerUnit = pRestHeight / ((pItemsList - pItemsGrid) * 1.0f);
		RectTransform barRect = pBar.GetComponent<RectTransform> ();
		Vector2 temPost = new Vector2 ();

		if (pIsUp == true) 
		{
			temPost.y = barRect.anchoredPosition.y + restPerUnit;
		} 
		else 
		{
			temPost.y = barRect.anchoredPosition.y - restPerUnit;
		}

		temPost.x = barRect.anchoredPosition.x;
		barRect.anchoredPosition = temPost;
	}

	private void SetDefaultBarValues(GameObject pBar, float pDefaultY, float pDefaultHeight)
	{
		RectTransform barRect = pBar.GetComponent<RectTransform> ();
		barRect.sizeDelta = new Vector2 (barRect.rect.width, pDefaultHeight);
		Vector2 temPost = new Vector2 ();
		temPost.y = pDefaultY;
		temPost.x = barRect.anchoredPosition.x;
		barRect.anchoredPosition = temPost;
	}

	private void ViewLevelRegister(int pIdMap, int pIdIndex)
	{
		for (int i = 0; i < 10 && i<UIManagerMenu.g.P.ListRegMap[pIdMap].ListRegLevel.Count; i++) 
		{
			GameObject levelNumber = GameObject.Find ("GridLevelNumber" + (i+1));
			Text levelNumberText = levelNumber.GetComponent<Text> ();
			levelNumberText.text = UIManagerMenu.g.P.ListRegMap[pIdMap].ListRegLevel[i + pIdIndex].Level.Id_level.ToString();

			GameObject levelPlayer = GameObject.Find ("GridLevelPlayer" + (i+1));
			Text levelPlayerText = levelPlayer.GetComponent<Text> ();
			levelPlayerText.text = UIManagerMenu.g.P.Name;

			GameObject levelScore = GameObject.Find ("GridLevelScore" + (i+1));
			Text levelScoreText = levelScore.GetComponent<Text> ();
			levelScoreText.text = UIManagerMenu.g.P.ListRegMap[pIdMap].ListRegLevel[i+pIdIndex].Score.ToString();

			ShowStars (i, UIManagerMenu.g.P.ListRegMap [pIdMap].ListRegLevel [i+pIdIndex].Count_stars);
		}

		GameObject totalScore = GameObject.Find ("TotalScoreText");
		Text totalScoreText = totalScore.GetComponent<Text> ();
		totalScoreText.text = "Total Score: " + UIManagerMenu.g.P.ListRegMap[pIdMap].Score_map.ToString();
	}
		
	private void ShowStars(int pCount,int pCountStars)
	{
		//GameObject levelStar = GameObject.Find ("GridLevelStar" + (pCount + 1));
		GameObject levelStar = GameObject.Find ("Stars" + pCount);
		Image starsImage = levelStar.GetComponent<Image> ();
		RectTransform starsRect = levelStar.GetComponent<RectTransform> ();
		starsImage.enabled = true;

		if (pCountStars == 1)
		{
			starsImage.sprite = Resources.Load<Sprite> ("BarsAndOthers/OneStart") as Sprite;
			starsRect.sizeDelta = new Vector2 (19.13f, 19.6f);
		} 
		else if (pCountStars == 2) 
		{
			starsImage.sprite = Resources.Load<Sprite> ("BarsAndOthers/TwoStarts") as Sprite;
			starsRect.sizeDelta = new Vector2 (38.26f, 19.6f);
		} 	
		else 
		{
			starsImage.sprite = Resources.Load<Sprite> ("BarsAndOthers/ThreeStarts") as Sprite;
			starsRect.sizeDelta = new Vector2 (57.4f, 19.6f);
		}
	}

	private void GoToNextLevel()
	{
		GameObject selectLevel = GameObject.Find ("SelectedLevel");
		RectTransform rectPost = selectLevel.GetComponent<RectTransform> ();

		GameObject explosion = GameObject.Find ("Explosion");
		Image explosionImage = explosion.GetComponent<Image> ();
		RectTransform rectExp = explosion.GetComponent<RectTransform> ();
		int CountListRegLevel = 0;
		if(UIManagerMenu.g.P.ListRegMap.Count>0)
		{
			CountListRegLevel = UIManagerMenu.g.P.ListRegMap [UIManagerMenu.IdMap].ListRegLevel.Count;
		}

		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel.Count; i++) {
			Level level = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [i];

			GameObject levelNx = GameObject.Find ("Level" + level.Id_level);
			Button buttonLevel = levelNx.GetComponent<Button> ();

			if (level.Id_level < CountListRegLevel + 1) {

				//levelGo = level.Id_level;
				this.ShowStars (level.Id_level, UIManagerMenu.g.P.ListRegMap[UIManagerMenu.IdMap].ListRegLevel [i].Count_stars);

			} else if (level.Id_level == CountListRegLevel + 1) {

				levelGo = level.Id_level;

				Vector2 postSelect = new Vector2 (level.X, level.Y);
				rectPost.anchoredPosition = postSelect;

				Vector2 postExplosion = new Vector2 (level.X_exp, level.Y_exp);
				rectExp.anchoredPosition = postExplosion;
				explosionImage.enabled = false;
				StartCoroutine ("CoRoutineShowExplosion");

				picture = GameObject.Find ("Frame");
				Animator picture_anim = picture.GetComponent<Animator> ();
				picture_anim.Play ("MoveToLevel" + level.Id_level);
				buttonLevel.interactable = true;
			} else {
				buttonLevel.interactable = false;
			}

		}
	}

	IEnumerator CoRoutineLeaveLifes()
	{
		Sound.GetSound("ButtonClosePlay");
		GameObject imageLife = GameObject.Find ("ImageLife");
		Animator anim = imageLife.GetComponent<Animator> ();
		anim.Play ("LeaveLife");

		yield return new WaitForSeconds (1f);

		new UICommon ().SetActiveControls (true);
	}

	IEnumerator CoRoutineLeaveLevel()
	{
		anim.Play ("LeaveLevel");

		yield return new WaitForSeconds (1f);

		new UICommon ().SetActiveControls (true);

		GameObject cursorBar = GameObject.Find ("CursorBar");
		BoxCollider2D cursorBox = cursorBar.GetComponent<BoxCollider2D> ();
		cursorBox.enabled = true;

	}

	IEnumerator CoRoutineShowStadistics()
	{
		new UICommon ().SetActiveControls (false);
		GameObject stadistics = GameObject.Find ("ImageStadistics");
		Animator stadisticsAnim = stadistics.GetComponent<Animator> ();
		stadisticsAnim.Play ("ShowStadistics");

		yield return new WaitForSeconds (1f);

		ViewMapLevelRegister ();
	}

	IEnumerator CoRoutineLeaveStadistics()
	{
		new UICommon().SetActiveControls (true);
		yield return new WaitForSeconds (1f);
		GameObject stadistics = GameObject.Find ("ImageStadistics");
		Animator stadisticsAnim = stadistics.GetComponent<Animator> ();
		stadisticsAnim.Play ("LeaveStadistics");
	}
}