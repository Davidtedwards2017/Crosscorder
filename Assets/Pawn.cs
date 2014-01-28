using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

	public float ZClampValue;
	public Color pawncolor;

	private PlatformInputController controller;
	// Use this for initialization
	void Start () {

	}

	public void SetColor(Color color)
	{
		transform.GetComponentInChildren<Renderer> ().material.color = color;
		pawncolor = color;
		//gameObject.renderer.material.color = color;

//		switch (color) 
//		{
//		case "red":
//			renderer.material = RedMat;
//			break;
//		case "blue":
//			renderer.material[0] = BlueMat;
//			break;
//		case "yellow":
//			renderer.material[0] = YellowMat;
//			break;
//		case "green":
//			renderer.material[0] = GreenMat;
//			break;
//
//		}

	}
	
	// Update is called once per frame
	void Update () {

		//clamp y = 0
		Vector3 pos = transform.position;
		pos.z = ZClampValue;

		transform.position = pos;

	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.tag == "Pawn") {
			Physics.IgnoreCollision (collision.collider, collider);
		}

		if (collision.gameObject.tag.Equals ("bullet")) 
		{
			BulletScript bullet = collision.transform.GetComponent<BulletScript>();
			if(bullet.owner == gameObject)
			{
				Physics.IgnoreCollision (collision.collider, collider);
				return;
			}

			Destroy(bullet);
			GameInfo.PlayerCtrl.PawnKilled(gameObject);
		}
		
	}

}
