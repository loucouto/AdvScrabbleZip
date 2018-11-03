using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonusType{
	private int id_type_bonus;
	private string name;
	private string description;

	public int Id_type_bonus {
		get {
			return this.id_type_bonus;
		}
		set {
			id_type_bonus = value;
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
	public BonusType(){
	}

	public BonusType (int id_type_bonus, string name, string description)
	{
		this.id_type_bonus = id_type_bonus;
		this.name = name;
		this.description = description;
	}
	
}
