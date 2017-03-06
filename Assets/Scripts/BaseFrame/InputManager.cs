using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class InputManager : MonoBehaviour {

	public GameObject clickableObj;
	public Vector2 clickableSize = new Vector2(.5f,.5f);


    public void Update()
    {
      
        if(Input.GetMouseButtonDown(0))
        {
            _MouseClicked(Input.mousePosition);
        }
        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _MouseClicked(Input.GetTouch(0).position);
        }

    }
	public void OnMouseOver()
	{
		MouseOver (Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	private void _MouseClicked(Vector2 clickPos)
	{
		Vector2 clickedPos = Camera.main.ScreenToWorldPoint (clickPos);

		if (clickableObj) {
			bool inrange = true;
			if ((clickedPos.x > clickableObj.transform.position.x - clickableSize.x && clickedPos.x < clickableObj.transform.position.x + clickableSize.x) &&
				(clickedPos.y > clickableObj.transform.position.y - clickableSize.x && clickedPos.y < clickableObj.transform.position.y + clickableSize.y))
			{
				MouseClickedOnObjOfInterest ();
			}
			else
			{
				MouseDown(clickedPos);
			}
				

		} else {
			MouseDown (clickedPos);
		}
	}

	protected virtual void MouseOver(Vector2 mouseWorldPos)
	{

	}

	protected virtual void MouseDown(Vector2 mouseWorldPos)
	{

	}

	protected virtual void MouseClickedOnObjOfInterest()
	{

	}
}
