using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Game {
	private int id_game;
	private string name;
	private string route_file;
	private DateTime date_done;
	private Player p;
	private List<Map> list_maps;
	private List<Piece> list_pieces;

	public Game(){
	}

	public Game (int id_game, string name, string route_file, DateTime date_done, Player p, List<Map> list_maps, List<Piece> list_pieces)
	{
		this.id_game = id_game;
		this.name = name;
		this.route_file = route_file;
		this.date_done = date_done;
		this.p = p;
		this.list_maps = list_maps;
		this.list_pieces = list_pieces;
	}


	public int Id_game {
		get {
			return this.id_game;
		}
		set {
			id_game = value;
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

	public string Route_file {
		get {
			return this.route_file;
		}
		set {
			route_file = value;
		}
	}

	public DateTime Date_done {
		get {
			return this.date_done;
		}
		set {
			date_done = value;
		}
	}

	public Player P {
		get {
			return this.p;
		}
		set {
			p = value;
		}
	}

	public List<Map> List_maps {
		get {
			return this.list_maps;
		}
		set {
			list_maps = value;
		}
	}

	public List<Piece> List_pieces {
		get {
			return this.list_pieces;
		}
		set {
			list_pieces = value;
		}
	}
}
