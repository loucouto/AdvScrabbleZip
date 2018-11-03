using UnityEngine;

public class RemarkedSquared : MonoBehaviour {

	void OnMouseDown()
	{
		SpriteRenderer spriteRen = new SpriteRenderer ();
		spriteRen = gameObject.GetComponent<SpriteRenderer> ();
		spriteRen.sprite = Resources.Load<Sprite> ("SquareDrawing/GreenSquare") as Sprite;
		this.ChangeSquareBoard ();
		this.ChangeBackground ();
		this.DestroyRemarkedSquared ();
		this.Enable ();
		ListPieces.message_string = "";
		BonusSquare.isBonusMess = false;
		ListPieces.bonusPerThree--;
		UIManagerMenu.g.P.ListBonus [1].Count_bonus--;
		new Persistence ().UpdateBonus (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus [1]);
		this.DisactiveCancelButton ();
	}

	private void ChangeSquareBoard()
	{
		bool found = false;
		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && found == false; i++) 
		{
			if (gameObject.transform.position.x == UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Coordinate_x_center && gameObject.transform.position.y == UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Coordinate_y_center) 
			{
				UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Square_type.Id_type_square= 6;
				found = true;
			}
		}
	}

	private void ChangeBackground()
	{
		GameObject background = GameObject.Find ("Background");
		SpriteRenderer spriteRen = new SpriteRenderer ();
		spriteRen = background.GetComponent<SpriteRenderer> (); 
		spriteRen.sprite = Resources.Load<Sprite> ("BackgroundLevel/" + UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].ListLevel[LoadMap.levelGo - 1].File) as Sprite;
	}
		
	private void DestroyRemarkedSquared()
	{
		for (int i = 0; i < BonusSquare.remarkedList.Count; i++)
		{
			if (gameObject.transform.position.x != BonusSquare.remarkedList [i].Coordinate_x_center || gameObject.transform.position.y != BonusSquare.remarkedList [i].Coordinate_y_center) {

				Destroy (BonusSquare.remarkedList [i].Game_square);
			}
		}
		BonusSquare.remarkedList.Clear ();
	}

	private void Enable()
	{
		GameObject gameBotonLeft = GameObject.Find ("ButtonLeft");
		GameObject gameBotonRight = GameObject.Find ("ButtonRight");

		BoxCollider2D boxBotonLeft = gameBotonLeft.GetComponent<BoxCollider2D> ();
		BoxCollider2D boxBotonRight = gameBotonRight.GetComponent<BoxCollider2D> ();

		boxBotonLeft.enabled = true;
		boxBotonRight.enabled = true;

		for (int i = 0; i < ListPieces.pieceList.Count; i++) 
		{
			BoxCollider2D b = ListPieces.pieceList [i].Game_piece.GetComponent<BoxCollider2D> ();
			b.enabled = true;
		}

	}

	private void DisactiveCancelButton()
	{
		ListPieces.buttonCancel.SetActive (false);
	}
		
}
