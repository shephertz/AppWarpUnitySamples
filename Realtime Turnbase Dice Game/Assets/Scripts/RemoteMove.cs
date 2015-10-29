using UnityEngine;
using System.Collections;

public class RemoteMove : MonoBehaviour {
	int d;
	public Transform[] p;
	int z;
	bool isLerping;
	int das;
	int n;
	public bool flag;
	private Move move;
	UI ui;

	float timeTakenDuringLerp=0.3f;
	float timeStartedLerping;
	
	void Start(){
		transform.position = p [0].position;
		isLerping = false;
		z = 0;
		flag = false;
		move = GameObject.FindWithTag("Player").GetComponent<Move>();
		ui = GameObject.FindWithTag ("GameController").GetComponent<UI>();
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
		//	Debug.Log("Z"+z);
			//Debug.Log("N"+n);
			transform.position = Vector3.Lerp (transform.position, p [z+ n].position, percentageCompleted);
			if(percentageCompleted>=1.0f){
				Debug.Log("wassssup");
				isLerping=false;
				z = findPresent ();
				if(flag){
					move.dice();
					flag=false;
				}
				
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
		ui.hudO (x);
		return x;
	}
	

	
	
}



