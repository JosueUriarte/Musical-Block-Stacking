using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class DragObject : MonoBehaviour 
{
	void Start ()
	{
		//************* Instantiate the OSC Handler...
	    //OSCHandler.Instance.Init ();
        //*************
	}
	
    private Vector3 mOffset;
    private float mZCoord;

    void Update () 
	{
        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
        {
            //Vector3 normalizedDirection = (gameObject.transform.position - Camera.main.transform.position).normalized;
            Vector3 move = new Vector3(50.0f,50.0f,50.0f);
            Camera.main.transform.Translate(move.normalized * Input.GetAxis("Mouse ScrollWheel"));
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            //Vector3 normalizedDirection = (gameObject.transform.position - Camera.main.transform.position).normalized;
            Vector3 move = new Vector3(0.0f,1.0f,0.0f);
            Camera.main.transform.Translate(move * Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void OnMouseDown()
    {
		//Debug.Log("click");
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/pickup", 1);
		
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen

        mousePoint.z = mZCoord;

        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}