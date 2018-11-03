using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelScore{
	private int id_level_score;
	private int score_level;
	private float loc_star;
	private float bar_value;
	private Star star;

	public int Id_level_score {
		get {
			return this.id_level_score;
		}
		set {
			id_level_score = value;
		}
	}

	public int Score_level {
		get {
			return this.score_level;
		}
		set {
			score_level = value;
		}
	}

	public Star Star {
		get {
			return this.star;
		}
		set {
			star = value;
		}
	}

	public float Loc_star {
		get {
			return this.loc_star;
		}
		set {
			loc_star = value;
		}
	}

	public float Bar_value {
		get {
			return this.bar_value;
		}
		set {
			bar_value = value;
		}
	}

	public LevelScore(){
	}
		
	public LevelScore (int id_level_score, int score_level, Star star, float loc_star, float bar_value)
	{
		this.Id_level_score = id_level_score;
		this.Score_level = score_level;
		this.Star = star;
		this.Loc_star = loc_star;
		this.Bar_value = bar_value;
	}
	
}
