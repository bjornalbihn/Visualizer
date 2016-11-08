using UnityEngine;
using System.Collections;

public static class ArcMath
{
	public static Vector2[] Arc(float radius, float start, float end, int amount)
	{
		if (amount > 0)
		{
			float angles = Mathf.Deg2Rad * (start-end);
			float step = angles/amount;

			Vector2[] values = new Vector2[amount];
			for (int i = 0; i<amount; i++)
			{
				float x = Mathf.Cos(i*step);
				float y = Mathf.Sin(i*step);

				values[i] = new Vector2(x,y) * radius;
			}
			return values;
		}
		else
		{
			return null;
		}
	}

	public static Vector3 Vector(float yaw, float pitch)
	{
		yaw *= Mathf.Deg2Rad;
		pitch *= Mathf.Deg2Rad;

		float x = Mathf.Cos(yaw)*Mathf.Cos(pitch);
		float y = Mathf.Sin(yaw)*Mathf.Cos(pitch);
		float z = Mathf.Sin(pitch);

		return new Vector3(x,y,z);
	}

	// Incomplete
	private static Vector3[] ArcArray(float radius, float xStart, float xEnd, int xAmount, float yStart, float yEnd, int yAmount)
	{
		if (xAmount > 0)
		{
			float angles = Mathf.Deg2Rad * (xStart-xEnd);
			float step = angles/xAmount;
			
			Vector3[] values = new Vector3[xAmount*yAmount];
			for (int i = 0; i<xAmount; i++)
			{
				float x = Mathf.Cos(i*step);
				float y = Mathf.Sin(i*step);
				
				values[i] = new Vector2(x,y) * radius;
			}
			return values;
		}
		else
		{
			return null;
		}
	}

}
