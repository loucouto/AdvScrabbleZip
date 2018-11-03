using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Heart
{
	private int id_heart;
	private int count_lifes;
	private int minutes;
	private int seconds;
	private int minutes_per_life;
	private bool isInfinite;
	private DateTime time_infinite;
	private List<NextLife> list_next_life = new List<NextLife>();

	public int Id_heart {
		get {
			return this.id_heart;
		}
		set {
			id_heart = value;
		}
	}

	public int Count_lifes {
		get {
			return this.count_lifes;
		}
		set {
			count_lifes = value;
		}
	}

	public int Minutes {
		get {
			return this.minutes;
		}
		set {
			minutes = value;
		}
	}

	public int Seconds {
		get {
			return this.seconds;
		}
		set {
			seconds = value;
		}
	}

	public int Minutes_per_life {
		get {
			return this.minutes_per_life;
		}
		set {
			minutes_per_life = value;
		}
	}

	public bool IsInfinite {
		get {
			return this.isInfinite;
		}
		set {
			isInfinite = value;
		}
	}

	public DateTime Time_infinite {
		get {
			return this.time_infinite;
		}
		set {
			time_infinite = value;
		}
	}

	public List<NextLife> List_next_life {
		get {
			return this.list_next_life;
		}
		set {
			list_next_life = value;
		}
	}

	public Heart ()
	{
	}
	
	public Heart (int id_heart, int count_lifes, int minutes, int seconds, int minutes_per_life, bool isInfinite, DateTime time_infinite, List<NextLife> list_next_life)
	{
		this.id_heart = id_heart;
		this.count_lifes = count_lifes;
		this.minutes = minutes;
		this.seconds = seconds;
		this.minutes_per_life = minutes_per_life;
		this.isInfinite = isInfinite;
		this.time_infinite = time_infinite;
		this.list_next_life = list_next_life;
	}
	

}
