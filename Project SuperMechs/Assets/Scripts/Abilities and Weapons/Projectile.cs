using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
	[Tooltip("Range of the projectile")]
    public float Range = 0;

	[Tooltip("The speed of the projectile")]
	public float Speed = 0;

	[Tooltip("The damage applied by the projectile")]
	public float Damage = 0;

	/// <summary>
	/// Layer mask of the projectile set by the gun
	/// </summary>
	private int Layermask = 0;

	private Rigidbody2D rb2d;

	private void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// Moveprojectile in the forward dirrection
		rb2d.MovePosition((Vector3) rb2d.position + transform.up * Speed * Time.deltaTime);
		//transform.position += transform.up * Speed * Time.deltaTime;

		// decrease the range
		Range -= Speed * Time.deltaTime;

		// if the range is less that 0, destory the projectile
		if(Range <= 0)
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Properties of the projectile to be set 
	/// </summary>
	/// <param name="range"> Range of the projectile </param>
	/// <param name="speed"> Speed of the projectile</param>
	/// <param name="damage"> Damage applied by the projectile </param>
	/// <param name="layermask"> The layermask indicating objects the projectile can hit</param>
	public void SetProperties(float range, float speed, float damage, int layermask)
	{
		Range = range;
		Speed = speed;
		Damage = damage;
		Layermask = layermask;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		HandleCollision(collision);
	}

	/// <summary>
	/// Handle the collision of the projectile with another object
	/// </summary>
	/// <param name="collision"> The collider2d of the other object </param>
	private void HandleCollision(Collider2D collision)
	{
		// Bit left shift the current layer of the other object
		int collisionLayer = 1 << collision.gameObject.layer;

		// bitwise and to check if collision is part of the layermask
		int result = Layermask & collisionLayer;

		// If the result is not zero, it means that collision layer was part of layermask
		if (result != 0)
		{
			// Get the shootable object
			Shootable shootable = collision.gameObject.GetComponent<Shootable>();

			// If the shootable is not null
			if(shootable)
			{
				// Call the virtual function OnShotAt
				shootable.OnShotAt(Damage, transform.up);
			}

			// Destory the projectile
			Destroy(gameObject);
		}
	}
}
