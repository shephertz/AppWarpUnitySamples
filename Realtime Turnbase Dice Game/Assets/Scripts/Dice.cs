using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Dice : MonoBehaviour {
	public float forceAmount=10f;
	public float torqueAmount=10f;
	public ForceMode forceMode;
	Vector3 move;
	float hor;
	float vert;
public	int numb;
	Listener listen;
//	appwarp app;
	public bool control;
	public bool enter;
	public bool flag;
	public bool gameStart;
	Camera cam;
	int timer;

//	string  userName;
//	string sender;
//	string nextTurn;
	// Use this for initialization
	void Start () {
		gameStart = false;
		control = false;
		enter = true;
		cam = Camera.main;
		flag = true;
		listen=GameObject.FindWithTag("Player").GetComponent<Listener>();
		timer = 0;
//		app=GameObject.FindWithTag("Player").GetComponent<appwarp>();

	}
	
	// Update is called once per frame
	void Update () {
		//userName = appwarp.username;
		if ((Input.GetButton("Horizontal")||Input.GetButton("Vertical")) && enter && gameStart) {
			numb=0;
			control = true;
			enter =false;
			Invoke ("change", 3);
	//		Debug.Log("dice horiz");
		}
//		if (control)
//			Invoke ("change", 3);
//
//		//Debug.Log (GetComponent<Rigidbody>().velocity.magnitude);
	}

	void FixedUpdate () {
		if (control) {
			hor = Input.GetAxis ("Horizontal");
			vert = Input.GetAxis ("Vertical");
//			GetComponent<Rigidbody>().AddForce(Random.onUnitSphere*forceAmount,forceMode);
//			GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*torqueAmount,forceMode);
		} else {
			hor = 0;
			vert = 0;
		}
		move = vert*Vector3.forward + hor*Vector3.right;
		GetComponent<Rigidbody>().AddForce(move*forceAmount,forceMode);
		GetComponent<Rigidbody>().AddTorque(move*torqueAmount,forceMode);

		if (GetComponent<Rigidbody> ().velocity.magnitude < 0.1f && !control && !enter && flag) {
			Debug.Log("GETNUMBBBB");
			numb = DiceFace ();



			if(timer>15){
	           GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 30f,forceMode);
				timer=0;
             }

			if(numb!=0 ){
				changeScene();
			}
			timer++;

		}
	}

	int DiceFace(){
		int t=0;
		if (Vector3.Dot (transform.up, Vector3.up) == 1) {
			t = 5;
		
		}
		if (Vector3.Dot (-transform.up, Vector3.up) == 1) {
			t = 2;

		}
		if (Vector3.Dot (transform.forward, Vector3.up) == 1) {
			t = 1;

		}
		if (Vector3.Dot (-transform.forward, Vector3.up) == 1) {
			t = 6;

		}
		if (Vector3.Dot (transform.right, Vector3.up) == 1) {
			t = 4;
		
		}
		if (Vector3.Dot (-transform.right, Vector3.up) == 1) {
			t = 3;

		}
//		if (Vector3.Dot (transform.up, Vector3.up) != 1) {
//			t = 0;
//
//		}


		return t;


	}


	 void change(){
		control = false;
	}

	void changeScene(){

		cam.transform.parent = GameObject.FindWithTag ("Player").transform;
//		cam.transform.localPosition = new Vector3 (1.67f,1.38f,1f);
//		cam.transform.localEulerAngles = new Vector3 (16f,270f,8.89f);

		cam.transform.localPosition = new Vector3 (-34f,71f,193f);
		cam.transform.localEulerAngles = new Vector3 (0,168.014f,0);

		GameObject.FindWithTag ("Player").GetComponent <Move>().Run (numb);
		flag = false;
		gameStart = false;
		sendMove (numb);


	}
//	public void assign(string sender, string nextTurn){
//		this.sender = sender;
//		this.nextTurn = nextTurn;
//		if(nextTurn==userName)
//		gameStart = true;
//	}

	public void sendMove(int num){
		string data= num.ToString();
		Debug.Log ("Data"+data);
		listen.moveSend(data);
	}

}
