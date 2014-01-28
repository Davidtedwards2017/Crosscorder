using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public float GunAimSpeed;
	public float FireCooldownTime;
	public Transform bulletPrefab;

	private float m_fireCooldown;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		if (m_fireCooldown >= 0)
			m_fireCooldown -= Time.deltaTime;
	}

	public void Fire(Vector3 MousePos)
	{
		if (m_fireCooldown >= 0)
			return;

		Vector3 mouseHitWorld = Vector3.zero;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(MousePos);
		if (Physics.Raycast (ray, out hit))
		{
			mouseHitWorld = hit.point;
			mouseHitWorld.z = 0;
		} 

		SpawnBullet (mouseHitWorld);

		m_fireCooldown = FireCooldownTime;
	}



	private void SpawnBullet(Vector3 AimAtLocation)
	{
		//Quaternion spawnRot = Quaternion.Euler(AimAtLocation - transform.position);
		Transform t = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as Transform;
		BulletScript bullet = t.GetComponent<BulletScript> ();
		bullet.direction = (AimAtLocation - transform.position).normalized;
		bullet.SetColor (gameObject.GetComponent<Pawn>().pawncolor);
		bullet.owner = gameObject;
	}

}
