using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Engine;

public class MySceneController : MonoBehaviour, SceneController, UserAction{
	readonly Vector3 water_pos = new Vector3 (0, 0.5f, 0);
	UserGUI user;
	public CoastController fromCoast;
	public CoastController toCoast;
	public BoatController boat;
	public GameObject water;
	public Judgement judgement;
	//private MyCharacterController[] characters;
	private List<MyCharacterController> team;

	void Awake(){
		Director director = Director.get_Instance ();
		judgement = new Judgement();
		director.curren = this;
		user = gameObject.AddComponent<UserGUI> () as UserGUI;
		//characters = new MyCharacterController[6];
		team = new List<MyCharacterController>();
		loadResources ();
	}
	public void loadResources(){

		water.name = "water";

		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();

		for (int i = 0; i < 3; i++) {
			MyCharacterController tem = new MyCharacterController ("priest");
			tem.setName ("priest" + i);
			tem.setPosition (fromCoast.getEmptyPosition ());
			tem.getOnCoast (fromCoast);
			fromCoast.getOnCoast (tem);
			team.Add (tem);
		}
		for (int i = 0; i < 3; i++) {
			MyCharacterController tem = new MyCharacterController ("devil");
			tem.setName ("devil" + i);
			tem.setPosition (fromCoast.getEmptyPosition ());
			tem.getOnCoast (fromCoast);
			fromCoast.getOnCoast (tem);
			team.Add (tem);
		}
	}
	public void moveboat(){
		if (boat.IfEmpty ())
			return;
		boat.boatMove ();
		//check whether game over
		user.win = judgement.Stat(fromCoast, toCoast, boat);
	}
	public void isClickChar (MyCharacterController tem_char){
		if (moveable.move_able == 1)
			return;
		if (tem_char._isOnBoat ()) {
			CoastController tem_coast;
			if (boat.getTFflag () == -1) {
				tem_coast = toCoast;
			} else {
				tem_coast = fromCoast;
			}
			boat.getOffBoat (tem_char.getName ());
			tem_char.moveToPosition (tem_coast.getEmptyPosition ());
			tem_char.getOnCoast (tem_coast);
			tem_coast.getOnCoast (tem_char);
		} else {
			CoastController tem_coast2 = tem_char.getCoastController ();
			if (boat.getEmptyIndex () == -1)
				return;
			if (boat.getTFflag () != tem_coast2.getTFflag ())
				return;
			tem_coast2.getOffCoast (tem_char.getName());
			tem_char.moveToPosition (boat.getEmptyPosition ());
			tem_char.getOnBoat (boat);
			boat.getOnBoat (tem_char);
		}
		//check whether game over;
		user.win = judgement.Stat(fromCoast, toCoast, boat);
	}
	public void restart(){
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		foreach (MyCharacterController i in team) {
			i.reset ();
		}
		moveable.move_able = 0;
	}
	public void pause(){
		boat.Mypause ();
		foreach (MyCharacterController i in team) {
			i.Mypause();
		}
	}
	public void Coninu (){
		boat.MyConti ();
		foreach (MyCharacterController i in team) {
			i.MyConti();
		}
	}

}


