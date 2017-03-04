using UnityEngine;
using System.Collections;

public class RingBehaviour : MonoBehaviour {

	public GameObject ringSound;

	void Update() 
	{
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			ScoreManager.score++;
			GameManager.gm.ringSound.Play ();
		}

	}
}
