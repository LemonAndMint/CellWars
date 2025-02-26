using System.Collections.Generic;
using Network;
using Player;
using UnityEngine;
using Utils.Direction;

namespace Player
{
    
    public class PlayerActions : MonoBehaviour
    {
        public PlayerInput playerInput;
        public PlayerMovement playerMovement;
        public PlayerStats PlayerCellStats; 
        public GameObject playerGO;

        public GameObject BoundPrefb;

        public CellNetwork<PlayerNode, PlayerStats> playerCellNetwork;

        void Start()
        {

            try
            {
                
                playerInput.OnKeyInput += Move;
                playerInput.OnMouseInput += playerMovement.Rotate;
                playerInput.OnBindInput += Bound;

                playerCellNetwork = CellNetworkCreater.CreateNetwork(PlayerCellStats, playerGO);


            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Data);
            }

        }

        public void Move(List<Direction> directions){

            playerMovement.Move(directions, PlayerCellStats.MoveSpeed);

        }

        public void Bound(CellStats stats){

            //when added bounded cell rigidbody needs to be eliminated.

        }

    }

}
