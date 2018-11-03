using System.Collections.Generic;
using UnityEngine;

public class PieceDrag : MonoBehaviour {

	private Vector2 temPost;
	private int topIndex;
	public static List<SquarePiece> squarePieceList = new List<SquarePiece>();
	SquarePiece sp = new SquarePiece ();
	public static bool first_piece =  true;
	public static List<Square> adjacentList = new List<Square>();
	private SpriteRenderer square_sprite;
	private static int total_sum_hor = 0;
	private static int total_sum_ver = 0;
	private static int total_score_hor = 0;
	private static int total_score_ver = 0;
	public static int total_score_gral = 0;
	private static int total_pieces_hor = 0;
	private static int total_pieces_ver = 0;
	private static int total_score_hor_ver = 0;
	private bool is_sum_vertical = false;
	private bool is_ady_vertical = false;
	private Animator anim;
	private SpriteRenderer spriteAnimator;
	private static int total_score = 300;
	private float size_x = 1.00490f;
	public static int green_time = 0;
	public static int yellow_time = 0;
	public static int red_time = 0;
	public static bool firstOk = false;
	public static bool secondOk = false;

	//Mouse Events
	void OnMouseDown()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = (AudioClip)Resources.Load ("Sound/click1");

		if (UICommon.soundActive == true) 
		{
			audio.Play ();
		} 
		else 
		{
			if (audio.isPlaying == true) 
			{
				audio.Stop();
			}
		}

		bool last = false;
		int deletedIndex = GetIndex();
		topIndex = deletedIndex;
		sp.piece = ListPieces.pieceList [deletedIndex];
		/*squarePieceList.Add(ListPieces.pieceList[deletedIndex])*/
		ListPieces.pieceList.RemoveAt(deletedIndex);
		PieceManagerRight.topMax--;

		if(deletedIndex <= ListPieces.pieceList.Count - 1)
		{
			while (last == false) {
				temPost = ListPieces.pieceList[deletedIndex].Game_piece.transform.position;
				if (temPost.x > 1.30f && temPost.x < 2f)
				{
				   ListPieces.pieceList [deletedIndex].Game_piece.SetActive (true);
				}
			
				temPost.x = temPost.x - 0.6f;
				temPost.y = -1.97f;
				ListPieces.pieceList [deletedIndex].Game_piece.transform.position = temPost;

				if (deletedIndex < ListPieces.pieceList.Count - 1)
				{
					deletedIndex++;
				} 
				else 
				{
					last = true;
				}

			}
		}

		UndoMovement.isBonusMessBlack = false;
		BonusSquare.isBonusMess = false;
	}

	void OnMouseDrag()
	{
		temPost = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (temPost);
		transform.position = objPosition;
	}

	void OnMouseUp()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = (AudioClip)Resources.Load ("Sound/mouserelease1");

		if (UICommon.soundActive == true) 
		{
			audio.Play ();
		} 
		else 
		{
			if (audio.isPlaying == true) 
			{
				audio.Stop();
			}
		}
			
		bool top_x = false;
		bool top_y = false;
		bool outside = false;
		int location_x = 0;
		int location_y = 0;

		temPost = transform.position;

		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && top_x == false; i++) {

			if (transform.position.x < - 3.63  || transform.position.x > 3.95) 
			{
				ListPieces.message_string = "Don't put pieces outside\nthe board";
				ListPieces.pieceList.Add (sp.piece);
				temPost.x = ListPieces.pieceList[ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
				temPost.y = -1.97f;
				PieceManagerRight.topMax++;
				if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) 
				{
					ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
				} 
				else 
				{
					ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
				}
				top_x = true;
				outside = true;
			}
			else if (transform.position.x < -2.88 && transform.position.x > -3.63) 
			{
				temPost.x = -2.525f;
				location_x = 1;
				top_x = true;
			} 
			else if (transform.position.x > 2.244 && transform.position.x < 3.95) 
			{
				temPost.x = 2.6f;
				location_x = 9;
				top_x = true;
			} 
			else 
			{
				if (transform.position.x > (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Coordinate_x_border - 2.858f) && transform.position.x < (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i + 1].Coordinate_x_border - 2.858f)) {

					temPost.x = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].Coordinate_x_center;
					location_x = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].Location_x;
					top_x = true;
				}
			}
		}

		if (outside == false) {
			for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && top_y == false; i++) {
				if (transform.position.y < -2.94 || transform.position.y > 5) {
					ListPieces.message_string = "Don't put pieces outside\nthe board";
					ListPieces.pieceList.Add (sp.piece);
					temPost.x = ListPieces.pieceList [ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
					temPost.y = -1.97f;
					PieceManagerRight.topMax++;
					if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) 
					{
						ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
					} 
					else 
					{
						ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
					}
					top_y = true;
				} else if (transform.position.y > -2.94 && transform.position.y < -0.784) {

					temPost.y = -1.12f;
					location_y = 9;
					top_y = true;
				} else if (transform.position.y > 4.27 && transform.position.y < 5) {

					temPost.y = 4.001f;
					location_y = 1;
					top_y = true;
				} else {
					if (transform.position.y < (-UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].Coordinate_y_border + 4.336f) && transform.position.y > (-UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i + 1].Coordinate_y_border + 4.336f)) {

						temPost.y = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].Coordinate_y_center;
						location_y = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].Location_y;
						top_y = true;
					}
				}
			}
			ListPieces.message_string = "";

			if (this.ChangeOcupation (location_x, location_y) == false) 
			{
				if (this.CheckAdjLocation (location_x, location_y) == false) {
					ListPieces.message_string = "Don't put pieces outside\n the adyacents frontier";
					ListPieces.pieceList.Add (sp.piece);
					temPost.x = ListPieces.pieceList [ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
					temPost.y = -1.97f;
					PieceManagerRight.topMax++;
					if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) 
					{
						ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
					} 
					else 
					{
						ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
					}
					sp.square.IsOcupated = false;
					transform.position = temPost;
				} 
				else 
				{
					transform.position = temPost;
					gameObject.GetComponent<BoxCollider2D> ().enabled = false;
					squarePieceList.Add (sp);
					this.CheckSumVer (location_x, location_y);
					this.CheckSumHor (location_x, location_y);
					this.ReachObjectives ();
				}
			} 
			else
			{
				transform.position = temPost;
				gameObject.GetComponent<BoxCollider2D> ().enabled = false;
				squarePieceList.Add (sp);
				this.CheckSumVer (location_x, location_y);
				this.CheckSumHor (location_x, location_y);
				this.ReachObjectives ();
			}
		} 
		else 
		{
			transform.position = temPost;
		}

		outside = false;
	}
	/*--------------------------------------------------------------------------------------------------------------------*/

	//Calculating and Update Fuctions

	private bool ChangeOcupation (int pLocation_x, int pLocation_y)
	{
		bool changed = false;
		int n = 1;

		while(this.GetIndexBoardLocation(pLocation_x, pLocation_y, false).IsOcupated == true)
		{
			changed = true;

			if(this.GetIndexBoardLocation(pLocation_x + n, pLocation_y, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x + n, pLocation_y, false).Coordinate_x_center;
				pLocation_x = pLocation_x + n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x + n, pLocation_y + n, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x + n, pLocation_y + n, false).Coordinate_x_center;
				temPost.y = this.GetIndexBoardLocation(pLocation_x + n, pLocation_y + n, false).Coordinate_y_center;

				pLocation_x = pLocation_x + n;
				pLocation_y = pLocation_y + n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x, pLocation_y + n, false).IsOcupated == false)
			{
				temPost.y = this.GetIndexBoardLocation(pLocation_x, pLocation_y + n, false).Coordinate_y_center;

				pLocation_y = pLocation_y + n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x - n, pLocation_y + n, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x - n, pLocation_y + n, false).Coordinate_x_center;
				temPost.y = this.GetIndexBoardLocation(pLocation_x - n, pLocation_y + n, false).Coordinate_y_center;

				pLocation_x = pLocation_x - n;
				pLocation_y = pLocation_y + n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x - n, pLocation_y, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x - n, pLocation_y, false).Coordinate_x_center;

				pLocation_x = pLocation_x - n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x - n, pLocation_y - n, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x - n, pLocation_y - n, false).Coordinate_x_center;
				temPost.y = this.GetIndexBoardLocation(pLocation_x - n, pLocation_y - n, false).Coordinate_y_center;

				pLocation_x = pLocation_x - n;
				pLocation_y = pLocation_y - n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x, pLocation_y - n, false).IsOcupated == false)
			{
				temPost.y = this.GetIndexBoardLocation(pLocation_x, pLocation_y - n, false).Coordinate_y_center;

				pLocation_y = pLocation_y - n;
			}
			else if(this.GetIndexBoardLocation(pLocation_x + n, pLocation_y - n, false).IsOcupated == false)
			{
				temPost.x = this.GetIndexBoardLocation(pLocation_x + n, pLocation_y - n, false).Coordinate_x_center;
				temPost.y = this.GetIndexBoardLocation(pLocation_x + n, pLocation_y - n, false).Coordinate_y_center;

				pLocation_x = pLocation_x + n;
				pLocation_y = pLocation_y - n;
			}

			n++;
		}
		sp.square = this.GetIndexBoardLocation (pLocation_x, pLocation_y, true);
		return changed;
	}


	private void ShowAdjacentPieces()
	{
		if(adjacentList.Count > 0)
		{
			for (int i = 0; i < adjacentList.Count; i++)
			{
				Destroy (adjacentList [i].Game_square);
			}
		}

		adjacentList = new List<Square> ();

		for (int i = 0; i < squarePieceList.Count; i++) 
		{
			int loc_adj_x1 = squarePieceList[i].square.Location_x;
			int loc_adj_y1 = squarePieceList [i].square.Location_y - 1;
			int loc_adj_x2 = squarePieceList [i].square.Location_x + 1;
			int loc_adj_y2 = squarePieceList [i].square.Location_y;
			int loc_adj_x3 = squarePieceList [i].square.Location_x;
			int loc_adj_y3 = squarePieceList [i].square.Location_y + 1;
			int loc_adj_x4 = squarePieceList [i].square.Location_x - 1;
			int loc_adj_y4 = squarePieceList [i].square.Location_y;

			if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))
			{   
				Square adjacent = new Square ();
				adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
				adjacentList.Add (adjacent);
				this.GetSquarePerType (adjacent);
				temPost = adjacent.Game_square.transform.position;
				temPost.x =  adjacent.Coordinate_x_center;
				temPost.y =  adjacent.Coordinate_y_center;
				adjacent.Game_square.transform.position = temPost;
			}
			if((this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x2, loc_adj_y2) == false))
			{
				Square adjacent = new Square ();
				adjacent = this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false);
				adjacentList.Add (adjacent);
				this.GetSquarePerType (adjacent);
				temPost = adjacent.Game_square.transform.position;
				temPost.x =  adjacent.Coordinate_x_center;
				temPost.y =  adjacent.Coordinate_y_center;
				adjacent.Game_square.transform.position = temPost;
			}
			if((this.GetIndexBoardLocation (loc_adj_x3, loc_adj_y3, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x3, loc_adj_y3) == false))
			{
				Square adjacent = new Square ();
				adjacent = this.GetIndexBoardLocation (loc_adj_x3, loc_adj_y3, false);
				adjacentList.Add (adjacent);
				this.GetSquarePerType (adjacent);
				temPost = adjacent.Game_square.transform.position;
				temPost.x =  adjacent.Coordinate_x_center;
				temPost.y =  adjacent.Coordinate_y_center;
				adjacent.Game_square.transform.position = temPost;
			}
			if((this.GetIndexBoardLocation (loc_adj_x4, loc_adj_y4, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x4, loc_adj_y4) == false))
			{
				Square adjacent = new Square ();
				adjacent = this.GetIndexBoardLocation (loc_adj_x4, loc_adj_y4, false);
				adjacentList.Add (adjacent);
				this.GetSquarePerType (adjacent);
				temPost = adjacent.Game_square.transform.position;
				temPost.x =  adjacent.Coordinate_x_center;
				temPost.y =  adjacent.Coordinate_y_center;
				adjacent.Game_square.transform.position = temPost;
			}

		}
	}

	private void ShowAdjacentPiecesHor()
	{
		if (this.is_sum_vertical == false) {
			if (adjacentList.Count > 0) {
				for (int i = 0; i < adjacentList.Count; i++) {
					Destroy (adjacentList [i].Game_square);
				}
			}		
			adjacentList = new List<Square> ();
		}

		for (int i = 0; i < squarePieceList.Count; i++) {
			if (squarePieceList [i].isScored == false) {
				int loc_adj_x1 = squarePieceList [i].square.Location_x + 1;
				int loc_adj_y1 = squarePieceList [i].square.Location_y;
				int loc_adj_x2 = squarePieceList [i].square.Location_x - 1;
				int loc_adj_y2 = squarePieceList [i].square.Location_y;

				if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))  {   
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
					adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
				if ((this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x2, loc_adj_y2) == false)) {
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false);
					adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
			}
		}
	}

	private void ShowAdjacentPiecesVer()
	{
		if(adjacentList.Count > 0)
		{
			for (int i = 0; i < adjacentList.Count; i++)
			{
				Destroy (adjacentList [i].Game_square);
			}
		}

		adjacentList = new List<Square> ();

		for (int i = 0; i < squarePieceList.Count; i++) {
			if (squarePieceList [i].isScored == false) {
				int loc_adj_x1 = squarePieceList [i].Square.Location_x;
				int loc_adj_y1 = squarePieceList [i].Square.Location_y - 1;
				int loc_adj_x2 = squarePieceList [i].Square.Location_x;
				int loc_adj_y2 = squarePieceList [i].Square.Location_y + 1;

				if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))  {   
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
					adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
				if ((this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x2, loc_adj_y2) == false)) {
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false);
					adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
			}
		}
	}
		
	public void CheckSumHor(int pLocation_x, int pLocation_y)
	{
		Vector2 position;
		int square_score = 1;
		int block_score = 1;
		bool stop_one = false;
		bool stop_two = false;

		for (int i = pLocation_x; i>0 && stop_one == false; i--) 
		{
			if (GetIndexBoardLocation (i, pLocation_y, false).IsOcupated == true) 
			{
				block_score *= GetScorePerBlockType (this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square);
				total_score_hor += GetSquarePiece (i, pLocation_y).piece.Piece_score * square_score;
				total_sum_hor += GetSquarePiece (i, pLocation_y).piece.Piece_number;
				if (this.is_sum_vertical == false && i == pLocation_x)
				{
					CountPieces (GetSquarePiece (i, pLocation_y).piece.Piece_number, true);
				}
				if (i < pLocation_x) 
				{
					squarePieceList [GetSquarePieceIndex (i, pLocation_y)].isScored = false;				
				}
				++total_pieces_hor;
			}
			else 
			{
				stop_one = true;
			}

		}

		for (int i = pLocation_x + 1; i<10 && stop_two == false; i++) 
		{
			if(GetIndexBoardLocation(i, pLocation_y, false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square);
				total_score_hor += GetSquarePiece (i, pLocation_y).piece.Piece_score * square_score;
				total_sum_hor += GetSquarePiece (i, pLocation_y).piece.Piece_number;
				if (this.is_sum_vertical == false && i == pLocation_x)
				{
					CountPieces (GetSquarePiece (i, pLocation_y).piece.Piece_number, true);
				}
				if (i>= pLocation_x + 1) 
				{
					squarePieceList [GetSquarePieceIndex (i, pLocation_y)].isScored = false;				
				}
				++total_pieces_hor;
			}
			else 
			{
				stop_two = true;
			}
		}

		if (total_sum_hor >= 0 && total_sum_hor < 10) {

			if (first_piece == true) 
			{
				this.ShowAdjacentPieces ();
				first_piece = false;
			}

			if (total_pieces_hor > 1) 
			{
				if (this.is_ady_vertical == false) 
				{
					ShowAdjacentPiecesHor ();
				}
			} 

		} 
		else if (total_sum_hor == 10) {
			total_score_gral += total_score_hor * block_score;
			total_pieces_hor = 0;
			if (this.is_ady_vertical == false) 
			{
				this.ShowAdjacentPieces();
			}
			this.UpdateScoreBar ();
			this.SetIsScored (); 
			this.ShowAnimatedMessage (total_score_hor * block_score);
			this.ShowAnimatedMessage (total_score_hor_ver * block_score);
		} 

		else 
		{
			ListPieces.message_string = "The line must sum 10";
			ListPieces.pieceList.Add (sp.piece);
			position.x = ListPieces.pieceList [ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
			position.y = -1.97f;
			PieceManagerRight.topMax++;
			if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) 
			{
				ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
			} 
			else 
			{
				ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
			}
			CountPieces (sp.piece.Piece_number, false);

			if (this.is_sum_vertical == true) {
				squarePieceList [this.GetSquarePieceIndex(pLocation_x, pLocation_y)].square.IsOcupated = false;
				squarePieceList.Remove (sp);
				this.ShowAdjacentPieces ();
			} 
			else if (this.is_ady_vertical == true) {
				GameObject gameAnimator = GameObject.Find ("AnimationScore"); 
				spriteAnimator = gameAnimator.GetComponent<SpriteRenderer> ();
				spriteAnimator.enabled = false;
				this.UndoSum (pLocation_x, pLocation_y);
				squarePieceList [this.GetSquarePieceIndex(pLocation_x, pLocation_y)].square.IsOcupated = false;
				squarePieceList.Remove (sp);
				this.ShowAdjacentPieces ();
			}

			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			transform.position = position;
		}

		total_pieces_hor = 0;
		total_score_hor = 0;
		total_sum_hor = 0;
		total_score_hor_ver = 0;
	}

	public void CheckSumVer(int pLocation_x, int pLocation_y)
	{
		Vector2 position;
		int square_score = 1;
		int block_score = 1;
		bool stop_one = false;
		bool stop_two = false;

		for (int i = pLocation_y; i>0 && stop_one == false; i--) 
		{
			if(GetIndexBoardLocation(pLocation_x,i,false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square);
				total_score_ver += GetSquarePiece (pLocation_x, i).piece.Piece_score * square_score;
				total_sum_ver += GetSquarePiece (pLocation_x,i).piece.Piece_number;
				if (i == pLocation_y) 
				{
					CountPieces (GetSquarePiece (pLocation_x, i).piece.Piece_number, true);
				}
				if (i < pLocation_y) 
				{
					squarePieceList [GetSquarePieceIndex (pLocation_x, i)].isScored = false;				
				}
				++total_pieces_ver;
			}
			else 
			{
				stop_one = true;
			}
		}

		for (int i = pLocation_y + 1; i<10 && stop_two == false; i++) 
		{
			if(GetIndexBoardLocation(pLocation_x,i,false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square);
				total_score_ver += GetSquarePiece (pLocation_x, i).piece.Piece_score * square_score;
				total_sum_ver += GetSquarePiece (pLocation_x,i).piece.Piece_number;
				if (i == pLocation_y) 
				{
					CountPieces (GetSquarePiece (pLocation_x, i).piece.Piece_number, true);
				}
				if (i >= pLocation_y + 1) 
				{
					squarePieceList [GetSquarePieceIndex (pLocation_x, i)].isScored = false;				
				}
				++total_pieces_ver;
			}
			else 
			{
				stop_two = true;
			}
		}

		if (total_sum_ver >= 0 && total_sum_ver < 10) {
			if (first_piece == true) {
				this.ShowAdjacentPieces ();
				first_piece = false;
			}

			if (total_pieces_ver > 1) {
				ShowAdjacentPiecesVer ();
				this.is_sum_vertical = true;
			} 
		} 
		else if (total_sum_ver == 10) {
			total_score_gral += total_score_ver * block_score;
			total_pieces_ver = 0;
			this.is_ady_vertical = true;
			this.ShowAdjacentPieces ();
			this.UpdateScoreBar ();
			this.SetIsScored ();
			this.ShowAnimatedMessage (total_score_ver * block_score);
			this.ShowAnimatedMessage (total_score_hor_ver * block_score);
		} 
		else {
			ListPieces.message_string = "The line must sum 10";
			ListPieces.pieceList.Add (sp.piece);
			position.x = ListPieces.pieceList [ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
			position.y = -1.97f;
			PieceManagerRight.topMax++;
			if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) 
			{
				ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
			} 
			else 
			{
				ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
			}
			CountPieces (sp.piece.Piece_number, false);
			sp.square.IsOcupated = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			squarePieceList.Remove (sp);
			transform.position = position;
		}

		total_pieces_ver = 0;
		total_score_ver = 0;
		total_sum_ver = 0;
	}
		
	private void  UpdateScoreBar()
	{
		GameObject bar = GameObject.Find ("ScoreBarAhead");
		GameObject starv = GameObject.Find ("GreenStar");
		GameObject stara = GameObject.Find ("YellowStar");
		GameObject starr = GameObject.Find ("RedStar");

		AudioSource starg_sound = starv.GetComponent<AudioSource>();
		AudioSource stary_sound = starv.GetComponent<AudioSource>();
		AudioSource starr_sound = starv.GetComponent<AudioSource>();

		temPost = bar.transform.position;
		temPost.x = -5.195f;
		size_x = 1.00490f;

		if (total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[2].Score_level) 
		{
			size_x = 0f;
			temPost.x = temPost.x + 1.00490f; 			
		} 
		else 
		{
			size_x = size_x - ((size_x * total_score_gral) / total_score);
			temPost.x = temPost.x + ((1.00490f * total_score_gral) / total_score); 
		}

		bar.transform.position = temPost;
		bar.transform.localScale = new Vector2(size_x,0.98f);

		if(temPost.x >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[0].Loc_star)
		{
			ListPieces.particle_one.SetActive (true);
			if (green_time == 0) 
			{
				starg_sound.Play ();
				green_time++;
			}
		}

		if(temPost.x >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[1].Loc_star)
		{
			ListPieces.particle_two.SetActive (true);
			if (yellow_time == 0) 
			{
				stary_sound.Play ();
				yellow_time++;
			}
		}

		if(temPost.x >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[2].Loc_star)
		{
			ListPieces.particle_three.SetActive (true);
			if (red_time == 0) 
			{
				starr_sound.Play ();
				red_time++;
			}
		}

	}

	private void SetIsScored()
	{
		for (int i = 0; i < squarePieceList.Count; i++)
		{
			squarePieceList[i].isScored = true;
		}
	}

	private void UndoSum(int pLocation_x, int pLocation_y)
	{
		int square_score = 1;
		int block_score = 1;
		bool stop_one = false;
		bool stop_two = false;

		for (int i = pLocation_y; i>0 && stop_one == false; i--) 
		{
			if(GetIndexBoardLocation(pLocation_x,i,false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square);
				total_score_ver += GetSquarePiece (pLocation_x, i).piece.Piece_score * square_score;
			}
			else 
			{
				stop_one = true;
			}
		}

		for (int i = pLocation_y + 1; i<10 && stop_two == false; i++) 
		{
			if(GetIndexBoardLocation(pLocation_x,i,false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (pLocation_x, i).square.Square_type.Id_type_square);
				total_score_ver += GetSquarePiece (pLocation_x, i).piece.Piece_score * square_score;
			}
			else 
			{
				stop_two = true;
			}
		}

		total_score_gral -= total_score_ver * block_score;
		total_score_ver = 0;

		UpdateScoreBar ();
	}

	private void CountPieces(int pNumberPiece, bool pIsPositiveCount)
	{
		for (int i = (8 + 3*pNumberPiece); i < (11 + 3*pNumberPiece); i++) 
		{
			if (pIsPositiveCount == true) 
			{
				UIManagerMenu.g.P.ListAchievements [i].Count++;
			} 
			else 
			{
				UIManagerMenu.g.P.ListAchievements [i].Count--;
			}
		}
	}

	private void ShowAnimatedMessage(int pScore)
	{
		GameObject gameAnimator = GameObject.Find ("AnimationScore"); 
		spriteAnimator = gameAnimator.GetComponent<SpriteRenderer> ();
		spriteAnimator.enabled = true;
		anim = gameAnimator.GetComponent<Animator> ();

		if (20 <= pScore && pScore < 35) 
		{
			anim.Play ("ShowMessageGood");
		} 
		else if (35 <= pScore && pScore < 50)
		{
			anim.Play ("ShowMessageGreat");
		} 
		else if (50 <= pScore && pScore < 90)
		{
			anim.Play ("ShowMessageFantastic");
		} 
		else if (90 <= pScore && pScore < 150) 
		{
			anim.Play ("ShowMessageWonderful");
		}
		else if (pScore >= 150) 
		{
			anim.Play ("ShowMessageBrilliant");
		}
	}
		
	public void ReachObjectives()
	{
		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list.Count; i++) 
		{
			int idType = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Type_obj.Id_type_objective;

			if (idType == 1) 
			{
				if (total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value && firstOk == false) {
					ListPieces.okObjective1.SetActive (true);
					Animator okObjective1_anim = ListPieces.okObjective1.GetComponent<Animator> ();
					okObjective1_anim.Play ("ReachObjective");
					firstOk = true;
				}
			} 
			else if (idType == 2) 
			{
				if(ListPieces.pieceList.Count < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value && secondOk == false)
				{
					ListPieces.okObjective2.SetActive (true);
					Animator okObjective2_anim = ListPieces.okObjective2.GetComponent<Animator> ();
					okObjective2_anim.Play ("ReachObjective");
					secondOk = true;
				}
			}

			//Include the rest of the idTypes later
		}

	}

	/*--------------------------------------------------------------------------------------------------------------------*/

	//Searching Functions
	private bool CheckAdjLocation(int pLocation_x, int pLocation_y)
	{
		bool ok = false;

		if (first_piece == true) {
			Square adjacent = new Square ();
			adjacent = this.GetIndexBoardLocation (5, 5, false);
			adjacentList.Add (adjacent);
		}

		for (int i = 0; i < adjacentList.Count; i++)
		{
			if (adjacentList[i].Location_x == pLocation_x && adjacentList[i].Location_y == pLocation_y) 
			{
				ok = true;
			}
		}
		return ok;
	}

	private int GetIndex()
	{
		int index = 0;
		bool found = false;

		for (int i = 0; i< ListPieces.pieceList.Count && found == false; i++)
		{
			if (gameObject == ListPieces.pieceList[i].Game_piece) {
				index = i;
				found = true;
			}
		}

		return index;
	}
		
	private Square GetIndexBoardLocation(int pLocation_x, int pLocation_y, bool pOcupy)
	{
		Square s = new Square();
		bool found = false;

		//UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count

		for (int i = 0; i< UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && found == false; i++)
		{
			if (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_x == pLocation_x && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_y == pLocation_y)
			{
				if (pOcupy == true)
				{
					UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].IsOcupated = true;
				} 
	
				s = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i];
				found = true;
			}
		}

		if (found == false) 
		{
			s.IsOcupated = true;
		}

		return s;
	}

	public SquarePiece GetSquarePiece(int pLocation_x, int pLocation_y)
	{
		SquarePiece sq = new SquarePiece (); 

		for (int i = 0; i < squarePieceList.Count; i++) 
		{
			if(squarePieceList[i].square.Location_x == pLocation_x && squarePieceList[i].square.Location_y == pLocation_y)
			{
				sq = squarePieceList [i];

			}
		}

		return sq;
	}


	public int GetSquarePieceIndex(int pLocation_x, int pLocation_y)
	{
		int index = 0;

		for (int i = 0; i < squarePieceList.Count; i++) 
		{
			if(squarePieceList[i].square.Location_x == pLocation_x && squarePieceList[i].square.Location_y == pLocation_y)
			{
				index = i;
			}
		}

		return index;
	}

	private int GetScorePerSquareType(int pIdTypeSquare)
	{
		int multiply_square = 0;

		if (pIdTypeSquare == 2) 
		{
			multiply_square = 2;
		} 
		else if (pIdTypeSquare == 6) 
		{
			multiply_square = 3;
		} 
		else 
		{
			multiply_square = 1;
		}

		return multiply_square;
	}

	private int GetScorePerBlockType(int pIdTypeSquare)
	{
		int multiply_block = 0;

		if (pIdTypeSquare == 1) 
		{
			multiply_block = 2;
		} 
		else if (pIdTypeSquare == 5) 
		{
			multiply_block = 3;
		} 
		else 
		{
			multiply_block = 1;
		}

		return multiply_block;
	}

	private void GetSquarePerType(Square sq)
	{
		sq.Game_square = new GameObject ();
		square_sprite = sq.Game_square.AddComponent<SpriteRenderer> ();

		if (sq.Square_type.Id_type_square == 1) 
		{
			square_sprite.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/RemarkedYellowSquare") as Sprite;
		} 
		else if (sq.Square_type.Id_type_square == 2) 
		{
			square_sprite.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/RemarkedBlueSquare") as Sprite;
		} 
		else if (sq.Square_type.Id_type_square == 5) 
		{
			square_sprite.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/RemarkedRedSquare") as Sprite;
		} 
		else if (sq.Square_type.Id_type_square == 6) 
		{
			square_sprite.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/RemarkedGreenSquare") as Sprite;
		} 
		else if (sq.Square_type.Id_type_square == 7) 
		{
			square_sprite.sprite = Resources.Load<Sprite> ("RemarkedSquareDrawing/RemarkedSimpleSquare") as Sprite;
		}

		square_sprite.sortingOrder = 1;
	}

	/*--------------------------------------------------------------------------------------------------------------------*/
}
	