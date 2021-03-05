using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class resetObject : GlobalArray
{
    // Start is called before the first frame update

    public GameObject Teleport;
    private Quaternion originalRotationValue;

    void Start()
    {
        originalRotationValue = transform.rotation;
		//************* Instantiate the OSC Handler...
	    //OSCHandler.Instance.Init ();
        //*************
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == "Ground_3")
         {
             //Debug.Log("Object Reset");
             gameObject.tag = "Object_Start";
             globalArray.Remove(gameObject.name);
             GetComponent<Rigidbody>().velocity = Vector3.zero;
             GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
             transform.rotation = originalRotationValue;
             transform.position = Teleport.transform.position;
			 OSCHandler.Instance.SendMessageToClient("pd", "/unity/splash", 1);
         }
    }
}
