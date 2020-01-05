using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;


namespace myGame
{
    public interface ISceneController
    {
        void LoadResources();
    }
    public interface IUserAction
    {
        void MoveBoat();
        void Restart();
        void MoveRole(CharacterControllor role);
        int Check();
        int[] getStateInfo();
    }

    public class SSDirector : System.Object
    {
        private static SSDirector _instance;
        public ISceneController CurrentSceneController { get; set; }
        public static SSDirector GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

  


}
