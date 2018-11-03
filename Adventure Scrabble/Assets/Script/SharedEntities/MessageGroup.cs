using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageGroup{
	private int id_message_group;
	private string name;
	private List<Message> messageList = new List<Message> (); 

	public int Id_message_group {
		get {
			return this.id_message_group;
		}
		set {
			id_message_group = value;
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

	public List<Message> MessageList {
		get {
			return this.messageList;
		}
		set {
			messageList = value;
		}
	}

	public MessageGroup ()
	{
	}

	public MessageGroup (int id_message_group, string name, List<Message> messageList)
	{
		this.id_message_group = id_message_group;
		this.name = name;
		this.messageList = messageList;
	}

}
