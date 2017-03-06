using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class CameraFollow : MonoBehaviour {

    public float dampTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    public Transform toFollow;
    public Vector3 offset;
    public bool useLimits;
    public Vector2 limitBottomLeft;
    public Vector2 limitTopRight;
    public float viewportMargin = 0.5f;
    Camera cam;

    public void Awake()
    {
        cam = GetComponent<Camera>();
    }

	// Update is called once per frame
	void Update () {
		if (toFollow) {
            //transform.position = new Vector3(toFollow.position.x + offset.x, toFollow.position.y + offset.y, -10);

            Vector3 target = GetTarget();
            Vector3 point = cam.WorldToViewportPoint(target);
            Vector3 delta = target - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
	}

    public Vector3 GetTarget()
    {
        Vector3 target = new Vector3(toFollow.position.x + offset.x, toFollow.position.y + offset.y, -10);

        if (useLimits)
        {
            Vector3 vpTarget     = cam.WorldToViewportPoint(target);
            Vector3 vpTopRight   = cam.WorldToViewportPoint(limitTopRight);
            Vector3 vpBottomLeft = cam.WorldToViewportPoint(limitBottomLeft);

            bool overrule = false;
           
            if(vpTarget.x < vpBottomLeft.x + viewportMargin)
            {
                vpTarget.x = vpBottomLeft.x + viewportMargin;
                overrule = true;
            }
            if (vpTarget.x > vpTopRight.x - viewportMargin)
            {
                vpTarget.x = vpTopRight.x - viewportMargin;
                overrule = true;
            }
            if (vpTarget.y < vpBottomLeft.y + viewportMargin)
            {
                vpTarget.y = vpBottomLeft.y + viewportMargin;
                overrule = true;
            }
            if (vpTarget.y > vpTopRight.y - viewportMargin)
            {
                vpTarget.y = vpTopRight.y - viewportMargin;
                overrule = true;
            }

            if (overrule)
            {
                target = cam.ViewportToWorldPoint(vpTarget);
                target.z = -10;
            }
        }
        return target;
    }

	public void SetZoom(float z)
	{
        cam.orthographicSize = z;
	}
}
