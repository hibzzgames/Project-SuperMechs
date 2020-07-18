using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
	/// <summary>
	/// Make source look at the target
	/// </summary>
	/// <param name="source"> The object that needs to rotate and look at something </param>
	/// <param name="target"> The target to look at </param>
    public static void LookAt(Transform source, Vector3 target)
	{
		Vector3 diff = target - source.position;
		diff.Normalize();

		float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		source.rotation = Quaternion.Euler(0, 0, rotZ - 90);
	}
}
