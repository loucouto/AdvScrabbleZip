using UnityEngine;

public class PieceManagerLeft : MonoBehaviour {

	//private int count = 0;
	private Vector2 temPost;
	private SpriteRenderer first_sr;
	private AudioSource a;

	void OnMouseDown()
	{
		Sound.GetSound ("ButtonLeft");

		if (PieceManagerRight.count > 0) {

			if (ListPieces.pieceList.Count > 5 || PieceManagerRight.count > 0) {
				PieceManagerRight.count--;
				ListPieces.pieceList [PieceManagerRight.count].Game_piece.SetActive (true);
				if ((PieceManagerRight.count + 5) <= (ListPieces.pieceList.Count - 1)) {
					ListPieces.pieceList [PieceManagerRight.count + 5].Game_piece.SetActive (false);
				}
			}
			for (int i = 0; i < ListPieces.pieceList.Count; i++) 
			{
				temPost = ListPieces.pieceList [i].Game_piece.transform.position;
				temPost.x = temPost.x + 0.6f;
				temPost.y = -1.97f;
				ListPieces.pieceList [i].Game_piece.transform.position = temPost;
			}

		}

	}
}
