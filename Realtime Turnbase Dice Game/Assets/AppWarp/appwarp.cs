using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

//using UnityEditor;

using AssemblyCSharp;

public class appwarp : MonoBehaviour 
{    
	public string apiKey = "api Key";
	public string secretKey = "secret Key";
	//public string roomid = "Room ID";
	public static string username = "";

	public bool useUDP = true;
	//private Dice dice;
	private Move move;
	
	Listener listen;
	
	public Transform remotePrefab;
	//bool start;
	RemoteMove rm;
	void Start () {

		rm =remotePrefab.GetComponent<RemoteMove>();
		move = GetComponent<Move>();
		WarpClient.initialize(apiKey,secretKey);  //initializing the SDK

		listen = GetComponent<Listener>(); /*In Order to receive notification,connection & join/leave callbacks, you need 
		                                     to add corresponding listners as defined in listner class*/
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddLobbyRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		WarpClient.GetInstance().AddUpdateRequestListener(listen);
		WarpClient.GetInstance().AddZoneRequestListener(listen);
		WarpClient.GetInstance().AddTurnBasedRoomRequestListener(listen);
		
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString(); //Unique username is generated
			WarpClient.GetInstance().Connect(username);

	}

	public void CreateR(){
		listen.createR (username,"Rahul",2,50);
	}
	public float interval = 0.1f;

	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		WarpClient.GetInstance().Update();
	}
	
	void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10,10,500,100), listen.getDebug());
	}
	
	/*void OnEditorStateChanged()
	{
    	if(EditorApplication.isPlaying == false) 
		{
			WarpClient.GetInstance().Disconnect();
    	}
	}*/
	
	void OnApplicationQuit()
	{
		WarpClient.GetInstance().Disconnect();
	}
	
	public void onMsg(string sender, string msg)
	{
		/*
		if(sender != username)
		{
			
		}
		*/
	}
	public void startGame(){
		//start = true;
	
	}



	public void moveCompleted (string sender,string data,string next){
		int diceD;
		Debug.Log ("aappwarp"+data);
		diceD = int.Parse (data);
		Debug.Log ("aappwarp2"+diceD);
		if (sender != username) {
			remotePrefab.name=sender;
			rm.Run(diceD);

			Debug.Log("RemoteRun");

		}
		if (username == next) {
			//move.dice();
			rm.flag=true;
		
		}

	}

	public void gameStarted(string sender){
		if(sender==username)
		move.dice();
	}

	
	public void onUserLeft(string user)
	{
		GameObject remote;
		remote = GameObject.Find(user);
		
		if(remote != null)
		{
			Destroy(remote);
		}
	}
	
}
