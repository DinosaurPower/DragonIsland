using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAR_util {

	public static Quaternion getDeviceAngle(){
		Vector3 vec = Input.acceleration;

		float angleX = 90.0f - Mathf.Acos (Mathf.Clamp (-vec.z, -1.0f, +1.0f)) * 180.0f / Mathf.PI;

		float angleZ = 0.0f;
		float len = Mathf.Sqrt (vec.x * vec.x + vec.y + vec.y);

		if (len > 1.0f) {
			angleZ = Mathf.Atan2(-vec.y, vec.x) * 180.0f / Mathf.PI - 90.0f;
		}

		Quaternion rot = Quaternion.Euler (angleX, 0.0f, 0.0f) * Quaternion.Euler (0.0f, 0.0f, angleZ);
		return rot;
	}
}
