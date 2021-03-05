using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//*****************
using UnityOSC;
//*****************

public class TouchingScript : GlobalArray
{

    //************* Need to setup this server dictionary...
	//Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
	//*************
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //************* Instantiate the OSC Handler...
	    //OSCHandler.Instance.Init ();
        //*************
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(globalArray.Count);
    }

    void OnCollisionEnter(Collision collision) {
		//Debug.Log("collision");
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/collide", 1);
        if(collision.gameObject.name == "Holder_2" || collision.gameObject.tag == "Object_Balance")
         {
            //OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_1", 1);
            gameObject.tag = "Object_Balance";

            if(!globalArray.Contains(gameObject.name)){
                globalArray.Add(gameObject.name);
            }

            //Debug.Log(gameObject.name + " Objects Touching...Tag: " + gameObject.tag);

            string items = globalArray.Count + " ";
            foreach(var x in globalArray) {
                items += (x + " ");
            }
            Debug.Log(items);
         }
    }

    void OnCollisionExit(Collision collision) {
        if((collision.gameObject.name == "Holder_2" || collision.gameObject.tag == "Object_Balance"))
         {
            globalArray.Remove(gameObject.name);
            gameObject.tag = "Object_Start";
            //Debug.Log(gameObject.name + " NOT touching...Tag: " + gameObject.tag);

            string items = globalArray.Count + " ";
            foreach(var x in globalArray) {
                items += (x + " ");
            }
            Debug.Log(items);
         }
    }
}
