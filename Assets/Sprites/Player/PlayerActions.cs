using System.Collections.Generic;
using Network;
using UnityEngine;
using Utils.Direction;

namespace Player
{
    
    public class PlayerActions : MonoBehaviour
    {
        public PlayerInput playerInput;
        public PlayerMovement playerMovement;
        public PlayerStats PlayerCellStats; 
        public BoundManager boundManager;
        public Transform connectionTrans;
        public GameObject playerGO;

        public GameObject testgo;


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

            Bound(testgo);

        }

        public void Move(List<Direction> directions){

            playerMovement.Move(directions, PlayerCellStats.MoveSpeed);

        }

        public void Bound(GameObject goToBeBound){

            boundManager.Bound(connectionTrans, goToBeBound.transform);

        }

    }

}
