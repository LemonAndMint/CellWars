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
        public GameObject playerGO;
        public CellNetwork<PlayerNode, PlayerStats> playerCellNetwork;

        public PlayerStats _cellStats; //FOR TESTING PURPOSES. WILL BE DELETED

        void Start()
        {

            try
            {
                
                playerInput.OnKeyInput += Move;
                playerInput.OnMouseInput += playerMovement.Rotate;

                playerCellNetwork = CellNetworkCreater.CreateNetwork(new PlayerStats(), playerGO);

                _cellStats = playerCellNetwork.MainCellNode.Stats;

            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Data);
            }

        }

        public void Move(List<Direction> directions){

            playerMovement.Move(directions, _cellStats.MoveSpeed);

        }

        public void Bound(){



        }

    }

}
