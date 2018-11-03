using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageType{
	public int id_player;
	public int id_type_message;
	public int id_message;
	public string name;
	public string description;
	public List<Message> messageList = new List<Message> (); 
}
