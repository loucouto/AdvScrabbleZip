using UnityEngine;

public class PieceManagerRight : MonoBehaviour {

	public static int count = 0;
	public static int topMax = 10;
	private Vector2 temPost;
	private SpriteRenderer last_sr;
	private AudioSource a;

	void OnMouseDown()
	{
		Sound.GetSound ("ButtonRight");
		if (count < topMax) {

			if (ListPieces.pieceList.Count > 5 || count > 0) {
				count++;

				ListPieces.pieceList [count - 1].Game_piece.SetActive (false);
				ListPieces.pieceList [count + 4].Game_piece.SetActive (true);
			}

			for (int i = 0; i < ListPieces.pieceList.Count; i++) 
			{
				temPost = ListPieces.pieceList [i].Game_piece.transform.position;
				temPost.x = temPost.x - 0.6f;
				temPost.y = -1.97f;
				ListPieces.pieceList[i].Game_piece.transform.position = temPost;

			}
				
		}
			
	}
}
