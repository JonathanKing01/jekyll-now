using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {


	public GameObject itemSpawned;
	public int maxHealth = 50;
	public int health = 50;
	public Sprite[] decayStages = new Sprite [4];

	// Use this for initialization
	void Start () {
	
	}

	public void DestroyBlock(){
		GameObject.Instantiate (itemSpawned, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}




}
