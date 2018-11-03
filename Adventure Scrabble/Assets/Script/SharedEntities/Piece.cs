using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Piece
{
	private int id_piece;
	private int piece_number;
	private int piece_score;
	private string file;
	private Model model;
	private GameObject game_piece;

	public int Id_piece {
		get {
			return this.id_piece;
		}
		set {
			id_piece = value;
		}
	}

	public int Piece_number {
		get {
			return this.piece_number;
		}
		set {
			piece_number = value;
		}
	}

	public int Piece_score {
		get {
			return this.piece_score;
		}
		set {
			piece_score = value;
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

	public Model Model {
		get {
			return this.model;
		}
		set {
			model = value;
		}
	}

	public GameObject Game_piece {
		get {
			return this.game_piece;
		}
		set {
			game_piece = value;
		}
	}

	public Piece ()
	{
	}

	public Piece (int id_piece, int piece_number, int piece_score, string file, Model model, GameObject game_piece)
	{
		this.id_piece = id_piece;
		this.piece_number = piece_number;
		this.piece_score = piece_score;
		this.file = file;
		this.model = model;
		this.game_piece = game_piece;
	}

}
