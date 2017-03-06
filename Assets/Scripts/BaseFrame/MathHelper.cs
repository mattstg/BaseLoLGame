using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class MathHelper  {

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector2 V3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static float AngleBetweenPoints(Vector2 p1, Vector2 p2)
    {
        return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
    }

	public static bool Fiftyfifty()
	{
		return (Random.Range (0f, 1f) > .5f);
	}

    public static float ApproxDist(Vector3 pos1, Vector3 pos2)
    {
        
        float deltaX = Mathf.Abs(pos1.x - pos2.x);
        float deltaY = Mathf.Abs(pos1.y - pos2.y);
        float dist = 0;
        if(deltaY >= deltaX)
        {
            dist = (0.41f * deltaX) + (0.94f * deltaY);
        }
        else if(deltaX > deltaY)
        {
            dist = (0.41f * deltaY) + (0.94f * deltaX);
        }
        return (dist);
    }

    public static float ApproxDist(Vector2 _pos1, Vector3 _pos2)
    {
        return (ApproxDist(new Vector3(_pos1.x, _pos1.y, 0), _pos2));
    }

    public static float ApproxDist(Vector3 _pos1, Vector2 _pos2)
    {
        return (ApproxDist(_pos1, new Vector3(_pos2.x, _pos2.y, 0)));
    }

    public static float ApproxDist(Vector2 _pos1, Vector2 _pos2)
    {
        return (ApproxDist(new Vector3(_pos1.x, _pos1.y, 0), new Vector3(_pos2.x, _pos2.y, 0)));
    }
}
