using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Player {
	private int id_player;
	private string name;
	private Sprite picture;
	private Heart heart;
	private Ingot ingot;
	private List<MessageGroup> list_message_group = new List<MessageGroup>();
	private List<Bonus> listBonus = new List<Bonus>();
	private List<RegisterMap> listRegMap = new List<RegisterMap>();
	private List<LevelState> listStateLevel = new List<LevelState> ();
	private List<Achievement> listAchievements = new List<Achievement> ();

	public int Id_player {
		get {
			return this.id_player;
		}
		set {
			id_player = value;
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

	public Sprite Picture {
		get {
			return this.picture;
		}
		set {
			picture = value;
		}
	}

	public Heart Heart {
		get {
			return this.heart;
		}
		set {
			heart = value;
		}
	}

	public Ingot Ingot {
		get {
			return this.ingot;
		}
		set {
			ingot = value;
		}
	}

	public List<MessageGroup> List_message_group {
		get {
			return this.list_message_group;
		}
		set {
			list_message_group = value;
		}
	}

	public List<Bonus> ListBonus {
		get {
			return this.listBonus;
		}
		set {
			listBonus = value;
		}
	}

	public List<RegisterMap> ListRegMap {
		get {
			return this.listRegMap;
		}
		set {
			listRegMap = value;
		}
	}

	public List<LevelState> ListStateLevel {
		get {
			return this.listStateLevel;
		}
		set {
			listStateLevel = value;
		}
	}

	public List<Achievement> ListAchievements {
		get {
			return this.listAchievements;
		}
		set {
			listAchievements = value;
		}
	}

	public Player ()
	{
	}
	public Player (int id_player, string name, Sprite picture, Heart heart, Ingot ingot, List<MessageGroup> list_message_group, List<Bonus> listBonus, List<RegisterMap> listRegMap, List<LevelState> listStateLevel, List<Achievement> listAchievements)
	{
		this.id_player = id_player;
		this.name = name;
		this.picture = picture;
		this.heart = heart;
		this.ingot = ingot;
		this.list_message_group = list_message_group;
		this.listBonus = listBonus;
		this.listRegMap = listRegMap;
		this.listStateLevel = listStateLevel;
		this.listAchievements = listAchievements;
	}
	
	
}
