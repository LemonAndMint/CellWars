using System;
using UnityEngine;

public class Stats{

    public string ID; //GUID
    public float Mass; //Different from rigidbody mass.
    public float MoveSpeed;

     public Stats(){

        ID = Guid.NewGuid().ToString();

        //Generate random Mass, MoveSpeed and MoveVector for the object.

    }

}

public class CellStats : Stats
{
    public Vector3 MoveVector;

    public CellStats() : base(){


    }

}

public class PlayerStats : Stats{

    public Vector2 TotalForcePenalty;
    public float TotalSpeedPenalty;

    public PlayerStats() : base(){


    }

}

public class BoundStats
{
    public float Length;
    public float MaxBreakingForce;

    public BoundStats(float Length){

        //Generate random MaxBreakingForce for the bound and according to that change the sprite ccolor.
        //Length will be based on actual bond length so it wont be random.

    }

}