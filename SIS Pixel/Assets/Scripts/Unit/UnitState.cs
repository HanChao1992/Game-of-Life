using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Identifier { w, g, r, b }

public class UnitState {

    // Is unit alive?
    public bool IsAlive { get; set; }


    // Identifier of the unit.
    public Identifier Id { get; set; }


    // Constructor.
    public UnitState(bool alive, Identifier id)
    {
        IsAlive = alive;
        Id = id;
    }

    // Copy Constructor.
    public UnitState(UnitState otherUnitState)
    {
        IsAlive = otherUnitState.IsAlive;
        Id = otherUnitState.Id;
    }


    // Override equals.
    public override bool Equals(object obj)
    {
        UnitState other = obj as UnitState;
        if (other == null) return false;

        if (IsAlive != other.IsAlive) return false;
        if (Id != other.Id) return false;

        return true;
    }


    // Override GetHashCode().
    public override int GetHashCode()
    {
        int hashIsAlive = IsAlive.GetHashCode();
        int hashIdetifer = Id.GetHashCode();

        return hashIsAlive ^ hashIdetifer;
    }


    // Get Color.
    public Color GetColor()
    {
        if (!IsAlive) return Color.black;
        switch (Id)
        {
            case Identifier.w:
                return Color.white;
            case Identifier.b:
                return Color.blue;
            case Identifier.r:
                return Color.red;
            case Identifier.g:
                return Color.green;
            default:
                return Color.white;
        }
    }
}
