using UnityEngine;
using System.Collections;

public class InputRecorder : MonoBehaviour {

	public int MaxRecordSize = 10000;
	public int tickIndex;
	public Record[] InputRecording;
	public bool bRecording = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!bRecording || tickIndex >= MaxRecordSize)
			return;

		Vector3 dirVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		bool bJump = Input.GetButton("Jump");
		bool bFire = Input.GetButton ("Fire1");

		//TODO: update with aim direction and fireing
		//InputRecording [tickIndex] = new Record (dirVec, bJump, Vector3.zero, bFire );
		InputRecording [tickIndex] = new Record (dirVec, bJump, Input.mousePosition, bFire );

		tickIndex++;
	}

	public void StartRecording()
	{
		InputRecording = new Record[MaxRecordSize];
		bRecording = true;
		tickIndex = 0;
	}

	public Record[] StopRecording()
	{
		bRecording = false;
		return InputRecording;
	}
}
