using UnityEngine;
using System.Collections;

public class Equipped : MonoBehaviour {

	public GameObject equipped;

	void OnCollisionStay2D(Collision2D other){
		GameObject tile = other.gameObject;
		if (tile.layer == LayerMask.NameToLayer("Ground")) {
			if (Input.GetMouseButton (0)){
				tile.GetComponent<BlockManager> ().health -= 1;
				int health = tile.GetComponent<BlockManager> ().health;
				if (health <= 0){
					tile.GetComponent<BlockManager> ().DestroyBlock ();
					return;
				}
				tile.GetComponent<BlockManager> ().health -= 1;
				int maxHealth = tile.GetComponent<BlockManager> ().maxHealth;
				int damageStage = (int)(3 - ((float)health / maxHealth * 3));
				tile.GetComponent<SpriteRenderer> ().sprite = tile.GetComponent<BlockManager> ().decayStages [damageStage];
			}
		}
	}
}
