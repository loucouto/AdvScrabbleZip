using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board{
	private int id_board;
	private string file;
	private BoardType board_type;

	public int Id_board {
		get {
			return this.id_board;
		}
		set {
			id_board = value;
		}
	}

	public string File {
		get {
			return this.file;
		}
		set {
			file = value;
		}
	}

	public BoardType Board_type {
		get {
			return this.board_type;
		}
		set {
			board_type = value;
		}
	}

	public Board ()
	{
	}

	public Board (int id_board, string file, BoardType board_type)
	{
		this.id_board = id_board;
		this.file = file;
		this.board_type = board_type;
	}
	
}
