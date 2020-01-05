using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;


public class FirstControllor : MonoBehaviour, ISceneController, IUserAction
    {
        public LandControllor start_land;
        public LandControllor end_land;
        public BoatControllor boat;
        private CharacterControllor[] obj;
        UserGUI user_gui;
        Judge judge = new Judge();
    public MySceneActionManager actionManager;

        void Start()
        {
            SSDirector director = SSDirector.GetInstance();
            director.CurrentSceneController = this;
            user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
            LoadResources();

            actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager;
        }
        Vector3 riverPos = new Vector3(0, -1f, 0);
        public void LoadResources()
        {
            GameObject water = Instantiate(Resources.Load("prefabs/River", typeof(GameObject)), riverPos, Quaternion.identity) as GameObject;
            water.name = "water";
            start_land = new LandControllor("start");
            end_land = new LandControllor("end");
            boat = new BoatControllor();
            obj = new CharacterControllor[6];

            for (int i = 0; i < 3; i++)
            {
                CharacterControllor role = new CharacterControllor("priest");
                role.SetName("priest" + i);
                role.SetPosition(start_land.GetEmptyPosition());
                role.getOnLand(start_land);
                start_land.addCharacter(role);
                obj[i] = role;
            }

            for (int i = 0; i < 3; i++)
            {
                CharacterControllor role = new CharacterControllor("devil");
                role.SetName("devil" + i);
                role.SetPosition(start_land.GetEmptyPosition());
                role.getOnLand(start_land);
                start_land.addCharacter(role);
                obj[i + 3] = role;
            }
        }

        public void MoveBoat()
        {
            if (boat.IsEmpty() || user_gui.status != 0) return;
            actionManager.moveBoat(boat.getGameObject(), boat.BoatMoveToPosition(), boat.move_speed);
            user_gui.status = Check();


            judge.judge(user_gui.status);


        }

        public void MoveRole(CharacterControllor role)
        {
            if (user_gui.status != 0) return;
            if (role.IsOnBoat())
            {
                LandControllor land;
                if (boat.GetBoatSign() == -1)
                    land = end_land;
                else
                    land = start_land;
                boat.removeCharacter(role.GetName());

                Vector3 end_pos = land.GetEmptyPosition();
                Vector3 middle_pos = new Vector3(role.getGameObject().transform.position.x, end_pos.y, end_pos.z);
                actionManager.moveRole(role.getGameObject(), middle_pos, end_pos, role.move_speed);

                role.getOnLand(land);
                land.addCharacter(role);
            }
            else
            {
                LandControllor land = role.GetLandControllor();
                if (boat.GetEmptyNumber() == -1 || land.getState() != boat.GetBoatSign()) return;

                land.removeCharacter(role.GetName());

                Vector3 end_pos = boat.GetEmptyPosition();
                Vector3 middle_pos = new Vector3(end_pos.x, role.getGameObject().transform.position.y, end_pos.z);
                actionManager.moveRole(role.getGameObject(), middle_pos, end_pos, role.move_speed);

                role.getOnBoat(boat);
                boat.addCharacter(role);
            }
            user_gui.status = Check();

            judge.judge(user_gui.status);
        }

        public void Restart()
        {
            start_land.Reset();
            end_land.Reset();
            boat.Reset();
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Reset();
            }

        }

        public int Check()
        {
            int start_priest = (start_land.getCharacterNum())[0];
            int start_devil = (start_land.getCharacterNum())[1];
            int end_priest = (end_land.getCharacterNum())[0];
            int end_devil = (end_land.getCharacterNum())[1];

            if (end_priest + end_devil == 6)
                return 2;

            int[] boat_role_num = boat.getCharacterNumber();
            if (boat.GetBoatSign() == 1)
            {
                start_priest += boat_role_num[0];
                start_devil += boat_role_num[1];
            }
            else
            {
                end_priest += boat_role_num[0];
                end_devil += boat_role_num[1];
            }
            if (start_priest > 0 && start_priest < start_devil)
            {
                return 1;
            }
            if (end_priest > 0 && end_priest < end_devil)
            {
                return 1;
            }
            return 0;
        }

        public int [] getStateInfo()
        {
            int[] arr = new int[5];
            arr[0] = (start_land.getCharacterNum())[0];
            arr[1] = (start_land.getCharacterNum())[1];
            arr[2] = (end_land.getCharacterNum())[0];
            arr[3] = (end_land.getCharacterNum())[1];
            arr[4] = (boat.GetBoatSign() == 1) ? 0 : 1;
            return arr;
        }


    }
