using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	 Text you;
	 Text opponent;

	// Use this for initialization
	void Start () {
		you = GameObject.Find("YOU").GetComponent<Text> ();
		opponent = GameObject.Find("OPPONENT").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void hudP(int x){
		string s = x.ToString ();
		you.text = s;

	}
	public void hudO(int y){
		string r = y.ToString ();
		opponent.text = r;

	}
}
