using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective{
	private int id_objective;
	private int top_value;
	private ObjectiveType type_obj;

	public int Id_objective {
		get {
			return this.id_objective;
		}
		set {
			id_objective = value;
		}
	}

	public int Top_value {
		get {
			return this.top_value;
		}
		set {
			top_value = value;
		}
	}

	public ObjectiveType Type_obj {
		get {
			return this.type_obj;
		}
		set {
			type_obj = value;
		}
	}

	public Objective(){
	}

	public Objective (int id_objective, int top_value, ObjectiveType type_obj)
	{
		this.id_objective = id_objective;
		this.top_value = top_value;
		this.type_obj = type_obj;
	}
	
}
