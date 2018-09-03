using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit {

    // Current state of the unit.
    public UnitState curState;


    // Upcoming state of the unit.
    public UnitState nextState;


    // Constructor.
    public Unit(Identifier id, bool alive)
    {
        curState = new UnitState(alive, id);
        nextState = new UnitState(curState);
    }


    // Get Color.
    public Color GetColor() {
       return curState.GetColor();  
    }

    // Get current/next state.
    public UnitState GetState(bool cur)
    {
        if (cur) return curState;
        return nextState;
    }


    // Set current/next state.
    public bool SetState(bool cur, bool alive)
    {
        UnitState temp = new UnitState(alive, curState.Id);
        if (cur)
        {
            if (!curState.Equals(temp))
            {
                curState = new UnitState(temp);
                return true;
            }
            return false;
        }
        nextState = new UnitState(temp);
        return true;      
    }


    // Set current/next state.
    public bool SetState(bool cur, bool alive, Identifier id)
    {
        UnitState temp = new UnitState(alive, id);
        if (cur)
        {
            if (!curState.Equals(temp))
            {
                curState = new UnitState(temp);
                return true;
            }
            return false;
        }
        nextState = new UnitState(temp);
        return true;
    }


    // Move to the next state if different.
    public bool UpdateState()
    {
        if (!curState.Equals(nextState))
        {
            curState = new UnitState(nextState);
            return true;
        }
        return false;
    }

};
