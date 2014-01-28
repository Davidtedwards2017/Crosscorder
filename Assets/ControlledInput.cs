using UnityEngine;
using System.Collections;

public class ControlledInput : MonoBehaviour {

	public Vector3 MouseLoc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 GetdirectionVector()
	{
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);


		return directionVector;
	}
	public bool GetJump()
	{
		return Input.GetButton("Jump");
	}

	public bool GetFire ()
	{
		return Input.GetButton("Fire1");
	}
	
	public Vector3 GetMousePos()
	{
		return Input.mousePosition;
	}


	

}
