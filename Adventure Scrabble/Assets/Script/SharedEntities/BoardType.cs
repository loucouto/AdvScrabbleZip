using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardType{
	private int id_board_type;
	private string description;
	private List<Square> square_list;

	public int Id_board_type {
		get {
			return this.id_board_type;
		}
		set {
			id_board_type = value;
		}
	}

	public string Description {
		get {
			return this.description;
		}
		set {
			description = value;
		}
	}

	public List<Square> Square_list {
		get {
			return this.square_list;
		}
		set {
			square_list = value;
		}
	}

	public BoardType ()
	{
	}

	public BoardType (int id_board_type, string description, List<Square> square_list)
	{
		this.id_board_type = id_board_type;
		this.description = description;
		this.square_list = square_list;
	}

}
