using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Message
{
	private int id_message;
	private string title;
	private string description;
	private string file;
	private DateTime doneDate;
	private DateTime untilDate;

	public int Id_message {
		get {
			return this.id_message;
		}
		set {
			id_message = value;
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

	public string File {
		get {
			return this.file;
		}
		set {
			file = value;
		}
	}

	public DateTime DoneDate {
		get {
			return this.doneDate;
		}
		set {
			doneDate = value;
		}
	}

	public DateTime UntilDate {
		get {
			return this.untilDate;
		}
		set {
			untilDate = value;
		}
	}

	public Message(){
	}
	public Message (int id_message, string title, string description, string file, DateTime doneDate, DateTime untilDate)
	{
		this.id_message = id_message;
		this.title = title;
		this.description = description;
		this.file = file;
		this.doneDate = doneDate;
		this.untilDate = untilDate;
	}
		
}
