using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;
public class Judge 
{
    private IUserAction userAction;

    void Start()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;


    }
    // Start is called before the first frame update
    public void judge(int sign)
    {
        if (sign == 1)
        {
            Debug.Log("Game Scene End.");
            userAction.Restart();
        }

    }
}
