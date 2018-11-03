using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model{
	private int id_model;
	private string description;

    public int Id_model {
		get {
			return this.id_model;
		}
		set {
			id_model = value;
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

	public Model ()
	{
	}

	public Model (int id_model, string description)
	{
		this.id_model = id_model;
		this.description = description;
	}

}
