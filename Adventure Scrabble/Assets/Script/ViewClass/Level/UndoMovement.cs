using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoMovement : MonoBehaviour {

	private Vector2 temPost;
	private static int total_score_hor = 0;
	private static int total_score_ver = 0;
	private static int total_piece_hor = 0;
	private static int total_piece_ver = 0;
	private static int total_sum_ver = 0;
	private static int total_sum_hor = 0;
	private static int total_score = 300;
	private bool is_sum_vertical = false;
	private bool is_ady_vertical = false;
	private static bool first_piece =  false;
	private float size_x = 1.00490f;
	private SpriteRenderer square_sprite;
	public static bool isBonusMessBlack = false;
	private AudioSource a;

	void OnMouseDown()
	{
		if (PieceDrag.first_piece == false) {
			if (ListPieces.bonusUndo > 0) {
				Sound.GetSound ("UndoBonus");
			
				ListPieces.message_string = "Undo movement made\n with success";
				//substract values
				this.SumHor (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_y, true);
				this.SumVer (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_y, true);
	
				//undo the last achievement count
				CountPieces (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, false);

				//add the removed piece to ListPieces
				ListPieces.pieceList.Add (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].piece);

				//put the removed piece to the last place of the queue
				temPost.x = ListPieces.pieceList [ListPieces.pieceList.Count - 2].Game_piece.transform.position.x + 0.6f;
				temPost.y = -1.97f;
				PieceManagerRight.topMax++;
				if (PieceManagerRight.count >= ListPieces.pieceList.Count - 5) {
					ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (true);
				} else {
					ListPieces.pieceList [ListPieces.pieceList.Count - 1].Game_piece.SetActive (false);
				}

				//Set the square as unocupated (this was ocupated presviously with the removed piece) 
				this.SetBoardVacate (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_y);

				//put the piece to the saved temPost position (why I don't do that with ListPieces)
				PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].piece.Game_piece.transform.position = temPost;

				//Enable the boxColider of the removed piece
				BoxCollider2D b = PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].piece.Game_piece.GetComponent<BoxCollider2D> ();
				b.enabled = true;

				// Delete the removed piece from the PieceDrag
				PieceDrag.squarePieceList.RemoveAt (PieceDrag.squarePieceList.Count - 1);

				ListPieces.bonusUndo--;
				UIManagerMenu.g.P.ListBonus [2].Count_bonus--;
				new Persistence ().UpdateBonus (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus [2]);

				//when the count pieces is equal to zero
				this.SetFirstPiece ();

				//sum again showing the real value.
				this.ShowAdySumVer (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_y);
				this.ShowAdySumHor (PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_x, PieceDrag.squarePieceList [PieceDrag.squarePieceList.Count - 1].square.Location_y);

				DeleteObjectives ();

				isBonusMessBlack = true;
				BonusSquare.isBonusMess = false;
			} else {
				GameObject imageBackBonus = GameObject.Find ("ImageBackBonus");
				Animator anim = imageBackBonus.GetComponent<Animator> ();
				anim.Play ("ShowBonus");
				GameObject titleBonus = GameObject.Find ("TitleBonus");
				Text titleDescription = titleBonus.GetComponent<Text> ();
				titleDescription.text = "Undo Bonus";
				GameObject imageBonus = GameObject.Find ("ImageBonus");
				RectTransform size = imageBonus.GetComponent<RectTransform> ();
				size.sizeDelta = new Vector2 (58f, 52f);
				Image photoBonus = imageBonus.GetComponent<Image> ();
				photoBonus.sprite = Resources.Load<Sprite> ("Bonus/UndoBonus") as Sprite;
				GameObject descriptionBonus = GameObject.Find ("DescriptionBonus");
				Text description = descriptionBonus.GetComponent<Text> ();
				description.text = "Undo the last movement of the piece";
			}
		} 
		else 
		{
			GameObject descriptionBonus = GameObject.Find ("DescriptionBonus");
			Text description = descriptionBonus.GetComponent<Text> ();
			description.text = "You must to make the first movement at least";
		}
	}

	private void SetBoardVacate(int pLocation_x, int pLocation_y)
	{
		bool found = false;

		for (int i = 0; i< UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && found == false; i++)
		{
			if (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_x == pLocation_x && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_y == pLocation_y)
			{
				UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list [i].IsOcupated = false;
				    found = true;
			}
		}
	}

	private void SumVer(int pLocation_x, int pLocation_y, bool pResto)
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
				total_sum_ver += GetSquarePiece (pLocation_x,i).piece.Piece_number;
				++total_piece_ver;
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
				++total_piece_ver;
			}
			else 
			{
				stop_two = true;
			}
		}
		if (total_piece_ver > 1 && total_sum_ver == 10) {
			if (pResto == true) {
				PieceDrag.total_score_gral -= total_score_ver * block_score;
			} else {
				PieceDrag.total_score_gral += total_score_ver * block_score;
			}
			UpdateScoreBar ();
		}
		total_score_ver = 0;
		total_piece_ver = 0;
		total_sum_ver = 0;
	}

	private void SumHor(int pLocation_x, int pLocation_y, bool pResto)
	{
		int square_score = 1;
		int block_score = 1;
		bool stop_one = false;
		bool stop_two = false;

		for (int i = pLocation_x; i>0 && stop_one == false; i--) 
		{
			if(GetIndexBoardLocation(i, pLocation_y, false).IsOcupated == true)
			{
				block_score *= GetScorePerBlockType(this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square); 
				square_score = GetScorePerSquareType (this.GetSquarePiece (i, pLocation_y).square.Square_type.Id_type_square);
				total_score_hor += GetSquarePiece (i, pLocation_y).piece.Piece_score * square_score;
				total_sum_hor += GetSquarePiece (i, pLocation_y).piece.Piece_number;
				++total_piece_hor;
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
				++total_piece_hor;
			}
			else 
			{
				stop_two = true;
			}
		}
		if (total_piece_hor> 1 && total_sum_hor == 10) {
			if (pResto == true) {
				PieceDrag.total_score_gral -= total_score_hor * block_score;
			} else {
				PieceDrag.total_score_gral += total_score_hor * block_score;
			}
			UpdateScoreBar ();
		}

		total_score_hor = 0;
		total_piece_hor = 0;
		total_sum_hor = 0;
	}

	public void ShowAdySumHor(int pLocation_x, int pLocation_y)
	{
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
				if (i <= pLocation_x) 
				{
					PieceDrag.squarePieceList [GetSquarePieceIndex (i, pLocation_y)].isScored = false;				
				}
				++total_piece_hor;
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
				if (i>= pLocation_x + 1) 
				{
					PieceDrag.squarePieceList [GetSquarePieceIndex (i, pLocation_y)].isScored = false;				
				}
				++total_piece_hor;
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

			if (total_piece_hor > 1) 
			{
				if (this.is_ady_vertical == false) 
				{
					ShowAdjacentPiecesHor ();
				}
			} 

		} 
		else if (total_sum_hor == 10) {
			
			if (this.is_ady_vertical == false) 
			{
				this.ShowAdjacentPieces();
			}
			this.SetIsScored (); 
		} 

		total_piece_hor = 0;
		total_score_hor = 0;
		total_sum_hor = 0;
	}

	public void ShowAdySumVer(int pLocation_x, int pLocation_y)
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
				total_sum_ver += GetSquarePiece (pLocation_x,i).piece.Piece_number;
				if (i <= pLocation_y) 
				{
					PieceDrag.squarePieceList [GetSquarePieceIndex (pLocation_x, i)].isScored = false;				
				}
				++total_piece_ver;
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
				if (i >= pLocation_y + 1) 
				{
					PieceDrag.squarePieceList [GetSquarePieceIndex (pLocation_x, i)].isScored = false;				
				}
				++total_piece_ver;
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

			if (total_piece_ver > 1) {
				ShowAdjacentPiecesVer ();
				this.is_sum_vertical = true;
			} 

		} 
		else if (total_sum_ver == 10) {
			total_piece_ver = 0;
			this.is_ady_vertical = true;
			this.ShowAdjacentPieces ();
			this.SetIsScored (); 
		} 

		total_piece_ver = 0;
		total_score_ver = 0;
		total_sum_ver = 0;
	}

	private void ShowAdjacentPieces()
	{
		if(PieceDrag.adjacentList.Count > 0)
		{
			for (int i = 0; i < PieceDrag.adjacentList.Count; i++)
			{
				Destroy (PieceDrag.adjacentList [i].Game_square);
			}
		}

		PieceDrag.adjacentList = new List<Square> ();

		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) 
		{
			int loc_adj_x1 = PieceDrag.squarePieceList[i].square.Location_x;
			int loc_adj_y1 = PieceDrag.squarePieceList [i].square.Location_y - 1;
			int loc_adj_x2 = PieceDrag.squarePieceList [i].square.Location_x + 1;
			int loc_adj_y2 = PieceDrag.squarePieceList [i].square.Location_y;
			int loc_adj_x3 = PieceDrag.squarePieceList [i].square.Location_x;
			int loc_adj_y3 = PieceDrag.squarePieceList [i].square.Location_y + 1;
			int loc_adj_x4 = PieceDrag.squarePieceList [i].square.Location_x - 1;
			int loc_adj_y4 = PieceDrag.squarePieceList [i].square.Location_y;

			if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))
			{   
				Square adjacent = new Square ();
				adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
				PieceDrag.adjacentList.Add (adjacent);
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
				PieceDrag.adjacentList.Add (adjacent);
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
				PieceDrag.adjacentList.Add (adjacent);
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
				PieceDrag.adjacentList.Add (adjacent);
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
			if (PieceDrag.adjacentList.Count > 0) {
				for (int i = 0; i < PieceDrag.adjacentList.Count; i++) {
					Destroy (PieceDrag.adjacentList [i].Game_square);
				}
			}		
			PieceDrag.adjacentList = new List<Square> ();
		}

		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) {
			if (PieceDrag.squarePieceList [i].isScored == false) {
				int loc_adj_x1 = PieceDrag.squarePieceList [i].square.Location_x + 1;
				int loc_adj_y1 = PieceDrag.squarePieceList [i].square.Location_y;
				int loc_adj_x2 = PieceDrag.squarePieceList [i].square.Location_x - 1;
				int loc_adj_y2 = PieceDrag.squarePieceList [i].square.Location_y;

				if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))  {   
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
					PieceDrag.adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
				if ((this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x2, loc_adj_y2) == false)) {
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false);
					PieceDrag.adjacentList.Add (adjacent);
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
		if(PieceDrag.adjacentList.Count > 0)
		{
			for (int i = 0; i < PieceDrag.adjacentList.Count; i++)
			{
				Destroy (PieceDrag.adjacentList [i].Game_square);
			}
		}

		PieceDrag.adjacentList = new List<Square> ();

		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) {
			if (PieceDrag.squarePieceList [i].isScored == false) {
				int loc_adj_x1 = PieceDrag.squarePieceList [i].square.Location_x;
				int loc_adj_y1 = PieceDrag.squarePieceList [i].square.Location_y - 1;
				int loc_adj_x2 = PieceDrag.squarePieceList [i].square.Location_x;
				int loc_adj_y2 = PieceDrag.squarePieceList [i].square.Location_y + 1;

				if ((this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false).IsOcupated == false)  && (this.CheckAdjLocation(loc_adj_x1, loc_adj_y1) == false))  {   
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x1, loc_adj_y1, false);
					PieceDrag.adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
				if ((this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false).IsOcupated == false) && (this.CheckAdjLocation(loc_adj_x2, loc_adj_y2) == false)) {
					Square adjacent = new Square ();
					adjacent = this.GetIndexBoardLocation (loc_adj_x2, loc_adj_y2, false);
					PieceDrag.adjacentList.Add (adjacent);
					this.GetSquarePerType (adjacent);
					temPost = adjacent.Game_square.transform.position;
					temPost.x = adjacent.Coordinate_x_center;
					temPost.y = adjacent.Coordinate_y_center;
					adjacent.Game_square.transform.position = temPost;
				}
			}
		}
	}
		
	private void UpdateScoreBar()
	{
		Vector2 temPost;

		GameObject bar = GameObject.Find ("ScoreBarAhead");

		temPost = bar.transform.position;
		temPost.x = -5.195f;
		size_x = 1.00490f;

		if (PieceDrag.total_score_gral >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[2].Score_level) 
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

		if(temPost.x < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[2].Loc_star)
		{
			ListPieces.particle_three.SetActive (false);
			--PieceDrag.green_time;
		}
		if(temPost.x < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[1].Loc_star)
		{
			ListPieces.particle_two.SetActive (false);
			--PieceDrag.yellow_time;
		}
			
		if(temPost.x < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Level_score_list[0].Loc_star)
		{
			ListPieces.particle_one.SetActive (false);
			--PieceDrag.red_time;
		}
			
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

	private Square GetIndexBoardLocation(int pLocation_x, int pLocation_y, bool pOcupy)
	{
		Square s = new Square();
		bool found = false;

		for (int i = 0; i< UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list.Count && found == false; i++)
		{
			if (UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_x == pLocation_x && UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].Location_y == pLocation_y)
			{
				if (pOcupy == true)
				{
					UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Board_list [0].Board_type.Square_list[i].IsOcupated = true;
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

		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) 
		{
			if(PieceDrag.squarePieceList[i].square.Location_x == pLocation_x && PieceDrag.squarePieceList[i].square.Location_y == pLocation_y)
			{
				sq = PieceDrag.squarePieceList [i];

			}
		}

		return sq;
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

	private bool CheckAdjLocation(int pLocation_x, int pLocation_y)
	{
		bool ok = false;

		if (first_piece == true) {
			Square adjacent = new Square ();
			adjacent = this.GetIndexBoardLocation (5, 5, false);
			PieceDrag.adjacentList.Add (adjacent);
		}

		for (int i = 0; i < PieceDrag.adjacentList.Count; i++)
		{
			if (PieceDrag.adjacentList[i].Location_x == pLocation_x && PieceDrag.adjacentList[i].Location_y == pLocation_y) 
			{
				ok = true;
			}
		}
		return ok;
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

	public void SetFirstPiece()
	{
		if (PieceDrag.squarePieceList.Count == 0) 
		{
			first_piece = true;
		}

	}

	private void SetIsScored()
	{
		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++)
		{
			PieceDrag.squarePieceList[i].isScored = true;
		}
	}

	public int GetSquarePieceIndex(int pLocation_x, int pLocation_y)
	{
		int index = 0;

		for (int i = 0; i < PieceDrag.squarePieceList.Count; i++) 
		{
			if(PieceDrag.squarePieceList[i].square.Location_x == pLocation_x && PieceDrag.squarePieceList[i].square.Location_y == pLocation_y)
			{
				index = i;
			}
		}

		return index;
	}
		
	public void DeleteObjectives ()
	{
		for (int i = 0; i < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list.Count; i++) {
			int idType = UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Type_obj.Id_type_objective;

			if (idType == 1) {
				if (PieceDrag.total_score_gral < UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value && PieceDrag.firstOk == true) {
					ListPieces.okObjective1.SetActive (false);
					PieceDrag.firstOk = false;
				}
			} else if (idType == 2) {
				if (ListPieces.pieceList.Count >= UIManagerMenu.g.List_maps [UIManagerMenu.IdMap].ListLevel [LoadMap.levelGo - 1].Objective_list [i].Top_value && PieceDrag.secondOk == true) {
					ListPieces.okObjective2.SetActive (false);
					PieceDrag.secondOk = false;
				}
			}

			//Include the rest of the idTypes later
		}

	}
}