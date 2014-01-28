using UnityEngine;
using System.Collections;

public class RecordedInput : MonoBehaviour {

	public int TimeIndex = 0;
	public Record[] Recording;
	public bool bPlaying = false;


	public void SetRecord(Record[] record)
	{
		Debug.Log ("Setting Record for " + gameObject.name);
		Recording = record;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate()
	{
		if (!bPlaying)
			return;

		TimeIndex++;
	}

	public Vector3 GetdirectionVector()
	{
		if (TimeIndex >= Recording.Length)
			return Vector3.zero;

		return Recording[TimeIndex].directionVector;
		//return Recording [TimeIndex].directionVector;
	}
	public bool GetJump()
	{
		if (TimeIndex >= Recording.Length)
			return false;

		return Recording[TimeIndex].Jump;
		//return Recording [TimeIndex].Jump;
	}
	public bool GetFire()
	{
		if (TimeIndex >= Recording.Length)
			return false;
		
		return Recording [TimeIndex].Fire;
		//return Recording [TimeIndex].Jump;
	}
	public Vector3 GetMousePos()
	{
		if (TimeIndex >= Recording.Length)
			return Vector3.zero;
		
		return Recording [TimeIndex].MousePos;
	}

}
