using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionatingType{

	private int id_type_conditionating;
	private string description;

	public int Id_type_conditionating {
		get {
			return this.id_type_conditionating;
		}
		set {
			id_type_conditionating = value;
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

	public ConditionatingType ()
	{
	}

	public ConditionatingType (int id_type_conditionating, string description)
	{
		this.id_type_conditionating = id_type_conditionating;
		this.description = description;
	}
		
}
