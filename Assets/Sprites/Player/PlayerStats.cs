using UnityEngine;

public class PlayerStats : Stats{

    private Vector2 _totalForcePenalty = Vector2.zero;
    public Vector2 TotalForcePenalty{ get => _totalForcePenalty; }

    private float _totalSpeedPenalty = 0;
    public float TotalSpeedPenalty{ get => _totalSpeedPenalty; }

    public void AddPenalty(Vector2 TotalForcePenalty, float TotalSpeedPenalty){

        _totalForcePenalty += TotalForcePenalty;
        _totalSpeedPenalty += TotalSpeedPenalty;

    }

    public void RemovePenalty(Vector2 TotalForcePenalty, float TotalSpeedPenalty){

        _totalForcePenalty -= TotalForcePenalty;
        _totalSpeedPenalty -= TotalSpeedPenalty;

    }

}
