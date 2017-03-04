using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMode : MonoBehaviour {

	public float speed;
	public float tilt;
	public Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += Vector3.forward * Time.deltaTime;
		//Add lift force ,  set liftBooster to 100 
		//Vector3	lift = Vector3.Project (rb.velocity, transform.forward);
		//rb.AddForce (transform.up * lift.magnitude * 0.2f);

		//Banking controls, turning turning left and right on Z axis
		rb.AddTorque (Input.GetAxis ("Horizontal") * transform.forward * -1f);

		//Pitch controls, turning the nose up and down
		rb.AddTorque (Input.GetAxis ("Vertical") * transform.right );

		//Set drag and angular drag according relative to speed
		rb.drag = 0.1f * rb.velocity.magnitude;
		rb.angularDrag = 0.1f * rb.velocity.magnitude;
		
	}

}
