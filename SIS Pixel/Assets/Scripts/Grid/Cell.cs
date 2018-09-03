using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Cell {

    public int xIndex;
    public int yIndex;

    // Current state of the cell.
    public Unit curUnit;

    // Various colors.
    public Color borderColor;
    public Color highlightColor;
    public Color stateColor;

    // Refernces of the neighbors of the cell.
    public List<Cell> neighbors;


    // Constructor
    public Cell(int i, int j, Unit unit)
    {
        xIndex = i;
        yIndex = j;
        curUnit = unit;
        SetBorderColor();
        SetUnitColor();
        neighbors = new List<Cell>();
    }


    // Set the cell color based on the current state.
    public void SetUnitColor()
    {
        stateColor = curUnit.GetColor();
    }

    public void SetBorderColor()
    {
        borderColor = Color.grey;
        highlightColor = Color.yellow;
    }


    // Prepare for update without updating it.
    public bool PrepareUpdate(bool gameMode)
    {
        if (gameMode)
        {
            int[] aliveNeighborCountArray = new int[4];
            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.GetUnitState(true).IsAlive)
                {
                    if (neighbor.GetUnitState(true).Id == Identifier.w)
                    {
                        aliveNeighborCountArray[0]++;
                    }
                    else if (neighbor.GetUnitState(true).Id == Identifier.g)
                    {
                        aliveNeighborCountArray[1]++;
                    }
                    else if (neighbor.GetUnitState(true).Id == Identifier.r)
                    {
                        aliveNeighborCountArray[2]++;
                    }
                    else if (neighbor.GetUnitState(true).Id == Identifier.b)
                    {
                        aliveNeighborCountArray[3]++;
                    }
                }
            }

            int maxType = aliveNeighborCountArray.Max();
            Identifier majority = (Identifier)Array.IndexOf(aliveNeighborCountArray,maxType);

            if (aliveNeighborCountArray.Sum() < 2 || aliveNeighborCountArray.Sum() > 3)
            {
                SetUnitState(false, false);
            }
            else
            {
                if (maxType >= 2 && maxType <= 3)
                {
                    SetUnitState(false, true, majority);
                }
                else
                {
                    SetUnitState(false, GetUnitState(true).IsAlive);
                }
            }
        }
        else
        {
            int aliveNeighborCount = 0;

            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.GetUnitState(true).IsAlive) aliveNeighborCount++;
            }

            if (aliveNeighborCount < 2 || aliveNeighborCount > 3)
            {
                SetUnitState(false, false);
            }
            else if (aliveNeighborCount == 3)
            {
                SetUnitState(false, true);
            }
            else
            {
                SetUnitState(false, GetUnitState(true).IsAlive);
            }
        }
        return GetUnitState(true) != GetUnitState(false);
    }


    // Get the current/next state of unit.
    public UnitState GetUnitState(bool cur)
    {
        return curUnit.GetState(cur);
    }


    // Set the current/next state of unit.
    // Return true if current state changed.
    public bool SetUnitState(bool cur, bool alive)
    {
        if (cur)
        {
            if (curUnit.SetState(true, alive))
            {
                SetUnitColor();
                return true;
            }
            return false;
        }
        return curUnit.SetState(false, alive);
    }

    // Set the current/next state of unit.
    // Return true if current state changed.
    public bool SetUnitState(bool cur, bool alive, Identifier id)
    {
        if (cur)
        {
            if (curUnit.SetState(true, alive, id))
            {
                SetUnitColor();
                return true;
            }
            return false;
        }
        return curUnit.SetState(false, alive, id);
    }

    // Update the cell state to its next state.
    public void Update()
    {
        if (curUnit.UpdateState()) SetUnitColor();
    }
}
