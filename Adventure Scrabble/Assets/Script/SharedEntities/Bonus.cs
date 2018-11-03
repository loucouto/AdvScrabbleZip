using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bonus{
	private int id_bonus;
	private string file;
	private int count_bonus;
	private BonusType bonusType;

	public int Id_bonus {
		get {
			return this.id_bonus;
		}
		set {
			id_bonus = value;
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

	public int Count_bonus {
		get {
			return this.count_bonus;
		}
		set {
			count_bonus = value;
		}
	}

	public BonusType BonusType {
		get {
			return this.bonusType;
		}
		set {
			bonusType = value;
		}
	}
	public Bonus(){
	}

	public Bonus (int id_bonus, string file, int count_bonus, BonusType bonusType)
	{
		this.id_bonus = id_bonus;
		this.file = file;
		this.count_bonus = count_bonus;
		this.bonusType = bonusType;
	}
	
}
