using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtility : MonoBehaviour
{
	/// <summary>
	/// Generate a random vector2 force
	/// </summary>
	/// <param name="intensity"> The intensity of the force </param>
	/// <param name="MinAngle"> Minimum angle </param>
	/// <param name="MaxAngle"> Maximum angle </param>
	/// <returns></returns>
	public static Vector2 RandomForce(float intensity = 1, float MinAngle = 0, float MaxAngle = 360)
	{
		// Random angle based on given input data
		float randomAngle = Random.Range(MinAngle, MaxAngle);

		// Create a force with random angle using vector maths, scaled by intensity
		Vector2 result = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * intensity;
		return result;
	}
}
