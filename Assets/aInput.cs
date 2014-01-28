using UnityEngine;
using System.Collections;

public abstract class aInput: MonoBehaviour {


	public abstract Vector3 GetdirectionVector();
	public abstract bool GetJump();
}
