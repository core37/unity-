using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
public class UserGUI : MonoBehaviour
{
    public int status = 0;
    private IUserAction userAction;
    GUIStyle style;
    GUIStyle buttonStyle;
    GameState start, end;

    public int leftPriests = 3;
    public int leftDevils = 3;
    public int rightPriests = 0;
    public int rightDevils = 0;
    public bool boat_pos = true;
    private string tips = "";
    void Start()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
        end = new GameState(0, 0, 3, 3, false, null);

    }

    void OnGUI()
    {
        GUIStyle fontstyle = new GUIStyle();
        fontstyle.normal.background = null;
        fontstyle.normal.textColor = new Color(255, 192, 203);
        fontstyle.fontSize = 50;

        style = new GUIStyle()
        {
            fontSize = 16
        };
        style.normal.textColor = new Color(0, 0, 0);
        buttonStyle = new GUIStyle("button")
        {
            fontSize = 16
        };

        if (GUI.Button(new Rect (Screen.width/2 - 50 , Screen.height- 50, 100, 40), "Reset", buttonStyle))
            {
                status = 0;
                userAction.Restart();
            }
        if (status == 1)
        {
            GUI.Label(new Rect (Screen.width/2- 50, 50, 100, 40), "Lose", style);
        }
        else if (status == 2)
        {
            GUI.Label(new Rect (Screen.width/2- 50, 50, 100, 40), "Win", style);
        }
        GUI.Button(new Rect(Screen.width/2 - 150 , 30, 400, 40), tips, style);

        if (GUI.Button(new Rect(Screen.width/2 - 300 , 30, 100, 40), "Tips", buttonStyle))
        {
            int[] arr = userAction.getStateInfo();
            leftPriests = arr[0];
            leftDevils = arr[1];
            rightPriests = arr[2];
            rightDevils = arr[3];

            if (arr[4] == 0) boat_pos = true;
            else boat_pos = false;
            start = new GameState(leftPriests, leftDevils, rightPriests, rightDevils, boat_pos, null);

            GameState temp = GameState.BFS(start, end);


            int p = leftPriests - temp.lp;
            int d = leftDevils - temp.ld;
            if (p < 0) p = -p;
            if (d < 0) d = -d;
            tips = "Move " + p + " priest(s)，" + d
                + " devil(s) to another side.";
        }
    }
}