using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusNinePoints : MonoBehaviour {

	//public static bool isBonusMess = false;
	public static List<Square> remarkedList = new List<Square>();
	private AudioSource a;

	void OnMouseDown()
	{
		if (ListPieces.bonusNine > 0) {

			if (PieceDrag.squarePieceList.Count > 0) {
				Vector2 tempost;
				Square s = new Square ();
				SpriteRenderer spriteRen = new SpriteRenderer ();
				BoxCollider2D b = new BoxCollider2D ();
				Sound.GetSound ("BonusScore");

				for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) {

					if (PieceDrag.squarePieceList [i].piece.Piece_score != 9) {
						s = new Square ();
						s.Game_square = new GameObject ();
						spriteRen = s.Game_square.AddComponent<SpriteRenderer> ();
						spriteRen.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/BonusSquare") as Sprite;
						spriteRen.sortingOrder = 4;
						b = s.Game_square.AddComponent<BoxCollider2D> ();
						b.enabled = true;
						s.Game_square.AddComponent<RemarkedNine> ();
						s.Coordinate_x_center = PieceDrag.squarePieceList [i].square.Coordinate_x_center;
						s.Coordinate_y_center = PieceDrag.squarePieceList [i].square.Coordinate_y_center;
						remarkedList.Add (s);
					}
				}
				for (int i = 0; i < remarkedList.Count; i++) {
					tempost = remarkedList [i].Game_square.transform.position;
					tempost.x = remarkedList [i].Coordinate_x_center;
					tempost.y = remarkedList [i].Coordinate_y_center;
					remarkedList [i].Game_square.transform.position = tempost;
				}

				ListPieces.message_string = "Exchange one piece from the board\n to another piece with the same\n number and score 9";
				BonusSquare.isBonusMess = true;
				UndoMovement.isBonusMessBlack = false;
				this.Disable ();
				this.ChangeBackground ();
				this.ActiveCancelButton ();
			} 
			else 
			{
				ListPieces.message_string = "You must to put at least one\n piece on the board to use this bonus";
			}
		} 
		else 
		{
			GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
			Animator anim = imageBackBonus.GetComponent<Animator> ();
			anim.Play ("ShowBonus");
			GameObject titleBonus = GameObject.Find ("TitleBonus");
			Text titleDescription = titleBonus.GetComponent<Text> ();
			titleDescription.text = "Nine Points";
			GameObject imageBonus = GameObject.Find ("ImageBonus");
			RectTransform size = imageBonus.GetComponent<RectTransform> ();
			size.sizeDelta = new Vector2 (50f, 62f);
			Image photoBonus = imageBonus.GetComponent<Image> ();
			photoBonus.sprite = Resources.Load<Sprite> ("Bonus/ScoreBonus") as Sprite;
			GameObject descriptionBonus = GameObject.Find ("DescriptionBonus");
			Text description = descriptionBonus.GetComponent<Text> ();
			description.text = "Exchange one piece from the board to another\n piece with the same number and score 9";
		}
	}

    private void ChangeBackground()
    {
        GameObject background = GameObject.Find("Background");
        SpriteRenderer spriteRen = new SpriteRenderer();
        spriteRen = background.GetComponent<SpriteRenderer>();
        spriteRen.sprite = Resources.Load<Sprite>("BackgroundLevel/" + UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].ListLevel[LoadMap.levelGo - 1].File_osc) as Sprite;
	}

	private void Disable()
	{
		GameObject gameButtonLeft = GameObject.Find ("ButtonRight");
		GameObject gameButtonRight = GameObject.Find ("ButtonLeft");

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
