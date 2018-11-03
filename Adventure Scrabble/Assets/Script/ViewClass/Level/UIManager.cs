using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {

	private Button backButton;
	private Button nextButton;
	private Image pageImage;
	private AudioSource a;
	private AudioSource audio_lost_two;
	public Text totalScore;
	public AudioSource audio_win;
	public static bool goNext = false;
	public static int count = 0;
	public static int countStars=0;

	public void NoAnswer()
	{
		Sound.GetSound("ButtonNo");
		GameObject question = GameObject.Find ("QuestionImage");
		Animator anim_question = question.GetComponent<Animator> ();
		anim_question.Play("LeaveQuestion");
	}

	public void YesAnswer()
	{
		Sound.GetSound("ButtonYes");
		GameObject question = GameObject.Find ("QuestionImage");
		Animator anim_question = question.GetComponent<Animator> ();
		anim_question.Play("LeaveQuestion");

		if (this.AreObjectivesReached() == true) 
		{   
			SaveStateLevel (true);
			ListPieces.audioGame.Stop ();
			GameObject win = GameObject.Find ("ImageWin");
			audio_win = win.GetComponent<AudioSource> ();
			audio_win.Play ();
			Animator anim_win = win.GetComponent<Animator> ();
			anim_win.Play ("ShowWin");
			totalScore.text = "Score: " + PieceDrag.total_score_gral;
			StartCoroutine ("CoRoutineShowStars");
		} 
		else 
		{
			GameObject give_up = GameObject.Find ("ImageGiveUp");
			Animator anim_give_up = give_up.GetComponent<Animator> ();
			anim_give_up.Play ("ShowGiveUp");
		}
	}

	public void GiveUp()
	{
		Sound.GetSound("ButtonGiveUp");
		StartCoroutine ("coRoutineLost");
		SaveStateLevel (false);
	}
		
	public void TryAgain()
	{
        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);

        Sound.GetSound("ButtonTryAgain");
		GameObject lost_two = GameObject.Find ("ImageLostTwo");
		Sound.StopSound("ImageLostTwo");
		Animator anim_lost_two = lost_two.GetComponent<Animator> ();
		anim_lost_two.Play ("LeaveMessageLooseTwo");

		if (UIManagerMenu.g.P.Heart.Count_lifes > 0 || UIManagerMenu.g.P.Heart.IsInfinite == true) 
		{
			this.ReStart ();
		} 
		else 
		{
			GameObject image_life = GameObject.Find ("ImageLife");
			Animator anim_image_life = image_life.GetComponent<Animator> ();
			anim_image_life.Play ("ShowLife");
		}
	}
		
	public void PlayAgain()
	{
        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);

        Sound.GetSound("ButtonPlayAgain");
		GameObject win = GameObject.Find ("ImageWin");
		audio_win.Stop();
		Animator anim_win = win.GetComponent<Animator> ();
		anim_win.Play ("LeaveWin");

		this.DisableStars ();
		this.ReStart ();
	}

	public void Next()
	{
		Sound.GetSound("ButtonNext");
        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);

        GameObject next = GameObject.Find ("ImageWin");
		Animator anim_next = next.GetComponent<Animator> ();
		anim_next.Play ("LeaveWin");
		DestroyListPieces ();
		SceneManager.LoadScene ("Scenes/Map");
		goNext = true;
		LoadMap.isPlay = false;
	}

	public void PayBonusLingot()
	{
		Sound.GetSound("ButtonBonus");
		if (UIManagerMenu.g.P.Ingot.Coin_count >= 20) {
			UIManagerMenu.g.P.Ingot.Coin_count = UIManagerMenu.g.P.Ingot.Coin_count - 20;
			GameObject titleBonus = GameObject.Find ("TitleBonus");
			Text titleDescription = titleBonus.GetComponent<Text> ();

			if (titleDescription.text == "Nine Points") {
				ListPieces.bonusNine++;
			} else if (titleDescription.text == "Green Square") {
				ListPieces.bonusPerThree++;
			} else {
				ListPieces.bonusUndo++;
			}

			new Persistence ().UpdateIngot (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);

			GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
			Animator anim = imageBackBonus.GetComponent<Animator> ();
			anim.Play ("LeaveBonus");
		} 
		else 
		{
			StartCoroutine ("CoRoutineShowSales");
		}
	
	}

	public void PayExtraLife()
	{
		Sound.GetSound("ButtonOneLife");
		if (UIManagerMenu.g.P.Ingot.Coin_count >= 5) {
			
			new UICommon ().PayOneLife ();

			GameObject imageLife = GameObject.Find ("ImageLife");
			Animator anim = imageLife.GetComponent<Animator> ();
			anim.Play ("LeaveLife");

			this.ReStart ();
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

			this.ReStart ();
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
			GameObject imageLife = GameObject.Find ("ImageLife");
			Animator anim = imageLife.GetComponent<Animator> ();
			anim.Play ("LeaveLife");

			this.ReStart ();
		} 
		else 
		{
			this.ShowSales ();
		}

	}
		
	public void ClosePayBonus()
	{
		Sound.GetSound("CloseBonus");
		GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
		Animator anim = imageBackBonus.GetComponent<Animator> ();
		anim.Play ("LeaveBonus");
	}

	public void ClosePayLife()
	{
		Sound.GetSound ("CloseHeart");
        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);
        GameObject imageLife = GameObject.Find ("ImageLife");
		Animator imageLife_anim = imageLife.GetComponent<Animator> ();
		imageLife_anim.Play ("LeaveLife");
		SceneManager.LoadScene ("Scenes/Map");
		LoadMap.isPlay = false;
	}

	public void CloseSales()
	{
		Sound.GetSound("CloseSales");
		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("LeaveSales");
		/*SceneManager.LoadScene ("Scenes/Map");
		LoadMap.isPlay = false;*/
	}

	public void CloseTryAgain()
	{
		Sound.GetSound("ButtonCloseTryAgain");
        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);
        GameObject tryAgain = GameObject.Find ("ImageLostTwo");
		Animator anim_try = tryAgain.GetComponent<Animator> ();
		anim_try.Play ("LeaveMessageLooseTwo");
		DestroyListPieces ();
		SceneManager.LoadScene ("Scenes/Map");
		LoadMap.isPlay = false;
	}
		
	public void LookForSales()
	{
		this.ShowSales();
	}

	public void GoOut()
	{
		Sound.GetSound("ButtonDeparture");
		UIManagerMenu.g.P.ListBonus [0].Count_bonus = ListPieces.bonusNine;
		UIManagerMenu.g.P.ListBonus [1].Count_bonus = ListPieces.bonusPerThree;
		UIManagerMenu.g.P.ListBonus [2].Count_bonus = ListPieces.bonusUndo;

		if (ListPieces.isSwap == true)
		{
			UIManagerMenu.g.P.ListBonus [3].Count_bonus = ListPieces.bonusSwap;
		}

        new Persistence().UpdateIngot(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Ingot);
        new Persistence().UpdateListBonus(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus);
		DestroyListPieces ();
        SceneManager.LoadScene ("Scenes/Map");
		LoadMap.isPlay = false;
	}

	public void ShowSales()
	{
		Sound.GetSound("ButtonSales");
		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("ShowSales");

		GameObject countIngotSales = GameObject.Find ("CountIngotSales");
		Text countIngot = countIngotSales.GetComponent<Text> ();
		countIngot.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();
	}

	private void ReStart()
	{
		if(PieceDrag.adjacentList.Count > 0)
		{
			for (int i = 0; i < PieceDrag.adjacentList.Count; i++)
			{
				Destroy (PieceDrag.adjacentList [i].Game_square);
			}
		}

		if(PieceDrag.squarePieceList.Count > 0)
		{
			for (int i = 0; i < PieceDrag.squarePieceList.Count; i++)
			{
				Destroy (PieceDrag.squarePieceList[i].piece.Game_piece);
			}
		}

		if (ListPieces.pieceList.Count > 0) 
		{
			for (int i = 0; i < ListPieces.pieceList.Count; i++)
			{
				Destroy (ListPieces.pieceList[i].Game_piece);
			}
		}

		ListPieces.pieceList = new List<Piece>();
		ListPieces.board = new Board ();
		PieceDrag.squarePieceList = new List<SquarePiece> ();
		PieceDrag.adjacentList = new List<Square>();

		ListPieces.message_string = "";
		ListPieces.particle_one.SetActive (false);
		ListPieces.particle_two.SetActive (false);
		ListPieces.particle_three.SetActive (false);
		ListPieces.okObjective1.SetActive (false);
		ListPieces.okObjective2.SetActive (false);
		PieceDrag.total_score_gral = 0;
		PieceDrag.first_piece = true;
		PieceManagerRight.count = 0;
		PieceManagerRight.topMax = 10;

		this.UpdateScoreBar ();

		UIManagerMenu.g = new Persistence ().GetGame ();

		GameObject startGame = GameObject.Find ("StartGame"); 
		ListPieces l = startGame.GetComponent<ListPieces> ();
		l.Start ();
	}
		
	private void  UpdateScoreBar()
	{
		int total_score = 300;
		Vector2 temPost;
		float size_x = 0;
		GameObject bar = GameObject.Find ("ScoreBarAhead");

		temPost = bar.transform.position;
		temPost.x = -5.195f;
		size_x = 1.00490f;

		if (PieceDrag.total_score_gral >= 300) 
		{
			size_x = 0f;
			temPost.x = temPost.x + 1.00490f; 			
		} 
		else 
		{
			size_x = size_x - ((size_x * PieceDrag.total_score_gral) / total_score);
			temPost.x = temPost.x + ((1.00490f * PieceDrag.total_score_gral) / total_score); 
		}

		bar.transform.position = temPost;
		bar.transform.localScale = new Vector2(size_x,0.98f);

		if(temPost.x >= -4.69255f)
		{
			ListPieces.particle_one.SetActive (true);
		}

		if(temPost.x >= -4.525066667f)
		{
			ListPieces.particle_two.SetActive (true);
		}

		if(temPost.x >= -4.1901f)
		{
			ListPieces.particle_three.SetActive (true);
		}

	}
		
	private void MinusLife()
	{
		if (UIManagerMenu.g.P.Heart.IsInfinite == false) {
			UIManagerMenu.g.P.Heart.Count_lifes--;
			NextLife n = new NextLife ();
			UIManagerMenu.g.P.Heart.List_next_life.Add (n);

			if (UIManagerMenu.g.P.Heart.List_next_life.Count == 1) 
			{
				UIManagerMenu.g.P.Heart.List_next_life[UIManagerMenu.g.P.Heart.List_next_life.Count - 1].Date_next_life = DateTime.Now.AddMinutes (20);
			} 
			else 
			{
				UIManagerMenu.g.P.Heart.List_next_life[UIManagerMenu.g.P.Heart.List_next_life.Count - 1].Date_next_life = UIManagerMenu.g.P.Heart.List_next_life[UIManagerMenu.g.P.Heart.List_next_life.Count - 2].Date_next_life.AddMinutes (20);
			}
			new Persistence ().InsertNextLife (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart.Id_heart, UIManagerMenu.g.P.Heart.List_next_life [UIManagerMenu.g.P.Heart.List_next_life.Count - 1]);
			new Persistence ().UpdateHeart (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.Heart);
		}
	}

	private void DisableStars()
	{
			GameObject gameAnimatorGreen = GameObject.Find ("GreenStarScore"); 
			Image imageAnimatorGreen = gameAnimatorGreen.GetComponent<Image> ();
			if (imageAnimatorGreen.enabled == true) 
		    {
				imageAnimatorGreen.enabled = false;
		    }
			GameObject gameAnimatorYellow= GameObject.Find ("YellowStarScore"); 
			Image imageAnimatorYellow = gameAnimatorYellow.GetComponent<Image> ();
			if (imageAnimatorYellow.enabled == true) 
			{
				imageAnimatorYellow.enabled = false;
			}
			GameObject gameAnimatorRed = GameObject.Find ("RedStarScore"); 
			Image imageAnimatorRed = gameAnimatorRed.GetComponent<Image> ();
		    if (imageAnimatorRed.enabled == true) 
			{
				imageAnimatorRed.enabled = false;
			}
	}

	private void SaveStateLevel(bool pIsWon)
	{
		if (UIManagerMenu.g.P.ListStateLevel [LoadMap.levelGo - 1].Id_state == 0) 
		{
			if(pIsWon == true)
			{
				UIManagerMenu.g.P.ListStateLevel [LoadMap.levelGo - 1].Id_state = 1;
			}
			else
			{
				UIManagerMenu.g.P.ListStateLevel[LoadMap.levelGo - 1].Id_state = 2;
				UIManagerMenu.g.P.ListStateLevel [LoadMap.levelGo - 1].Times_lost++;
			}

            new Persistence().UpdateLevelState(UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].Id_map, UIManagerMenu.g.P.ListStateLevel[LoadMap.levelGo - 1]);

        }
	}

	private bool AreObjectivesReached()
	{
		int times = 0;
		bool isReached = false;
		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list.Count; i++) 
		{
			int idType = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Type_obj.Id_type_objective;

			if (idType == 1) 
			{
				if (PieceDrag.total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value) 
				{
					times++;
				}
			} 
			else if (idType == 2) 
			{
				if(ListPieces.pieceList.Count<UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value)
				{
					times++;
				}
			}

			//Include the rest of the idTypes later

			if (times == UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list.Count) 
			{
				isReached = true;
			}
		}

		return isReached;
	}

	IEnumerator coRoutineLost()
	{
		GameObject give_up = GameObject.Find ("ImageGiveUp");
		Animator anim_give_up = give_up.GetComponent<Animator> ();
		anim_give_up.Play ("LeaveGiveUp");

		if (ListPieces.audioGame.isPlaying == true) 
		{
			ListPieces.audioGame.Stop ();
		}

		yield return new WaitForSeconds (2f);

		Sound.GetSound("ImageLostOne");
		GameObject lost_one = GameObject.Find ("ImageLostOne");
		AudioSource audio_lost_one = lost_one.GetComponent<AudioSource> ();

		Animator anim_lost_one = lost_one.GetComponent<Animator> ();
		anim_lost_one.Play ("ShowMessageLoose");

		yield return new WaitForSeconds (7f);

		GameObject lost_two = GameObject.Find ("ImageLostTwo");
		if (audio_lost_one.isPlaying == true) 
		{
			audio_lost_one.Stop ();
		}
		Sound.GetSound("ImageLostTwo");
		Animator anim_lost_two = lost_two.GetComponent<Animator> ();
		anim_lost_two.Play ("ShowMessageLooseTwo");
		MinusLife ();
	}

	IEnumerator CoRoutineShowStars()
	{
		yield return new WaitForSeconds (2f);

		if (PieceDrag.total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[0].Score_level) 
		{
			countStars = 1;
			GameObject gameAnimatorGreen = GameObject.Find ("GreenStarScore"); 
			Image imageAnimatorGreen = gameAnimatorGreen.GetComponent<Image> ();
			imageAnimatorGreen.enabled = true;
			Animator animGreen = gameAnimatorGreen.GetComponent<Animator> ();
			animGreen.Play ("ShowGreenStar");
		}
			
		if (PieceDrag.total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[1].Score_level) 
		{
			yield return new WaitForSeconds (1f);
			countStars = 2;
			GameObject gameAnimatorYellow= GameObject.Find ("YellowStarScore"); 
			Image imageAnimatorYellow = gameAnimatorYellow.GetComponent<Image> ();
			imageAnimatorYellow.enabled = true;
			Animator animYellow = gameAnimatorYellow.GetComponent<Animator> ();
			animYellow.Play ("ShowYellowStar");
		}
			
		if (PieceDrag.total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[2].Score_level) 
		{
			yield return new WaitForSeconds (1f);
			countStars = 3;
			GameObject gameAnimatorRed = GameObject.Find ("RedStarScore"); 
			Image imageAnimatorRed = gameAnimatorRed.GetComponent<Image> ();
			imageAnimatorRed.enabled = true;
			Animator animRed = gameAnimatorRed.GetComponent<Animator> ();
			animRed.Play ("ShowRedStar");
		}
	}

	IEnumerator CoRoutineShowSales()
	{
		GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
		Animator anim = imageBackBonus.GetComponent<Animator> ();
		anim.Play ("LeaveBonus");

		yield return new WaitForSeconds (2f);

		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("ShowSales");

		GameObject countIngotSales = GameObject.Find ("CountIngotSales");
		Text countIngot = countIngotSales.GetComponent<Text> ();
		countIngot.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();
	}

	IEnumerator CoRoutineShowSalesTwo()
	{
		GameObject imageLife = GameObject.Find ("ImageLife");
		Animator imageLife_anim = imageLife.GetComponent<Animator> ();
		imageLife_anim.Play ("LeaveLife");

		yield return new WaitForSeconds (2f);

		GameObject sales = GameObject.Find ("SalesFrame");
		Animator anim_sales = sales.GetComponent<Animator> ();
		anim_sales.Play ("ShowSales");

		GameObject countIngotSales = GameObject.Find ("CountIngotSales");
		Text countIngot = countIngotSales.GetComponent<Text> ();
		countIngot.text = UIManagerMenu.g.P.Ingot.Coin_count.ToString();
	}

	private void DestroyListPieces()
	{
		for (int i = 0; i < ListPieces.pieceList.Count; i++)
		{
			Destroy (ListPieces.pieceList[i].Game_piece);
		}
		ListPieces.pieceList.Clear ();
	}
}
