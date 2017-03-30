using UnityEngine;
using System.Collections;

public class ChunkBehaviour : MonoBehaviour {

	public int chunksize = 60;
	public int chunkdepth = 100;
	public GameObject grass;
	public GameObject earth;
	public GameObject lastTile;
	public GameObject firstTile;
	public GameObject chunkManager;
	public int smoothing = 1;


	// Use this for initialization
	void Awake () {
		chunkManager = GameObject.Find ("ChunkManager");
	}
	
	// Update is called once per frame



	GameObject GenerateTile(int x, int y, GameObject tile){
		GameObject clone = (GameObject)Instantiate (tile, transform.position, transform.rotation);
		clone.transform.parent = transform;
		clone.transform.localPosition = new Vector3 (x, y, 0);
		return clone;
	}

	public void NextChunk(GameObject startTile, int smoothness){
		int y = (int)startTile.transform.position.y;
		smoothing = smoothness;
		this.GenerateChunk (y);
	}

	void GenerateChunk(int yStart){

		int yDiff = Random.Range (-1, 2);

		//Generate first column
		int x = -1 * chunksize/2;
		int y = (yStart + yDiff);
		firstTile = GenerateTile (x, y, grass);
		for (int i = y-1; i >= -1 *chunkdepth; i--)  {
			GenerateTile (x, i, earth);
		}

		//Generate all the middle columns
		while (x < (chunksize / 2)-2) {
			yDiff = (yDiff + Random.Range (-1, 2))%smoothing;

			x += 1;
			y = y + yDiff;
			if (y <= -1 * chunksize / 2)
				y = -1 * chunksize / 2;
			if (y >= chunksize / 2)
				y = chunksize / 2;
			GenerateTile (x,y,grass);
			for (int i = y-1; i >= -1 * chunkdepth; i--)  {
				GenerateTile (x, i, earth);
			}
		}

		//Generate last column
		yDiff = Random.Range (-1, 2);

		x += 1;
		y = y + yDiff;
		lastTile = GenerateTile (x, y, grass);
		for (int i = y-1; i >= -1 * chunkdepth; i--)  {
			GenerateTile (x, i, earth);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		chunkManager.GetComponent<ChunkManager> ().SetChunk (this.gameObject);
	}
}
