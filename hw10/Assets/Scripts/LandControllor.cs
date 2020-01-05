using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;
using chCtrl;

public class LandControllor
{
    GameObject land;
    readonly Vector3 startPos = new Vector3(8f, 0, 0); //start position
    readonly Vector3 endPos = new Vector3(-8f, 0, 0); //end position
    Vector3[] positions;
    int state;
    CharacterControllor[] obj = new CharacterControllor[6];

    public LandControllor(string land_mark)
    {
        positions = new Vector3[] {new Vector3(5.5F,1.2f,0f), new Vector3(6.5F,1.2f,0),
                new Vector3(7.5F,1.2f,0),
                new Vector3(8.5F,1.2f,0), new Vector3(9.5F,1.2f,0),
                new Vector3(10.5F,1.2f,0)
        };
        if (land_mark == "start")
        {
            land = Object.Instantiate(Resources.Load("prefabs/Bank", typeof(GameObject)), startPos, Quaternion.identity) as GameObject;
            state = 1;
        }
        else
        {
            land = Object.Instantiate(Resources.Load("prefabs/Bank", typeof(GameObject)), endPos, Quaternion.identity) as GameObject;
            state = -1;
        }
    }

    public int GetEmptyNumber()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] == null)
                return i;
        }
        return -1;
    }

    public int getState() { return state; }

    public Vector3 GetEmptyPosition()
    {
        Vector3 pos = positions[GetEmptyNumber()];
        pos.x = state * pos.x;
        return pos;
    }

    public void addCharacter(CharacterControllor role)
    {
        obj[GetEmptyNumber()] = role;
    }

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

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
            {
                if (obj[i].GetSign() == 0)
                    count[0]++;
                else
                    count[1]++;
            }
        }
        return count;
    }

    public void Reset()
    {
        obj = new CharacterControllor[6];
    }
}



