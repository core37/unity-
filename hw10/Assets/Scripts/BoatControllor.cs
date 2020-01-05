using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;

public class BoatControllor
{
    GameObject boat;
    Vector3[] start_empty_pos;
    Vector3[] end_empty_pos;
    readonly Vector3 startPos = new Vector3(4f, 0, 0);
    readonly Vector3 endPos = new Vector3(-4f, 0, 0);
    ClickGUI ClickGUI;
    int boat_sign = 1;
    CharacterControllor[] obj = new CharacterControllor[2];

    public float move_speed = 20;
    public GameObject getGameObject() { return boat; }

    public BoatControllor()
    {
        boat = Object.Instantiate(Resources.Load("prefabs/boat", typeof(GameObject)), startPos, Quaternion.identity) as GameObject;
        boat.name = "boat";
        ClickGUI = boat.AddComponent(typeof(ClickGUI)) as ClickGUI;
        ClickGUI.SetBoat(this);
        start_empty_pos = new Vector3[] { new Vector3(3.5F, 0.8f, 0), new Vector3(4.5F, 0.8f, 0) };
        end_empty_pos = new Vector3[] { new Vector3(-4.5F, 0.8f, 0), new Vector3(-3.5F, 0.8f, 0) };
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
                return false;
        }
        return true;
    }

    public Vector3 BoatMoveToPosition()
    {
        boat_sign = -boat_sign;
        if (boat_sign == -1)
        {
            return endPos;
        }
        else
        {
            return startPos;
        }
    }

    public int GetBoatSign() { return boat_sign; }

    public CharacterControllor removeCharacter(string role_name)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null && obj[i].GetName() == role_name)
            {
                CharacterControllor role = obj[i];
                obj[i] = null;
                return role;
            }
        }
        return null;
    }

    public int GetEmptyNumber()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 GetEmptyPosition()
    {
        Vector3 pos;
        if (boat_sign == -1)
            pos = end_empty_pos[GetEmptyNumber()];
        else
            pos = start_empty_pos[GetEmptyNumber()];
        return pos;
    }

    public void addCharacter(CharacterControllor role)
    {
        obj[GetEmptyNumber()] = role;
    }

    public GameObject GetBoat() { return boat; }

    public void Reset()
    {
        if (boat_sign == -1)
            BoatMoveToPosition();
        boat.transform.position = startPos;
        obj = new CharacterControllor[2];
    }

    public int[] getCharacterNumber()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] == null)
                continue;
            if (obj[i].GetSign() == 0)
                count[0]++;
            else
                count[1]++;
        }
        return count;
    }
}


