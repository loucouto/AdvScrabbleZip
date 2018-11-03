using UnityEngine;
using LitJson;

public class RemarkedNine : MonoBehaviour {

	private string piece_path;
	private string piece_jsonString;
	private JsonData piece_itemData;
	private static int total_score_hor = 0;
	private static int total_score_ver = 0;
	private static int total_piece_hor = 0;
	private static int total_piece_ver = 0;
	private static int total_sum_ver = 0;
	private static int total_sum_hor = 0;
	private static int total_score = 300;
	private float size_x = 1.00490f;

	void OnMouseDown()
	{
		this.ChangeSpritePiece ();
		this.ChangeBackground ();
		this.DestroyRemarkedSquared ();
		this.Enable ();
		ListPieces.message_string = "";
		BonusSquare.isBonusMess = false;
		ListPieces.bonusNine--;
		UIManagerMenu.g.P.ListBonus [0].Count_bonus--;
		new Persistence ().UpdateBonus (UIManagerMenu.g.Id_game, UIManagerMenu.g.P.Id_player, UIManagerMenu.g.P.ListBonus [0]);
		this.DisactiveCancelButton ();
	}

	private void ChangeSpritePiece()
	{
		bool stop = false;
		for (int i = 0; i < PieceDrag.squarePieceList.Count && stop == false; i++) {
			if (gameObject.transform.position.x == PieceDrag.squarePieceList [i].square.Coordinate_x_center && gameObject.transform.position.y == PieceDrag.squarePieceList [i].square.Coordinate_y_center) 
			{
				
                for (int j = 0; j < 90 && stop == false; j++)
                {
                    if ((PieceDrag.squarePieceList[i].piece.Piece_number == UIManagerMenu.g.List_pieces[j].Piece_number) && (UIManagerMenu.g.List_pieces[j].Piece_score == 9))
					{
						this.SumHor (PieceDrag.squarePieceList [i].square.Location_x, PieceDrag.squarePieceList [i].square.Location_y, true);
						this.SumVer (PieceDrag.squarePieceList [i].square.Location_x, PieceDrag.squarePieceList [i].square.Location_y, true);

						SpriteRenderer spriteRen = new SpriteRenderer ();
						spriteRen = PieceDrag.squarePieceList [i].piece.Game_piece.GetComponent<SpriteRenderer> ();
						spriteRen.sprite = Resources.Load<Sprite> ("PieceDrawing/" + UIManagerMenu.g.List_pieces[j].File) as Sprite;
						//old_score = PieceDrag.squarePieceList[i].piece.piece_score;

						PieceDrag.squarePieceList [i].piece.Id_piece = UIManagerMenu.g.List_pieces[j].Id_piece;
						PieceDrag.squarePieceList [i].piece.Piece_number = UIManagerMenu.g.List_pieces[j].Piece_number;
						PieceDrag.squarePieceList [i].piece.Piece_score = UIManagerMenu.g.List_pieces[j].Piece_score;
						PieceDrag.squarePieceList [i].piece.File = UIManagerMenu.g.List_pieces[j].File;

						this.SumHor (PieceDrag.squarePieceList [i].square.Location_x, PieceDrag.squarePieceList [i].square.Location_y, false);
						this.SumVer (PieceDrag.squarePieceList [i].square.Location_x, PieceDrag.squarePieceList [i].square.Location_y, false);

						stop = true;
					}
				}
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
		for (int i = 0; i < BonusNinePoints.remarkedList.Count; i++)
		{
			Destroy (BonusNinePoints.remarkedList[i].Game_square);
		}
		BonusNinePoints.remarkedList.Clear();
	}

	private void Enable()
	{
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

   private void  UpdateScoreBar()
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

	private void DisactiveCancelButton()
	{
		ListPieces.buttonCancel.SetActive (false);
	}
}
