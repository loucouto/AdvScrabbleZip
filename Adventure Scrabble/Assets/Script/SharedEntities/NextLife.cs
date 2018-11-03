using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NextLife{
	private int id_next_life;
	private DateTime date_next_life;

	public int Id_next_life {
		get {
			return this.id_next_life;
		}
		set {
			id_next_life = value;
		}
	}

	public DateTime Date_next_life {
		get {
			return this.date_next_life;
		}
		set {
			date_next_life = value;
		}
	}

	public NextLife ()
	{
	}

	public NextLife (int id_next_life, DateTime date_next_life)
	{
		this.id_next_life = id_next_life;
		this.date_next_life = date_next_life;
	}
	
}
