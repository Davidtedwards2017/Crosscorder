using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float BulletSpeed;
	public float LifeTime;
	public Vector3 direction;
	public GameObject owner;
	// Use this for initialization
	void Start () {
	}

	void Awake()
	{

	}

	public void SetColor(Color color)
	{
		TrailRenderer trail = GetComponent<TrailRenderer> ();
		trail.material.color = color;

	}
	// Update is called once per frame
	void Update () {

		transform.position += (direction * BulletSpeed) * Time.deltaTime;

		LifeTime -= Time.deltaTime;

		if (LifeTime <= 0)
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision) 
	{

		if (collision.gameObject == owner) {
			Physics.IgnoreCollision (collision.collider, collider);
			return;
		}

		if (collision.gameObject.tag.Equals ("wall")) 
		{
			direction = Vector3.Reflect(direction, collision.contacts[0].normal);
		}

		
	}
}
