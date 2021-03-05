using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class MovePlayer : GlobalArray {

	public float speed;
	public Text countText;
    public int level;
	private Rigidbody rb;

	//************* Need to setup this server dictionary...
	Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
	//*************

	void OnApplicationQuit() {
        OSCHandler.Instance.SendMessageToClient ("pd", "/unity/start_metro", 0);
    }

	void Awake () {
	    QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true; //allows unity to update when not in focuss

		//************* Instantiate the OSC Handler...
	    OSCHandler.Instance.Init ();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start_metro", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_0", 1);
        //*************

        rb = GetComponent<Rigidbody> ();
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

        //Debug.Log(rb.position.x);

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce(movement*speed);

		checkObjects();

		//************* Routine for receiving the OSC...
		OSCHandler.Instance.UpdateLogs();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;

		foreach (KeyValuePair<string, ServerLog> item in servers) {
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if (item.Value.log.Count > 0) {
				int lastPacketIndex = item.Value.packets.Count - 1;

				//get address and data packet
				countText.text = item.Value.packets [lastPacketIndex].Address.ToString ();
				countText.text += item.Value.packets [lastPacketIndex].Data [0].ToString ();

			}
		}
		//*************
	}

	void checkObjects(){

	    if (globalArray.Count == 0 && level != 0){
	        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_0", 1);
	        level = 0;
	    }

	    else if (globalArray.Count == 1 && level != 1){
	        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_1", 1);
	        level = 1;
	    }

	    else if (globalArray.Count == 2 && level != 2){
	        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_2", 1);
	        level = 2;
	    }

	    else if (globalArray.Count == 3 && level != 3){
	        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_3", 1);
	        level = 3;
	    }

	    else if (globalArray.Count == 4 && level != 4){
	        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_4", 1);
	        level = 4;
	    }

	    else{
	        //Do nothing??
	    }
	}

//
//	void OnTriggerEnter(Collider other)
//    {
//        //Debug.Log("-------- COLLISION!!! ----------");
//
//        if (other.gameObject.CompareTag ("Pick Up"))
//		{
//			other.gameObject.SetActive (false);
//			count = count + 1;
//
//            // change the tempo of the sequence based on how many obejcts we have picked up.
//            if(count < 2)
//            {
//                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 500);
//            }
//            if (count < 4)
//            {
//                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 400);
//            }
//            else if(count < 6)
//            {
//                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 300);
//            }
//            else if (count < 8)
//            {
//                OSCHandler.Instance.SendMessageToClient("pd", "/unity/tempo", 150);
//            }
//            else
//            {
//                OSCHandler.Instance.SendMessageToClient ("pd", "/unity/start_metro", 0);
//                //OSCHandler.Instance.SendMessageToClient("pd", "/unity/start_metro", 1);
//                //OSCHandler.Instance.SendMessageToClient("pd", "/unity/lvl_0", 0);
//            }
//
//        }
//        else if(other.gameObject.CompareTag("Wall"))
//        {
//            //Debug.Log("-------- HIT THE WALL ----------");
//            // trigger noise burst whe hitting a wall.
//            OSCHandler.Instance.SendMessageToClient("pd", "/unity/colwall", 1);
//        }
//
//    }


}
