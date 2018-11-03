using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Star {
	private int id_star;
	private string name;
	private string file;
	private int grade;

	public int Id_star {
		get {
			return this.id_star;
		}
		set {
			id_star = value;
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

	public int Grade {
		get {
			return this.grade;
		}
		set {
			grade = value;
		}
	}

	public Star ()
	{
	}

	public Star (int id_star, string name, string file, int grade)
	{
		this.id_star = id_star;
		this.name = name;
		this.file = file;
		this.grade = grade;
		this.Id_star = id_star;
		this.Name = name;
		this.File = file;
		this.Grade = grade;
	}
	
}
