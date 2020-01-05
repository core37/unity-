using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Engine;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	private GUIStyle Wordsetting;
	private GUIStyle Btnsetting;
	public int win;

	void Start(){
		action = Director.get_Instance ().curren as UserAction;

		Wordsetting = new GUIStyle ();
		Wordsetting.fontSize = 40;
		// MyStyle.normal.textColor = new Color (255f, 0, 0);
		Wordsetting.alignment = TextAnchor.MiddleCenter;
		Btnsetting = new GUIStyle ("button");
		Btnsetting.fontSize = 16;
	}
	void reset(){
		if (GUI.Button (new Rect (Screen.width/2 - 50 , Screen.height- 100, 100, 40), "Reset", Btnsetting)) {
			win = 0;
			action.restart ();
			moveable.move_able = 0;
		}
	}

	void OnGUI(){
		reset ();
		if (win == -1) {
			GUI.Label (new Rect (Screen.width/2- 50, 50, 100, 50), "Game Over", Wordsetting);
			reset ();
		} else if (win == 1) {
			GUI.Label (new Rect (Screen.width/2- 50, 50, 100, 50), "Win", Wordsetting);
			reset ();
		}
	}
}
