using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement{

	private int id_achievement;
	private string image_file;
    private string title;
	private string description;
	private int count;
	private int max_count;
	private double percentaje;
	private int prize;
	private double width;
	private double height;
	private double loc_x;
	private bool is_earned;


	public int Id_achievement {
		get {
			return this.id_achievement;
		}
		set {
			id_achievement = value;
		}
	}

	public string Image_file {
		get {
			return this.image_file;
		}
		set {
			image_file = value;
		}
	}

	public string Title {
		get {
			return this.title;
		}
		set {
			title = value;
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

	public int Count {
		get {
			return this.count;
		}
		set {
			count = value;
		}
	}

	public int Max_count {
		get {
			return this.max_count;
		}
		set {
			max_count = value;
		}
	}

	public double Percentaje {
		get {
			return this.percentaje;
		}
		set {
			percentaje = value;
		}
	}

	public int Prize {
		get {
			return this.prize;
		}
		set {
			prize = value;
		}
	}

	public double Width {
		get {
			return this.width;
		}
		set {
			width = value;
		}
	}

	public double Height {
		get {
			return this.height;
		}
		set {
			height = value;
		}
	}

	public double Loc_x {
		get {
			return this.loc_x;
		}
		set {
			loc_x = value;
		}
	}

	public bool Is_earned {
		get {
			return this.is_earned;
		}
		set {
			is_earned = value;
		}
	}

	public Achievement(){
	}

	public Achievement (int id_achievement, string image_file, string title, string description, int count, int max_count, double percentaje, int prize, double width, double height, double loc_x, bool is_earned)
	{
		this.id_achievement = id_achievement;
		this.image_file = image_file;
		this.title = title;
		this.description = description;
		this.count = count;
		this.max_count = max_count;
		this.percentaje = percentaje;
		this.prize = prize;
		this.width = width;
		this.height = height;
		this.loc_x = loc_x;
		this.is_earned = is_earned;

	}
	
}
