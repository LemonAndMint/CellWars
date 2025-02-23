using Network;
using Player;
using UnityEngine;

namespace Player
{
    
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerInput playerInput;
        public Movement playerMovement;
        public GameObject playerGO;
        public CellNetwork playerCellNetwork;

        void Start()
        {

            try
            {
                
                playerInput.OnKeyInput += playerMovement.Move;
                playerCellNetwork = new CellNetwork(new CellStats(), playerGO);

            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Data);
            }

        }

    }

}
