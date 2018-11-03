using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class RegisterMap {
	private int id_register_map;
	private int score_map;
	private Map map;
	private List<RegisterLevel> listRegLevel = new List<RegisterLevel> ();

	public int Id_register_map {
		get {
			return this.id_register_map;
		}
		set {
			id_register_map = value;
		}
	}

	public int Score_map {
		get {
			return this.score_map;
		}
		set {
			score_map = value;
		}
	}

	public Map Map {
		get {
			return this.map;
		}
		set {
			map = value;
		}
	}

	public List<RegisterLevel> ListRegLevel {
		get {
			return this.listRegLevel;
		}
		set {
			listRegLevel = value;
		}
	}

	public RegisterMap ()
	{
	}

	public RegisterMap (int id_register_map, int score_map, Map map, List<RegisterLevel> listRegLevel)
	{
		this.id_register_map = id_register_map;
		this.score_map = score_map;
		this.map = map;
		this.listRegLevel = listRegLevel;
	}
	
}
