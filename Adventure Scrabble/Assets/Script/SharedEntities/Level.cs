using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Level {
	private int id_level;
	private string name;
	private string file;
	private string file_osc;
	private float x;
	private float y;
	private float x_exp;
	private float y_exp;
	private float x_frame;
	private float y_frame;
	private List<LevelScore> level_score_list;
	private List<Objective> objective_list;
	private List<Conditionating> conditionating_list;
	private List<Board> board_list = new List<Board>();

	public int Id_level {
		get {
			return this.id_level;
		}
		set {
			id_level = value;
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

	public string File {
		get {
			return this.file;
		}
		set {
			file = value;
		}
	}

	public float X {
		get {
			return this.x;
		}
		set {
			x = value;
		}
	}

	public float Y {
		get {
			return this.y;
		}
		set {
			y = value;
		}
	}

	public float X_exp {
		get {
			return this.x_exp;
		}
		set {
			x_exp = value;
		}
	}

	public float Y_exp {
		get {
			return this.y_exp;
		}
		set {
			y_exp = value;
		}
	}

	public float X_frame {
		get {
			return this.x_frame;
		}
		set {
			x_frame = value;
		}
	}

	public float Y_frame {
		get {
			return this.y_frame;
		}
		set {
			y_frame = value;
		}
	}
	public string File_osc {
		get {
			return this.file_osc;
		}
		set {
			file_osc = value;
		}
	}

	public List<LevelScore> Level_score_list {
		get {
			return this.level_score_list;
		}
		set {
			level_score_list = value;
		}
	}

	public List<Objective> Objective_list {
		get {
			return this.objective_list;
		}
		set {
			objective_list = value;
		}
	}

	public List<Conditionating> Conditionating_list {
		get {
			return this.conditionating_list;
		}
		set {
			conditionating_list = value;
		}
	}

	public List<Board> Board_list {
		get {
			return this.board_list;
		}
		set {
			board_list = value;
		}
	}

	public Level(){
	}

	public Level (int id_level, string name, string file, string file_osc, float x, float y, float x_exp, float y_exp, float x_frame, float y_frame, List<LevelScore> level_score_list, List<Objective> objective_list, List<Conditionating> conditionating_list, List<Board> board_list)
	{
		this.id_level = id_level;
		this.name = name;
		this.file = file;
		this.file_osc = file_osc;
		this.x = x;
		this.y = y;
		this.x_exp = x_exp;
		this.y_exp = y_exp;
		this.x_frame = x_frame;
		this.y_frame = y_frame;
		this.level_score_list = level_score_list;
		this.objective_list = objective_list;
		this.conditionating_list = conditionating_list;
		this.board_list = board_list;
	}
	
	
}
