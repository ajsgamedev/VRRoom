using UnityEngine;
using System.Collections;

public class SpawnMover : MonoBehaviour {

	public GameObject spawner; // reference to the spawner to move
	public GameObject player;
	public GameObject[] myWaypoints; // array of all the waypoints
	private Vector3 offset;

	[Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
	public float moveSpeed = 5f; // enemy move speed
	public float waitAtWaypointTime = 1f; // how long to wait at a waypoint before moving to next waypoint

	public bool loop = true; // should it loop through the waypoints

	// private variables

	Transform _transform;
	int myWaypointIndex = 0;		// used as index for My_Waypoints
	float moveTime;
	bool moving = true;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		_transform = spawner.transform;
		moveTime = 0f;
		moving = true;
	}
	
	// game loop
	void Update () {
		// if beyond moveTime, then start moving
		if (Time.time >= moveTime) {
			Movement();
		}
	}

	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}

	void Movement() {
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (moving)) {

			// move towards waypoint
			_transform.position = Vector3.MoveTowards(_transform.position, myWaypoints[myWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if(Vector3.Distance(myWaypoints[myWaypointIndex].transform.position, _transform.position) <= 0) {
				myWaypointIndex++;
				moveTime = Time.time + waitAtWaypointTime;
			}
			
			// reset waypoint back to 0 for looping, otherwise flag not moving for not looping
			if(myWaypointIndex >= myWaypoints.Length) {
				if (loop)
					myWaypointIndex = 0;
				else
					moving = false;
			}
		}
	}
}
