using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[Header("Interactable Properties")]
	[Tooltip ("The mode of interaction with this interactable object")]
	public InteractMethod interactMethod = InteractMethod.InteractKeyPress;

	/// <summary>
	/// Private reference to the collider attached to this object
	/// </summary>
	private new Collider2D collider;

	private void Start()
	{
		collider = GetComponent<Collider2D>();
	}

	/// <summary>
	/// This function is called when an object interacts with this Interactable
	/// </summary>
	/// <param name="Interactii"> The object that interacts with Interactable </param>
	public virtual void OnInteract(GameObject Interactii)
	{
		Debug.Log(Interactii.name + "is interacting with " + name);
	}

	/// <summary>
	/// Display a friendly interact indicator message
	/// </summary>
	public virtual void DisplayInteractMessage()
	{
		Debug.Log("Press interact key to interact with " + name);
	}

	/// <summary>
	/// Attempt to interact with this object
	/// </summary>
	/// <param name="Interactii"> The object interacting with Interactable </param>
	/// <param name="IsInteractKeyPressed"> Is the interact key pressed during this attempt at interaction </param>
	public void Interact(GameObject Interactii, bool IsInteractKeyPressed)
	{
		// If the two object are not touching, do not proceed
		if (!Interactii.GetComponent<Rigidbody2D>().IsTouching(collider))
			return;

		// If the interactii touches it, handle interaactions
		if(interactMethod == InteractMethod.WalkOver)
        {
			OnInteract(Interactii);
        }

		// If the interactii touches it, and the interaction key was pressed, handle the interaction 
		else if(interactMethod == InteractMethod.InteractKeyPress && IsInteractKeyPressed)
        {
			OnInteract(Interactii);
        }
	}


	// Method of interaction
	public enum InteractMethod
	{
		WalkOver,
		InteractKeyPress
	}
}
