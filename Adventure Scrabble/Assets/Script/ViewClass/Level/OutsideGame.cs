using UnityEngine;

public class OutsideGame : MonoBehaviour {

	void OnMouseDown()
	{
		Sound.GetSound ("Outside");
		GameObject question = GameObject.Find ("QuestionImage");
		Animator anim = question.GetComponent<Animator> ();
		anim.Play ("ShowQuestion");
	}
}
