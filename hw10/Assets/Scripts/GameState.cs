using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // character amount
    public int lp;
    public int rp;
    public int ld;
    public int rd;

    public bool pos;       //true for right，false for left
    public GameState parent_state;

    //constructors
    public GameState(int lp, int ld, int rp, int rd, bool bp, GameState gs)
    {
        this.lp = lp;
        this.rp = rp;
        this.ld = ld;
        this.rd = rd;
        this.pos = bp;
        this.parent_state = gs;

    }
    public GameState(GameState temp)
    {
        this.lp = temp.lp;
        this.rp = temp.rp;
        this.ld = temp.ld;
        this.rd = temp.rd;
        this.pos = temp.pos;
        this.parent_state = temp.parent_state;
    }


    public static bool operator ==(GameState lhs, GameState rhs)
    {
        return (lhs.lp == rhs.lp && lhs.rp == rhs.rp &&
        lhs.ld == rhs.ld && lhs.rd == rhs.rd &&
        lhs.pos == rhs.pos);
    }

    public static bool operator !=(GameState lhs, GameState rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj.GetType().Equals(this.GetType()) == false)
        {
            return false;
        }
        GameState temp = (GameState)obj;
        return this.lp.Equals(temp.lp) && this.rp.Equals(temp.rp) && this.rd.Equals(temp.rd)
            && this.ld.Equals(temp.ld) && this.pos.Equals(temp.pos);
    }

    public override int GetHashCode()
    {
        return this.lp.GetHashCode() + this.rp.GetHashCode() + this.ld.GetHashCode()
           + this.rd.GetHashCode() + this.pos.GetHashCode();
    }

    public bool isValid() //judge if state is valid
    {
        return ((lp == 0 || lp >= ld) && (rp == 0 || rp >= rd));
    }




    public static GameState BFS(GameState start, GameState end)
    {
        Queue<GameState> queue = new Queue<GameState>(); //store state
        GameState temp = new GameState(start.lp, start.ld, start.rp, start.rd, start.pos, null);
        queue.Enqueue(temp);



        while (queue.Count > 0)
        {
            temp = queue.Peek();

            if (temp == end)
            {
                while (temp.parent_state != start)
                {
                    temp = temp.parent_state;
                }
                return temp;
            }
            queue.Dequeue();

            if (temp.pos)
            {

                if (temp.lp > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = false;
                    next.lp--;
                    next.rp++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }
                if (temp.ld > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = false;
                    next.ld--;
                    next.rd++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.ld > 0 && temp.lp > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = false;
                    next.ld--;
                    next.rd++;
                    next.lp--;
                    next.rp++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.lp > 1)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = false;
                    next.lp -= 2;
                    next.rp += 2;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.ld > 1)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = false;
                    next.ld -= 2;
                    next.rd += 2;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }
            }


            else

            {

                if (temp.rp > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = true;
                    next.rp--;
                    next.lp++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.rd > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = true;
                    next.rd--;
                    next.ld++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.rd > 0 && temp.rp > 0)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = true;
                    next.rd--;
                    next.ld++;
                    next.rp--;
                    next.lp++;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.rd > 1)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = true;
                    next.rd -= 2;
                    next.ld += 2;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                if (temp.rp > 1)
                {
                    GameState next = new GameState(temp);
                    next.parent_state = new GameState(temp);
                    next.pos = true;
                    next.rp -= 2;
                    next.lp += 2;
                    if (next.isValid() && !queue.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }
            }
        }
        return null;
    }

}
