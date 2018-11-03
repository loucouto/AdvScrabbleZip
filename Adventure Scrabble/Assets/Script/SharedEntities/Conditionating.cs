using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditionating{
	private int id_conditionating;
	private int top_value;
	private bool is_infinitive;
	private ConditionatingType cond_type;

	public int Id_conditionating {
		get {
			return this.id_conditionating;
		}
		set {
			id_conditionating = value;
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

	public bool Is_infinitive {
		get {
			return this.is_infinitive;
		}
		set {
			is_infinitive = value;
		}
	}

	public ConditionatingType Cond_type {
		get {
			return this.cond_type;
		}
		set {
			cond_type = value;
		}
	}

	public Conditionating ()
	{
	}

	public Conditionating (int id_conditionating, int top_value, bool is_infinitive, ConditionatingType cond_type)
	{
		this.id_conditionating = id_conditionating;
		this.top_value = top_value;
		this.is_infinitive = is_infinitive;
		this.cond_type = cond_type;
	}

}
