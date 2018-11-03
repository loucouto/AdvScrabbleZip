using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMove : MonoBehaviour {

	public Texture2D moveTexture;
	public Texture2D mouseTexture;
	public Vector2 temPost = Vector2.zero;
	public static Vector2 nowPost = new Vector2(0f,272f);
	public CursorMode curMode = CursorMode.Auto;

	void OnMouseDown()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = (AudioClip)Resources.Load ("Sound/click1");

		if (UICommon.soundActive == true) 
		{
			audio.Play ();
		} 
		else 
		{
			if (audio.isPlaying == true) 
			{
				audio.Stop();
			}
		}
	}

	void OnMouseDrag()
	{

		temPost = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (temPost);

		GameObject map = GameObject.Find ("BackgroundImage");
		RectTransform rectPost = map.GetComponent<RectTransform> ();

		nowPost.y = objPosition.y + nowPost.y;
		if (nowPost.y > -280f && nowPost.y < 271f) {
			rectPost.anchoredPosition = nowPost;
		} 
		else 
		{
			if (nowPost.y <= -280f) 
			{
				nowPost.y = -279f;
			} 
			else 
			{
				nowPost.y = 270f;
			}
		
		}
		Cursor.SetCursor(moveTexture, objPosition, curMode);
	}

	void OnMouseUp()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = (AudioClip)Resources.Load ("Sound/mouserelease1");

		if (UICommon.soundActive == true) 
		{
			audio.Play ();
		} 
		else 
		{
			if (audio.isPlaying == true) 
			{
				audio.Stop();
			}
		}
	
		temPost = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (temPost);

		GameObject map = GameObject.Find ("BackgroundImage");

		RectTransform rectPost = map.GetComponent<RectTransform> ();

		nowPost.y = objPosition.y + nowPost.y;
		if (nowPost.y > -280f && nowPost.y < 271f) {
			rectPost.anchoredPosition = nowPost;
		} 
		else 
		{
			if (nowPost.y <= -280f) 
			{
				nowPost.y = -279f;
			} 
			else 
			{
				nowPost.y = 270f;
			}

		}
		Cursor.SetCursor(mouseTexture, objPosition, CursorMode.Auto);
	}
		
}
