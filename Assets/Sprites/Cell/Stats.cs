using System;
using UnityEngine;

public class PlayerStats{

    public Vector2 TotalForcePenalty;
    public float TotalSpeedPenalty;

}

public class CellStats
{
    public string ID; //GUID
    public float Mass; //Different from rigidbody mass.
    public float MoveSpeed;
    public Vector3 MoveVector;

    public CellStats(){

        ID = Guid.NewGuid().ToString();

        //Generate random Mass, MoveSpeed and MoveVector for the object.

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