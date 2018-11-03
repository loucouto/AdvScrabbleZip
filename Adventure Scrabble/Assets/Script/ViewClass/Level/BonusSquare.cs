using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSquare : MonoBehaviour {

	public static bool isBonusMess = false;
	public static List<Square> remarkedList = new List<Square>();
	private AudioSource a;

	void OnMouseDown()
	{
		if (ListPieces.bonusPerThree > 0) {
			Sound.GetSound ("GreenSquareBonus");
			Vector2 tempost;
			Square s = new Square ();
			SpriteRenderer spriteRen = new SpriteRenderer ();
			BoxCollider2D b = new BoxCollider2D ();

			for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count; i++) {

				if (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Square_type.Id_type_square == 2 && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].IsOcupated == false) {
					s = new Square ();
					s.Game_square = new GameObject ();
					spriteRen = s.Game_square.AddComponent<SpriteRenderer> ();
					spriteRen.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/BonusSquare") as Sprite;
					spriteRen.sortingOrder = 1;
					b = s.Game_square.AddComponent<BoxCollider2D> ();
					b.enabled = true;
					s.Game_square.AddComponent<RemarkedSquared> ();
					s.Coordinate_x_center = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Coordinate_x_center;
					s.Coordinate_y_center = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Coordinate_y_center;
					remarkedList.Add (s);
				}

			}

			for (int i = 0; i < remarkedList.Count; i++) {
				tempost = remarkedList [i].Game_square.transform.position;
				tempost.x = remarkedList [i].Coordinate_x_center;
				tempost.y = remarkedList [i].Coordinate_y_center;
				remarkedList [i].Game_square.transform.position = tempost;
			}
	
			ListPieces.message_string = "Exchange one blue square\n to another green square";
			isBonusMess = true;
			UndoMovement.isBonusMessBlack = false;
			this.Disable ();
			this.ChangeBackground ();
			this.ActiveCancelButton ();
		} 
		else 
		{
			GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
			Animator anim = imageBackBonus.GetComponent<Animator> ();
			anim.Play ("ShowBonus");
			GameObject titleBonus = GameObject.Find ("TitleBonus");
			Text titleDescription = titleBonus.GetComponent<Text> ();
			titleDescription.text = "Green Square";
			GameObject imageBonus = GameObject.Find ("ImageBonus");
			RectTransform size = imageBonus.GetComponent<RectTransform> ();
			size.sizeDelta = new Vector2 (61.8f, 40f);
			Image photoBonus = imageBonus.GetComponent<Image> ();
			photoBonus.sprite = Resources.Load<Sprite> ("Bonus/GreenSquareBonus") as Sprite;
			GameObject descriptionBonus = GameObject.Find ("DescriptionBonus");
			Text description = descriptionBonus.GetComponent<Text> ();
			description.text = "Exchange one blue square to another green square";
		}
	}

	private void ChangeBackground()
	{
		GameObject background = GameObject.Find ("Background");
		//SpriteRenderer spriteRen = new SpriteRenderer ();
		SpriteRenderer spriteRen = background.GetComponent<SpriteRenderer> ();  
		spriteRen.sprite = Resources.Load<Sprite> ("BackgroundLevel/" + UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].ListLevel[LoadMap.levelGo - 1].File_osc) as Sprite;
		//spriteRen.sprite = Resources.Load<Sprite> ("BackgroundLevel/DarkUnfocusedKnight") as Sprite;
	}

	private void Disable()
	{
		GameObject gameButtonLeft = GameObject.Find ("ButtonLeft");
		GameObject gameButtonRight = GameObject.Find ("ButtonRight");

		BoxCollider2D boxButtonLeft = gameButtonLeft.GetComponent<BoxCollider2D> ();
		BoxCollider2D boxButtonRight = gameButtonRight.GetComponent<BoxCollider2D> ();

		boxButtonLeft.enabled = false;
		boxButtonRight.enabled = false;

		for (int i = 0; i < ListPieces.pieceList.Count; i++) 
		{
			BoxCollider2D b = ListPieces.pieceList [i].Game_piece.GetComponent<BoxCollider2D> ();
			b.enabled = false;
		}

	}

	private void ActiveCancelButton()
	{
		ListPieces.buttonCancel.SetActive (true);
	}
}
