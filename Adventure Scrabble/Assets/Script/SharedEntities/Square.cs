using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Square {
	private int id_square;
	private float coordinate_x_border;
	private float coordinate_y_border;
	private float coordinate_x_center;
	private float coordinate_y_center;
	private int location_x;
	private int location_y;
	private bool isOcupated;
	private SquareType square_type;
	private GameObject game_square;

	public int Id_square {
		get {
			return this.id_square;
		}
		set {
			id_square = value;
		}
	}

	public float Coordinate_x_border {
		get {
			return this.coordinate_x_border;
		}
		set {
			coordinate_x_border = value;
		}
	}

	public float Coordinate_y_border {
		get {
			return this.coordinate_y_border;
		}
		set {
			coordinate_y_border = value;
		}
	}

	public float Coordinate_x_center {
		get {
			return this.coordinate_x_center;
		}
		set {
			coordinate_x_center = value;
		}
	}

	public float Coordinate_y_center {
		get {
			return this.coordinate_y_center;
		}
		set {
			coordinate_y_center = value;
		}
	}

	public int Location_x {
		get {
			return this.location_x;
		}
		set {
			location_x = value;
		}
	}

	public int Location_y {
		get {
			return this.location_y;
		}
		set {
			location_y = value;
		}
	}

	public bool IsOcupated {
		get {
			return this.isOcupated;
		}
		set {
			isOcupated = value;
		}
	}

	public SquareType Square_type {
		get {
			return this.square_type;
		}
		set {
			square_type = value;
		}
	}

	public GameObject Game_square {
		get {
			return this.game_square;
		}
		set {
			game_square = value;
		}
	}

	public Square ()
	{
	}

	public Square (int id_square, float coordinate_x_border, float coordinate_y_border, float coordinate_x_center, float coordinate_y_center, int location_x, int location_y, bool isOcupated, SquareType square_type, GameObject game_square)
	{
		this.id_square = id_square;
		this.coordinate_x_border = coordinate_x_border;
		this.coordinate_y_border = coordinate_y_border;
		this.coordinate_x_center = coordinate_x_center;
		this.coordinate_y_center = coordinate_y_center;
		this.location_x = location_x;
		this.location_y = location_y;
		this.isOcupated = isOcupated;
		this.square_type = square_type;
		this.game_square = game_square;
	}
	
}
