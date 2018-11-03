using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class RegisterLevel {
	private int id_register_level;
	private int score;
	private int count_stars;
	private Level level;

	public int Id_register_level {
		get {
			return this.id_register_level;
		}
		set {
			id_register_level = value;
		}
	}

	public int Score {
		get {
			return this.score;
		}
		set {
			score = value;
		}
	}

	public int Count_stars {
		get {
			return this.count_stars;
		}
		set {
			count_stars = value;
		}
	}

	public Level Level {
		get {
			return this.level;
		}
		set {
			level = value;
		}
	}

	public RegisterLevel ()
	{
	}

	public RegisterLevel (int id_register_level, int score, int count_stars, Level level)
	{
		this.id_register_level = id_register_level;
		this.score = score;
		this.count_stars = count_stars;
		this.level = level;
	}

}
