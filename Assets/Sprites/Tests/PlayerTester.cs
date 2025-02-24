using Player;
using UnityEngine;

namespace Tests
{
    
    public class PlayerTester : MonoBehaviour
    {

        public float Mass; //Different from rigidbody mass.
        public float MoveSpeed;
        public PlayerActions playerActions;
        private void Update() {
            
            _modifySpeedAndMass();

        }

        private void _modifySpeedAndMass(){

            playerActions._cellStats.Mass = Mass;
            playerActions._cellStats.MoveSpeed = MoveSpeed;

        }
        
    }

}

