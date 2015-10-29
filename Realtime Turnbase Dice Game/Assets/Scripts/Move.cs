using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public Transform[] p;
	Camera cam;
	int z;
	bool isLerping;
	int das;
	int n;

	UI ui;
	//public bool changeScene;
	//public GameObject explosion;
	float timeTakenDuringLerp=0.3f;
	float timeStartedLerping;

	void Start(){
	//	changeScene = false;
		cam = Camera.main;
		cam.transform.parent = gameObject.transform; /* GameObject.FindWithTag ("Player").transform;*/
//		cam.transform.localPosition = new Vector3 (1.67f,1.38f,1f);
//		cam.transform.localEulerAngles = new Vector3 (16f,270f,8.89f);

		cam.transform.localPosition = new Vector3 (-34f,71f,193f);
		cam.transform.localEulerAngles = new Vector3 (0,168.014f,0);

		transform.position = p [0].position;
		ui = GameObject.FindWithTag ("GameController").GetComponent<UI>();
		isLerping = false;
		z = 0;

		
	}
	
	void Update(){
//if (changeScene)
//			dice ();
	}
	public void Run(int n){
		this.n = n;
		timeTakenDuringLerp =n;
		timeStartedLerping = Time.time;
		isLerping = true;
	}

	void FixedUpdate(){
		if (isLerping) {
			float timeSinceStarted= Time.time-timeStartedLerping;
			float percentageCompleted=timeSinceStarted/timeTakenDuringLerp;
			transform.position = Vector3.Lerp (transform.position, p [z+ n].position, percentageCompleted);
			if(percentageCompleted>=1f){
				isLerping=false;
				z = findPresent ();
				//upgrade();
			}
		
		}
	}
	
	int findPresent(){
		int x=0;
		for (int i =0; i<p.Length; i++) {
			if(transform.position == p[i].position){
				x=i;
			}
		}
		ui.hudP (x);
		return x;
	}
	
	public void dice(){
		Debug.Log ("Present"+z);
		GameObject.FindWithTag ("Dice").GetComponent<Dice> ().enter = true;
		GameObject.FindWithTag ("Dice").GetComponent<Dice> ().control = false;
		GameObject.FindWithTag ("Dice").GetComponent<Dice> ().flag = true;
		GameObject.FindWithTag ("Dice").GetComponent<Dice> ().gameStart = true;
		cam.transform.parent = null;
		cam.transform.position= new Vector3 (190.2f,26.85f,13.83f);
		cam.transform.eulerAngles = new Vector3 (90f,0,0);

		
	}

	void upgrade(){
		int x = p [z].gameObject.GetComponent<Upgrades> ().x;

		switch (x) {
		case 0 :
			Debug.Log("Do nothing");
			//Invoke ("dice",1.5f);
			break;

		case 1:
			Run(5);
			break;

		case 2:
			Run (2);
			break;

		case 3:
			Blast();
			break;
		
		}
	}

	void Blast(){
		//Instantiate (explosion, transform.position, Quaternion.identity);
		//Invoke ("dice",0.7f);
	}


	
	
}
