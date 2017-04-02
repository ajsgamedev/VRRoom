using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}


public class PlayerMovement : MonoBehaviour {

	public float speed;

	public float tilt;
	public Boundary boundary;
	Vector3 move;
	float reduceSpeed=1;

	void Awake()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Debug.Log ("Mobile");
			move = new Vector3 (0.0f, 0.0f, Input.acceleration.x);

		}
		else
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			Debug.Log ("Windows");
			move = new Vector3 (0.0f, 0.0f, Input.GetAxis ("Horizontal"));

		}
	}


	void Update ()
	{
		float moveHorizontal = Input.acceleration.x;

		transform.position += (Vector3.forward*reduceSpeed) * Time.deltaTime;

		transform.Translate(moveHorizontal,0,0);

		Vector3 move = new Vector3 (0.0f, 0.0f,moveHorizontal);

		GetComponent<Rigidbody>().velocity = move*speed;
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			transform.position.z+0.7f);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.z * -tilt);

	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "teleport")
		{
			transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		}

		if (coll.gameObject.tag == "obsta")
		{
			StartCoroutine (SpeedDown ());
		}

	}

	IEnumerator SpeedDown()
	{
		reduceSpeed /= 2;

		yield return new WaitForSeconds(10);

		reduceSpeed *= 2;
	}
}
