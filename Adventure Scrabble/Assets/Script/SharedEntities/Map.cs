using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Map {
	private int id_map;
	private string name;
	private string description;
	private string file;
	private List<Level> listLevel = new List<Level> ();

	public int Id_map {
		get {
			return this.id_map;
		}
		set {
			id_map = value;
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

	public string File {
		get {
			return this.file;
		}
		set {
			file = value;
		}
	}

	public List<Level> ListLevel {
		get {
			return this.listLevel;
		}
		set {
			listLevel = value;
		}
	}

	public Map(){
	}

	public Map (int id_map, string name, string description, string file, List<Level> listLevel)
	{
		this.id_map = id_map;
		this.name = name;
		this.description = description;
		this.file = file;
		this.listLevel = listLevel;
	}
	
}
