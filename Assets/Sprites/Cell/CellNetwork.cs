using System;
using Network;
using Unity.VisualScripting;
using UnityEngine;

namespace Network
{
    public class CellNetwork<T, U> where T : Node 
    {
        //Main cell could be a player or a bot. If its player, it will have PlayerStats,
        //if not it will have CellStats.
        private T _mainCellNode;
        public T MainCellNode{ get => _mainCellNode; }

        private float _maxAllowedBoundLength;
        private BoundManager _boundManager;

        public delegate T NodeFactory(U cellStats, GameObject cellGO);

        /// <summary>
        /// Unless you know what you are doing, dont use this. Im telling you future me XOXO.
        /// Use "CellNetworkCreater" functions.
        /// </summary>
        public CellNetwork(U cellStats, GameObject CellGO, float maxAllowedBoundLength, BoundManager boundManager, NodeFactory factory){

            _mainCellNode = factory(cellStats, CellGO);
            _maxAllowedBoundLength = maxAllowedBoundLength;
            _boundManager = boundManager;

        }

        /*//We will add the "cells" based on which cell bounded them. Player cells cannot be added
        //to another systems so we are directly using CellStats.
        //OBSOLUTE!
        public void Add(GameObject parentCellGO, CellStats cellToBeBoundStat, GameObject CellGO){

            Node? parentCellNode = _searchRecursive(_mainCellNode, parentCellID);

            if(parentCellNode != null){

                parentCellNode.Add(cellToBeBoundStat, CellGO, _boundManager.Bound);

            }

        }*/

        public GameObject Add(CellStats cellToBeBoundStat, GameObject CellGO){

            Node? parentCellNode = _searchNearestRecursive(MainCellNode, CellGO.transform.position);

            if(parentCellNode != null){

                parentCellNode.Add(cellToBeBoundStat, CellGO, _boundManager.Bound);
                return parentCellNode.CellGO;

            }

            return null;

        }

        public bool Remove(GameObject goToBeUnbound){

            Node? parentCellNode = _searchRecursive(_mainCellNode, goToBeUnbound);

            if(parentCellNode != null){

                parentCellNode.Remove(goToBeUnbound, _boundManager.Unbound);
                return true;

            }

            return false;

        }

        public bool RemoveBound(GameObject boundGO){

            Node? parentCellNode = _searchRecursive(_mainCellNode, boundGO);

            if(parentCellNode != null){

                parentCellNode.Remove(boundGO, _boundManager.Unbound);
                return true;

            }

            return false;

        }


        public string DisplayAll(){

            string displayString = "";
            int level = -1;

            Display(MainCellNode, ref displayString, ref level);

            return displayString;

        }

        public void Display(Node currNode, ref string displayString, ref int level){

            level += 1;
            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i]?.NextNode != null){

                    Display(currNode.Nodes[i]?.NextNode, ref displayString, ref level);

                }

            }

            displayString+= currNode.CellGO.name + "  " + level +"-";

        }

        /// <summary>
        /// Needs to start at max allowed distance of bound.
        /// </summary>
        /// <returns>Gets parent cell.</returns>
        private Node _searchNearestRecursive(Node currNode, Vector2 targetPoint){
            
            //Not the perfect version of adding nodes but in this stage im not going to try perfecting it.

            float distance = Vector2.Distance(currNode.CellGO.transform.position, targetPoint);
                
            if(distance <= _maxAllowedBoundLength && currNode.GetNearestEmptyIndex() != -1){

                return currNode; // may "add" from here.

            }

            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i]?.NextNode != null){

                    Node node = _searchNearestRecursive(currNode.Nodes[i]?.NextNode, targetPoint);

                    if(node != null){

                        return node; // which is currNode from prev recursion.

                    }
                    
                }

            }

            return null;

        }


        /// <returns>Gets parent cell.</returns>
        private Node _searchRecursive(Node currNode, GameObject boundGO){

            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i]?.NextNode.CellGO != boundGO){

                    if(currNode.Nodes[i]?.NextNode != null){

                        Node node = _searchRecursive(currNode.Nodes[i]?.NextNode, boundGO);

                        if(node != null){// which is currNode from prev recursion.

                            return node;

                        }
                        
                    }

                }
                else{
                    
                    return currNode;

                }

            }

            return null;

        }

        /// <returns>Gets parent cell.</returns>
        /*private Node _searchRecursive<V>(Node currNode, V objectThatChecked, Func<V, bool> boolStatement){ //#DO LATER

            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(boolStatement.Invoke(objectThatChecked)){

                    if(currNode.Nodes[i]?.NextNode != null){

                        Node node = _searchRecursive(currNode.Nodes[i]?.NextNode, objectThatChecked, boolStatement);

                        if(node != null){// which is currNode from prev recursion.

                            return node;

                        }
                        
                    }

                }
                else{
                    
                    return currNode;

                }

            }

            return null;

        }*/

    }

    public static class CellNetworkCreater
    {

        public static CellNetwork<PlayerNode, PlayerStats> CreateNetwork(PlayerStats cellStats, GameObject cellGO, float maxAllowedBoundLength, BoundManager boundManager){

            CellNetwork<PlayerNode, PlayerStats> network = new CellNetwork<PlayerNode, PlayerStats>(
                cellStats, 
                cellGO, 
                maxAllowedBoundLength,
                boundManager,
                (s, go) => new PlayerNode(s, go)
            );

            return network;

        }

        public static CellNetwork<CellNode, CellStats> CreateNetwork(CellStats cellStats, GameObject cellGO, float maxAllowedBoundLength, BoundManager boundManager){

            CellNetwork<CellNode, CellStats> network = new CellNetwork<CellNode, CellStats>(
                cellStats, 
                cellGO, 
                maxAllowedBoundLength,
                boundManager,
                (s, go) => new CellNode(s, go)
            );

            return network;

        }

    } 

    /// <summary>
    /// Used in only CellNetwork.
    /// </summary>
    public class Node{

        public Bound?[] Nodes = new Bound?[3]; // based on ternary tree
        public GameObject CellGO; 
        public Stats Stats;

        public Node(GameObject CellGO){

            this.CellGO = CellGO;

        }

        /// <summary>
        /// Creates cell and bound and adds them to parent nodes.
        /// </summary>
        public bool Add(CellStats cellToBeBoundStat, GameObject toBeBoundedCellGO, Func<Transform,Transform,GameObject> boundFunc){

            int unOccipiedIndex = GetNearestEmptyIndex();

            if(unOccipiedIndex != -1){

                CellNode boundedCell = new CellNode(cellToBeBoundStat, toBeBoundedCellGO);
                
                GameObject boundGO = boundFunc?.Invoke(CellGO.transform.GetChild(0), //we assume that this is the connection gameobject.
                                                       toBeBoundedCellGO.transform);

                Bound bound = new Bound { 

                    NextNode = boundedCell,
                    BoundGO = boundGO,

                };

                Nodes[unOccipiedIndex] = bound;

                return true;

            }

            return false;
            
        }

        //We will make the remove operation from parent cell and remove the taret child cell.
        public void Remove(GameObject goToBeUnbound, Action<Bound?> unboundFunc){ 

           for (int i = 0; i < Nodes.Length; i++)
            {
                
                if(Nodes[i]?.NextNode.CellGO == goToBeUnbound){

                    unboundFunc?.Invoke(Nodes[i]);
                    Nodes[i] = null;
                    return;

                }

            }

            new Exception("Couldnt delete the cell. Mismatched id");

        }

        //We will make the remove operation from parent cell and remove the taret child cell.
        public void RemoveBound(GameObject boundGO, Action<Bound?> unboundFunc){ 

           for (int i = 0; i < Nodes.Length; i++)
            {
                
                if(Nodes[i]?.BoundGO == boundGO){

                    unboundFunc?.Invoke(Nodes[i]);
                    Nodes[i] = null;
                    return;

                }

            }

            new Exception("Couldnt delete the cell. Mismatched id");

        }

        /// <returns>"-1" if all Nodes are occupied.</returns>
        public int GetNearestEmptyIndex(){

            for (int i = 0; i < Nodes.Length; i++)
            {
                
                if(Nodes[i] == null){

                    return i;

                }
            }

            return -1;

        }

    }

    public class CellNode : Node
    {
        public new CellStats Stats;

        public CellNode(CellStats cellStats, GameObject CellGO) : base(CellGO){

            Stats = cellStats;

        }

    }

    public class PlayerNode : Node
    {

        public new PlayerStats Stats;

        public PlayerNode(PlayerStats cellStats, GameObject CellGO) : base(CellGO){

            Stats = cellStats;

        }

    }

    public struct Bound{

        public CellNode NextNode;
        public GameObject BoundGO;

    }

}
