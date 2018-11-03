using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelState{
	private int id_state;
	private int num_state;
	private int times_lost;
	private Level level;

	public int Id_state {
		get {
			return this.id_state;
		}
		set {
			id_state = value;
		}
	}

	public int Num_state {
		get {
			return this.num_state;
		}
		set {
			num_state = value;
		}
	}

	public int Times_lost {
		get {
			return this.times_lost;
		}
		set {
			times_lost = value;
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

	public LevelState ()
	{
	}

	public LevelState (int id_state, int num_state, int times_lost, Level level)
	{
		this.id_state = id_state;
		this.num_state = num_state;
		this.times_lost = times_lost;
		this.level = level;
	}

}
