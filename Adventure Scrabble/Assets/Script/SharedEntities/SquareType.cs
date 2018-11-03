using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SquareType
{
	public int id_type_square;
	public string name;
	public string description;

	public int Id_type_square {
		get {
			return this.id_type_square;
		}
		set {
			id_type_square = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
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
		
	public SquareType ()
	{
	}

	public SquareType (int id_type_square, string name, string description)
	{
		this.id_type_square = id_type_square;
		this.name = name;
		this.description = description;
	}

}
