using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[Tooltip("Range of the gun")]
	public float Range = 25.0f;

	[Tooltip("Projectile speed")]
	public float Speed = 20.0f;

	[Tooltip("Fire rate of the gun")]
	public float Firerate = 0.5f;

	[Tooltip("List of layers to shoot through")]
	public List<string> layermaskNames;

	[Tooltip("Reference to a projectile")]
	[InspectorName("Projectile Type")]
	public GameObject projectile;

	/// <summary>
	/// Local fire rate timer help controlling the firerate
	/// </summary>
	private float firerateTimer = 0.5f;

	/// <summary>
	/// Current Layer mask indicating what items the gun can shoot
	/// </summary>
	private int layermask = 0;

	private void Start()
	{
		// Cheap way to not wait for firerate to affect the first projectile
		firerateTimer = Firerate * 2;

		// Generate the gun's layer mask
		generateLayermask();
	}

	/// <summary>
	/// Attempts to shoot from the gun
	/// </summary>
	/// <param name="source"> The source from which thee projectile must be launched</param>
	/// <returns> If the shot was successfully fired or not </returns>
	public bool Shoot(Transform source)
	{
		// If the firerate timer is greater than gun's firerate
		if (firerateTimer > Firerate)
		{
			// reset firerate timer back to 0
			firerateTimer = 0;
			
			// Instantiate a new projectile
			GameObject newProjectile = Instantiate(projectile, source.position, source.rotation);
			
			// Apply the force
			newProjectile.GetComponent<Projectile>().SetProperties(Range, Speed, 20, layermask);
			
			// True indicates the a projectile was fired
			return true;
		}

		// False indicates that a projectile wasn't fired in this loop
		return false;
	}

	/// <summary>
	/// Add a layer to the Gun's later mask
	/// </summary>
	/// <param name="layername"></param>
	public void AddLayersToHit(string layername)
	{
		// Adds it to a string list
		layermaskNames.Add(layername);
		
		// regrenarate a new layer mask
		generateLayermask();
	}

	/// <summary>
	/// Remove the given string layer name from gun's layer mask
	/// </summary>
	/// <param name="layername"> The name of the layer to be removed </param>
	public void RemoveLayersToHit(string layername)
	{
		// removes from layer string list
		layermaskNames.Remove(layername);

		// regenereate a new layer mask 
		generateLayermask();
	}

	/// <summary>
	/// Generate's a layermask for the gun based on list of layer strings
	/// </summary>
	private void generateLayermask()
	{
		// initialize to zero
		layermask = 0;

		// For every string in layermaskNames list
		foreach(string layername in layermaskNames)
		{
			// bitwise left shift
			int currentLayer = 1 << LayerMask.NameToLayer(layername);
			
			// currentlayer gets added to layermask using a bitwise or operation
			layermask = layermask | currentLayer;
		}
	}	

	private void Update()
	{
		// increment the firerate timer
		if(firerateTimer <= Firerate) { firerateTimer += Time.deltaTime; }
	}
}

