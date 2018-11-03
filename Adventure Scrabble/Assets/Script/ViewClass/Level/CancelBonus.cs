using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelBonus : MonoBehaviour {

	private AudioSource a;

	void OnMouseDown()
	{
		Sound.GetSound ("CancelButton");
	
		GameObject gameButtonLeft = GameObject.Find ("ButtonLeft");
		GameObject gameButtonRight = GameObject.Find ("ButtonRight");

		BoxCollider2D boxButtonLeft = gameButtonLeft.GetComponent<BoxCollider2D> ();
		BoxCollider2D boxButtonRight = gameButtonRight.GetComponent<BoxCollider2D> ();

		boxButtonLeft.enabled = true;
		boxButtonRight.enabled = true;

		for (int i = 0; i < ListPieces.pieceList.Count; i++) 
		{
			BoxCollider2D b = ListPieces.pieceList [i].Game_piece.GetComponent<BoxCollider2D> ();
			b.enabled = true;
		}
	
		GameObject background = GameObject.Find ("Background");
		SpriteRenderer spriteRen = new SpriteRenderer ();
		spriteRen = background.GetComponent<SpriteRenderer> (); 
		spriteRen.sprite = Resources.Load<Sprite> ("BackgroundLevel/" + UIManagerMenu.g.List_maps[UIManagerMenu.IdMap].ListLevel[LoadMap.levelGo - 1].File) as Sprite;

		ListPieces.buttonCancel.SetActive (false);
		ListPieces.message_string = "";
		this.DestroyRemarkedSquared ();
		this.DestroyRemarkedSquaredNine ();
		UndoMovement.isBonusMessBlack = false;
		BonusSquare.isBonusMess = false;
	}

	private void DestroyRemarkedSquared()
	{
		if (BonusSquare.remarkedList.Count > 0) 
		{
			for (int i = 0; i < BonusSquare.remarkedList.Count; i++)
			{
				Destroy (BonusSquare.remarkedList [i].Game_square);
			}
			BonusSquare.remarkedList.Clear ();
		}
	}

	private void DestroyRemarkedSquaredNine()
	{
		if (BonusNinePoints.remarkedList.Count > 0) {
			for (int i = 0; i < BonusNinePoints.remarkedList.Count; i++) {
				Destroy (BonusNinePoints.remarkedList [i].Game_square);
			}
			BonusNinePoints.remarkedList.Clear ();
		}
	}
}
