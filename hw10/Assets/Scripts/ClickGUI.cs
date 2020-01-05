using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;

public class ClickGUI : MonoBehaviour
{
    IUserAction action;
    CharacterControllor role = null;
    BoatControllor boat = null;
    public void SetRole(CharacterControllor role)
    {
        this.role = role;
    }
    public void SetBoat(BoatControllor boat)
    {
        this.boat = boat;
    }
    void Start()
    {
        action = SSDirector.GetInstance().CurrentSceneController as IUserAction;
    }
    void OnMouseDown()
    {
        if (boat == null && role == null) return;
        if (boat != null)
            action.MoveBoat();
        else if (role != null)
            action.MoveRole(role);
    }
}
