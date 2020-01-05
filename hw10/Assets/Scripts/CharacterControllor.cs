using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;

namespace chCtrl
{
    public class CharacterControllor
    {
        GameObject role;
        int role_sign;
        ClickGUI ClickGUI;
        bool on_boat;
        LandControllor land_model = (SSDirector.GetInstance().CurrentSceneController as FirstControllor).start_land;

        public float move_speed = 30;
        public CharacterControllor(string role_name)
        {
            if (role_name == "priest")
            {
                role = Object.Instantiate(Resources.Load("prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
                role.transform.Rotate(Vector3.up, -90);
                role_sign = 0;
            }
            else
            {
                role = Object.Instantiate(Resources.Load("prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
                role.transform.Rotate(Vector3.up, -90);
                role_sign = 1;
            }
            ClickGUI = role.AddComponent(typeof(ClickGUI)) as ClickGUI;
            ClickGUI.SetRole(this);
        }

        public int GetSign() { return role_sign; }
        public LandControllor GetLandControllor() { return land_model; }
        public string GetName() { return role.name; }
        public bool IsOnBoat() { return on_boat; }
        public void SetName(string name) { role.name = name; }
        public void SetPosition(Vector3 pos) { role.transform.position = pos; }
        public GameObject getGameObject() { return role; }


        public void getOnLand(LandControllor land)
        {
            role.transform.parent = null;
            land_model = land;
            on_boat = false;
        }

        public void getOnBoat(BoatControllor boat)
        {
            role.transform.parent = boat.GetBoat().transform;
            land_model = null;
            on_boat = true;
        }

        public void Reset()
        {
            land_model = (SSDirector.GetInstance().CurrentSceneController as FirstControllor).start_land;
            getOnLand(land_model);
            SetPosition(land_model.GetEmptyPosition());
            land_model.addCharacter(this);
        }
    }

}
