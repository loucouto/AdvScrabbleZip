using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ListPieces : MonoBehaviour {

	//View Variables
	private int randomId;
	public static List<Piece> pieceList = new List<Piece>();
	public static Board board = new Board();
	private SpriteRenderer piece;
	private Vector2 temPost;
	private Piece f;
	private GameObject g;
	private string name_object = "";
	private BoxCollider2D b;
	public static string message_string;
	public Text pieceCountText;
	public Text countClick;
	public Text message;
	public Text score;
	public Text CountBonusNine;
	public Text CountBonusSwap;
	public Text CountBonusPerThree;
	public Text CountBonusUndo;
	private Animator anim;
	private SpriteRenderer spriteAnimator;
	public static int bonusNine = 0;
	public static int bonusSwap = 0;
	public static int bonusPerThree = 0;
	public static int bonusUndo = 0;
	private AudioSource a;
	public static AudioSource audioGame;
	public static bool first_time;
	private static bool first_charge = true;
	private static bool first_loose = false;
	public static bool isSwap = false;
	//private float startTime;
	private string minutes = "";
	private string seconds = "";
	public static GameObject particle_one;
	public static GameObject particle_two;
	public static GameObject particle_three;
	public static GameObject buttonCancel;
	public static GameObject okObjective1;
	public static GameObject okObjective2;
	public static GameObject okObjective3;
	public static GameObject scoreText1;
	public static GameObject scoreText2;
	public static GameObject scoreText3;
	public static GameObject textPiece1;
	public static GameObject textPiece2;
	public static GameObject textPiece3;
	public static GameObject pieceText1;
	public static GameObject pieceText2;
	public static GameObject pieceText3;
	public static GameObject andText1;
	public static GameObject andText2;
	public static bool first_back = false;
	// Use this for initialization
	public void Start () {

		GameObject gameAnimator = GameObject.Find ("ObjectiveMessage"); 
		anim = gameAnimator.GetComponent<Animator> ();
		anim.Play ("ShowMessageObjective");

		switch (LoadMap.levelGo) {
		case 1:
			ShowPieces(15, -1.73f, -1.97f);
			isSwap = false;
			break;
		}

		if (first_time == false) {
			particle_one = GameObject.Find ("ParticleSystem1");
			particle_two = GameObject.Find ("ParticleSystem2");
			particle_three = GameObject.Find ("ParticleSystem3");
			buttonCancel = GameObject.Find ("CancelButton");
			okObjective1 = GameObject.Find ("OkObjective1");
			okObjective2 = GameObject.Find ("OkObjective2");
			okObjective3 = GameObject.Find ("OkObjective3");
			scoreText1 = GameObject.Find ("ScoreText1");
			scoreText2 = GameObject.Find ("ScoreText2");
			scoreText3 = GameObject.Find ("ScoreText3");
			textPiece1 = GameObject.Find ("TextPiece1");
			textPiece2 = GameObject.Find ("TextPiece2");
			textPiece3 = GameObject.Find ("TextPiece3");
			pieceText1 = GameObject.Find ("PieceText1");
			pieceText2 = GameObject.Find ("PieceText2");
			pieceText3 = GameObject.Find ("PieceText3");
			andText1 = GameObject.Find ("AndText1");
			andText2 = GameObject.Find ("AndText2");

			particle_one.SetActive (false);
			particle_two.SetActive (false);
			particle_three.SetActive (false);
			buttonCancel.SetActive (false);
			okObjective1.SetActive (false);
			okObjective2.SetActive (false);
			okObjective3.SetActive (false);
			scoreText1.SetActive (false);
			scoreText2.SetActive (false);
			scoreText3.SetActive (false);
			textPiece1.SetActive (false);
			textPiece2.SetActive (false);
			textPiece3.SetActive (false);
			pieceText1.SetActive (false);
			pieceText2.SetActive (false);
			pieceText3.SetActive (false);
			andText1.SetActive (false);
			andText2.SetActive (false);
			first_time = true;
		}

		ChargeComponents (LoadMap.levelGo);

		this.pieceCountText.text = "Pieces: " + pieceList.Count;
		this.countClick.text = "CountProof: " + PieceManagerRight.count;

		GameObject startGame = GameObject.Find ("StartGame");
		GameObject music = GameObject.Find ("ButtonMusic");
		GameObject sound = GameObject.Find ("ButtonSound");
		audioGame = startGame.GetComponent<AudioSource> ();
		Image musicImage = music.GetComponent<Image> ();
		Image soundImage = sound.GetComponent<Image> ();

		if (UIManagerMenu.musicActive == true) 
		{
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusic") as Sprite;
			audioGame.Play();
		}
		else 
		{
			musicImage.sprite = Resources.Load<Sprite> ("Buttons/ButtonMusicProh") as Sprite;
			if (audioGame.isPlaying == true) 
			{
				audioGame.Stop();
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

		/*if (first_charge == true) {
			LoadMap.startTime = new UICommon ().ChargeTimer ();
			first_charge = false;
		}*/
	}
		
	// Update is called once per framework
	void Update () {

	    this.pieceCountText.text = "Pieces: " + ListPieces.pieceList.Count;
		this.countClick.text = "CountProof: " + PieceManagerRight.count;
		this.message.text = message_string;

		if (BonusSquare.isBonusMess == true) {
			this.message.color = Color.white;
		}

		if (UndoMovement.isBonusMessBlack == true) {
			this.message.color = Color.black;
		}

		this.score.text = "Score: " + PieceDrag.total_score_gral;
		this.CountBonusNine.text = "" + bonusNine;
		this.CountBonusPerThree.text = "" + bonusPerThree;
		this.CountBonusSwap.text = "" + bonusSwap;
		this.CountBonusUndo.text = "" + bonusUndo;
		this.CountBonusSwap.text = "" + bonusSwap;

		UseTimer ();
		new UICommon ().AnimateButtons (false);
		new UICommon ().AnimateButtonsLevel ();
		new UICommon ().AnimateButtonsMapLevel ();
	}
		
	private void UseTimer()
	{
		if (LoadMap.isPlay == true) {
			GameObject timeLife = GameObject.Find ("TextNextLife");
			Text timeLifeText = timeLife.GetComponent<Text> ();
				
			if (UIManagerMenu.g.P.Heart.IsInfinite == true) 
			{
				if(LoadMap.startTime >= 3600f)
				{
					timeLifeText.text = Convert.ToInt32(LoadMap.startTime/3600).ToString() + " hs";
				}
			    else
				{
					if (minutes == "00" && seconds == "00") 
					{
						UIManagerMenu.g.P.Heart.Count_lifes = 5;
						UIManagerMenu.g.P.Heart.IsInfinite = false;
						UIManagerMenu.g.P.Heart.Time_infinite = new DateTime ();

						new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);

						GameObject countLife = GameObject.Find("CountLife");
						Text countLifeText = countLife.GetComponent<Text> ();
						countLifeText.text = UIManagerMenu.g.P.Heart.Count_lifes.ToString();
					}
					else 
					{
						new UICommon ().UpdateMinSec (ref first_loose, ref LoadMap.startTime, ref seconds, ref minutes);
						timeLifeText.text = minutes + ":" + seconds;
					}
				}
			}
			else if (UIManagerMenu.g.P.Heart.Count_lifes == 5 && UIManagerMenu.g.P.Heart.IsInfinite == false) {

				timeLifeText.text = "Full";
				first_loose = true;
			} 
			else {
				if (minutes == "00" && seconds == "00") {
					UIManagerMenu.g.P.Heart.Count_lifes++;
					new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart); 
					new Persistence ().DeleteNextLife (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart.Id_heart, UIManagerMenu.g.P.Heart.List_next_life [0]);
					UIManagerMenu.g.P.Heart.List_next_life.RemoveAt (0);
					if (UIManagerMenu.g.P.Heart.Count_lifes < 5) {
						LoadMap.startTime = Time.time + 1200f;
						minutes = "";
						seconds = "";
					} 
				}
				else 
				{
					new UICommon ().UpdateMinSec (ref first_loose, ref LoadMap.startTime, ref seconds, ref minutes);
					timeLifeText.text = minutes + ":" + seconds;

				}
			}
		}

	}

	private void CountSizeBoard(int pTypeSize)
	{
		for (int i = 47 + (5 * pTypeSize); i < 51 + (5 * pTypeSize); i++) 
	    {
			UIManagerMenu.g.P.ListAchievements [i].Count++;
		
		}
	}

	private void ShowPieces(int pCount, float pPosX, float pPosY)
	{
		for (int i = 0; i < pCount; i++) {

			randomId = UnityEngine.Random.Range (0, 90);
			name_object = "Piece" + i;
			g = new GameObject (name_object);

			piece = g.AddComponent<SpriteRenderer> ();
			piece.sprite = Resources.Load<Sprite> ("PieceDrawing/" + UIManagerMenu.g.List_pieces[randomId].File) as Sprite;
			piece.sortingOrder = 2;
			b = g.AddComponent<BoxCollider2D> ();
			b.enabled = true;
			a = g.AddComponent<AudioSource> ();
			a.playOnAwake = false;
			g.AddComponent<PieceDrag>();

			temPost = g.transform.position;
			temPost.x = pPosX + (0.6f * (i + 1));
			temPost.y = pPosY;
			g.transform.position = temPost;

			f = new Piece();
			f.Id_piece =  UIManagerMenu.g.List_pieces[randomId].Id_piece;
			f.Piece_number = UIManagerMenu.g.List_pieces[randomId].Piece_number;
			f.Piece_score = UIManagerMenu.g.List_pieces[randomId].Piece_score;
			f.File = UIManagerMenu.g.List_pieces[randomId].File;
			f.Game_piece = g; 
			pieceList.Add(f);

			if (i > 4) 
			{
				ListPieces.pieceList [i].Game_piece.SetActive(false);
			}

		}

	}

	private void ChargeComponents(int pLevel)
	{
		GameObject background = GameObject.Find ("Background");
		SpriteRenderer spriteRen = new SpriteRenderer ();
		if (first_back == false)
		{
			spriteRen = background.AddComponent<SpriteRenderer>();
			first_back = true;
		}
		else
		{
			spriteRen = background.GetComponent<SpriteRenderer> (); 
		}

		//spriteRen = background.GetComponent<SpriteRenderer> (); 
		spriteRen.sprite = Resources.Load<Sprite>("BackgroundLevel/" + UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].ListLevel[pLevel-1].File) as Sprite;
		//spriteRen.sprite = Resources.Load<Sprite> ("BackgroundLevel/UnfocusedKnight");

		GameObject board = GameObject.Find ("Board");
		SpriteRenderer boardSprite = board.GetComponent<SpriteRenderer> ();
		boardSprite.sprite = Resources.Load<Sprite> ("Boards/" + UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel[pLevel-1].Board_list[0].File) as Sprite;

		int count = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Level_score_list.Count;

		for (int i = 0; i < count; i++) {
			Vector2 starPost = new Vector2 ();
			Star s = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Level_score_list [i].Star;
			GameObject star = GameObject.Find (s.Name);
			starPost.x = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Level_score_list [i].Bar_value;
			starPost.y = -0.42f;
			star.transform.position = starPost;

			if (i == 0) 
			{
				particle_one.transform.position = starPost;
			} 
			else if (i == 1) 
			{
				particle_two.transform.position = starPost;
			} 
			else 
			{
				particle_three.transform.position = starPost;
			}
		}

		GameObject movements = GameObject.Find ("Movements");
		Text movements_Text = movements.GetComponent<Text> ();
		if (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Conditionating_list [0].Is_infinitive == false) {
			movements_Text.text = "Movements: " + UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Conditionating_list [0].Top_value.ToString ();
		} else {
			movements_Text.text = "Movements: ∞";
		}

		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list.Count; i++) 
		{
			int idType = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Type_obj.Id_type_objective;

			if (idType == 1) 
			{
				if (i == 0) 
				{
					scoreText1.SetActive (true);
					Text scoreText1Text = scoreText1.GetComponent<Text> ();
					scoreText1Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString () + " pts";
				} 
				else if (i == 1) 
				{
					scoreText2.SetActive (true);
					Text scoreText2Text = scoreText2.GetComponent<Text> ();
					scoreText2Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString () + " pts";
				} 
				else 
				{
					scoreText3.SetActive (true);
					Text scoreText3Text = scoreText2.GetComponent<Text> ();
					scoreText3Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString () + " pts";
				}
			} 
			else if (idType == 2) 
			{
				if (i == 0) 
				{
					pieceText1.SetActive (true);
					GameObject imagePiece1 = GameObject.Find ("ImagePiece");
					Image imagePiece1Image = imagePiece1.GetComponent<Image> ();
					imagePiece1Image.sprite = Resources.Load<Sprite> ("PieceDrawing/Piece0-1") as Sprite;
					GameObject countPiece1 = GameObject.Find ("CountPiece1");
					Text countPiece1Text = countPiece1.GetComponent<Text> ();
					countPiece1Text.text = "< " + UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				} 
				else if (i == 1) 
				{
					pieceText2.SetActive (true);
					GameObject imagePiece2 = GameObject.Find ("ImagePiece2");
					Image imagePiece2Image = imagePiece2.GetComponent<Image> ();
					imagePiece2Image.sprite = Resources.Load<Sprite> ("PieceDrawing/Piece0-1") as Sprite;
                    GameObject countPiece2 = GameObject.Find ("CountPiece2");
					Text countPiece2Text = countPiece2.GetComponent<Text> ();
					countPiece2Text.text = "< " + UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				} 
				else 
				{
					pieceText3.SetActive (true);
					GameObject imagePiece3 = GameObject.Find ("ImagePiece3");
					Image imagePiece3Image = imagePiece3.GetComponent<Image> ();
					imagePiece3Image.sprite = Resources.Load<Sprite> ("PieceDrawing/Piece0-1") as Sprite;
                    GameObject countPiece3 = GameObject.Find ("CountPiece3");
					Text countPiece3Text = countPiece3.GetComponent<Text> ();
					countPiece3Text.text = "< " + UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				}
			} 
			else 
			{
				if (i == 0) 
				{
					textPiece1.SetActive (true);
					GameObject pieceImage1 = GameObject.Find ("PieceImage1");
					Image pieceImage1Image = pieceImage1.GetComponent<Image> ();
					GetImage(idType, pieceImage1Image.sprite);
					GameObject pieceCount1 = GameObject.Find ("CountPiece1");
					Text pieceCount1Text = pieceCount1.GetComponent<Text> ();
					pieceCount1Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				} 
				else if (i == 1) 
				{
					textPiece2.SetActive (true);
					GameObject pieceImage2 = GameObject.Find ("PieceImage2");
					Image pieceImage2Image = pieceImage2.GetComponent<Image> ();
					GetImage(idType, pieceImage2Image.sprite);
					GameObject pieceCount2 = GameObject.Find ("CountPiece2");
					Text pieceCount2Text = pieceCount2.GetComponent<Text> ();
					pieceCount2Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				} 
				else 
				{
					textPiece3.SetActive (true);
					GameObject pieceImage3 = GameObject.Find ("PieceImage3");
					Image pieceImage3Image = pieceImage3.GetComponent<Image> ();
					GetImage(idType, pieceImage3Image.sprite);
					GameObject pieceCount3 = GameObject.Find ("CountPiece1");
					Text pieceCount3Text = pieceCount3.GetComponent<Text> ();
					pieceCount3Text.text = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list [i].Top_value.ToString ();
				}
			}
				
			if (i == 0 && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list.Count>1) 
			{
				andText1.SetActive (true);
			} 
			else if (i == 1 && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [pLevel-1].Objective_list.Count>2) 
			{
				andText2.SetActive (true);
			}

		}

		bonusNine = UIManagerMenu.g.P.ListBonus[0].Count_bonus;
		bonusPerThree = UIManagerMenu.g.P.ListBonus[1].Count_bonus;
		bonusUndo = UIManagerMenu.g.P.ListBonus[2].Count_bonus;
		bonusSwap = UIManagerMenu.g.P.ListBonus[3].Count_bonus;

		if (isSwap == false) {
			GameObject swap = GameObject.Find ("BonusExchange");
			BoxCollider2D swapBox = swap.GetComponent<BoxCollider2D> ();
			swapBox.enabled = false;
			SpriteRenderer swapSprite = swap.GetComponent<SpriteRenderer> ();
			swapSprite.color = new Color(255f, 255f, 255f, 139f);
			GameObject bonusCircleExchange = GameObject.Find ("BonusCicleExchange");
			SpriteRenderer bonusCircleExchangeSprite = bonusCircleExchange.GetComponent<SpriteRenderer> ();
			bonusCircleExchangeSprite.color = new Color(255f, 255f, 255f, 139f);
		}

	}
	private void GetImage(int pIdType, Sprite s)
	{
		switch (pIdType) {
		case 3:
			s = Resources.Load<Sprite> ("GlassDrawing/GlassSquare0-1") as Sprite;
			break;
		case 4:
			s = Resources.Load<Sprite> ("SquareDrawing/SimpleSquare") as Sprite;
			break;
		case 5:
			s = Resources.Load<Sprite> ("SquareDrawing/BlueSquare") as Sprite;
			break;
		case 6:
			s = Resources.Load<Sprite> ("SquareDrawing/GreenSquare") as Sprite;
			break;
		case 7:
			s = Resources.Load<Sprite> ("SquareDrawing/YellowSquare") as Sprite;
			break;
		case 8:
			s = Resources.Load<Sprite> ("SquareDrawing/RedSquare") as Sprite;
			break;
		}
	
	}



}