using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}


public class PlayerMovement : MonoBehaviour {

	public static int teleportCount = 0;
	public float speed;

	public float tilt;
	public Boundary boundary;
	Vector3 move;
	float reduceSpeed;

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
		teleportCount = 0;
	}
	void Start()
	{
		teleportCount = 0;
		reduceSpeed = 0.7f;
	}

	void Update ()
	{
		float moveHorizontal = Input.acceleration.x;

		transform.Translate(moveHorizontal,0,0);

		Vector3 move = new Vector3 (0.0f, 0.0f,moveHorizontal);

		GetComponent<Rigidbody>().velocity = move*(speed-reduceSpeed);
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			transform.position.z+reduceSpeed);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.z * -tilt);

	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "teleport")
		{
			transform.position = new Vector3 (transform.position.x, 0.0f, 0.0f);
			teleportCount++;
		}

		if (coll.gameObject.tag == "obsta")
		{
			StartCoroutine (SpeedDown ());
		}

	}

	IEnumerator SpeedDown()
	{
		reduceSpeed = 0.3f;

		yield return new WaitForSeconds(3);

		reduceSpeed = 0.7f;
	}
}
