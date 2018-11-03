using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingot
{
	private int id_ingot;
	private int coin_count;

	public int Id_ingot {
		get {
			return this.id_ingot;
		}
		set {
			id_ingot = value;
		}
	}

	public int Coin_count {
		get {
			return this.coin_count;
		}
		set {
			coin_count = value;
		}
	}

	public Ingot(){
	}

	public Ingot (int id_ingot, int coin_count)
	{
		this.id_ingot = id_ingot;
		this.coin_count = coin_count;
	}
	
}
