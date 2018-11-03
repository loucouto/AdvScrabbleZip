using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePiece {

	public Square square;
	public Piece piece;
	public bool isScored;

	public Square Square {
		get {
			return this.square;
		}
		set {
			square = value;
		}
	}

	public Piece Piece {
		get {
			return this.piece;
		}
		set {
			piece = value;
		}
	}

	public bool IsScored {
		get {
			return this.isScored;
		}
		set {
			isScored = value;
		}
	}

	public SquarePiece ()
	{
	}

	public SquarePiece (Square square, Piece piece, bool isScored)
	{
		this.square = square;
		this.piece = piece;
		this.isScored = isScored;
	}
	
}
