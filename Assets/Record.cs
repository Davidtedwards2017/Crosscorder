using UnityEngine;
using System.Collections;

public class Record {

	public Record( Vector3 vec, bool jump, Vector3 mousePos, bool fire)
	{
		directionVector = vec;
		this.Jump = jump;
		this.Fire = fire;
		this.MousePos = mousePos;
	}

	public Vector3 directionVector;
	public bool Jump;
	public bool Fire;
	public Vector3 MousePos;
}
