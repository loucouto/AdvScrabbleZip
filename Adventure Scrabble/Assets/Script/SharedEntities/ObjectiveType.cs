using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveType{
	private int id_type_objective;
	private string description;

	public int Id_type_objective {
		get {
			return this.id_type_objective;
		}
		set {
			id_type_objective = value;
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

	public ObjectiveType(){
	}

	public ObjectiveType (int id_type_objective, string description)
	{
		this.id_type_objective = id_type_objective;
		this.description = description;
	}
	
}
