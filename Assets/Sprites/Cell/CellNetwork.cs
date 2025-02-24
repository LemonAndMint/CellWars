using System;
using UnityEngine;

namespace Network
{
    public class CellNetwork<T, U> where T : Node 
    {
        //Main cell could be player or a bot. If its player, it will have PlayerStats,
        //if not it will have CellStats.
        private T _mainCellNode;
        public T MainCellNode{ get => _mainCellNode; }

        public delegate T NodeFactory(U cellStats, GameObject cellGO);

        /// <summary>
        /// Unless you know what you are doing, dont use this. Im telling you future me XOXO.
        /// </summary>
        public CellNetwork(U cellStats, GameObject CellGO, NodeFactory factory){

            _mainCellNode = factory(cellStats, CellGO);

        }

        //We will add the "cells" based on which cell bounded them. Player cells cannot be added
        //another systems so we are using directly CellStats.
        public void Add(string parentCellID, CellStats cellToBeBoundStat, GameObject CellGO, float boundLength){

            Node? parentCellNode = _searchRecursive(_mainCellNode, parentCellID);

            if(parentCellNode != null){

                parentCellNode.Add(cellToBeBoundStat, CellGO, boundLength);

            }

        }

        public void Remove(string taretCellID){

            Node? parentCellNode = _searchRecursive(_mainCellNode, taretCellID);

            if(parentCellNode != null){

                parentCellNode.Remove(taretCellID);

            }

        }

        public void Display(Node currNode, ref string displayString){


            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i]?.NextNode != null){

                    Display(currNode.Nodes[i]?.NextNode, ref displayString);

                }

            }

            displayString+= currNode.CellGO.name +  "-";

        }


        /// <returns>Gets parent cell.</returns>
        private Node _searchRecursive(Node currNode, string targetCellID){

            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Stats.ID != targetCellID){

                    if(currNode.Nodes[i]?.NextNode != null){

                        return _searchRecursive(currNode.Nodes[i]?.NextNode, targetCellID);
                        
                    }

                }
                else{
                    
                    return currNode;

                }

            }

            return null;

        }

    }

    public static class CellNetworkCreater
    {

        /// <summary>
        /// Use for Player main networks.
        public static CellNetwork<PlayerNode, PlayerStats> CreateNetwork(PlayerStats cellStats, GameObject cellGO){

            CellNetwork<PlayerNode, PlayerStats> network = new CellNetwork<PlayerNode, PlayerStats>(
                cellStats, 
                cellGO, 
                (s, go) => new PlayerNode(s, go)
            );

            return network;

        }

        public static CellNetwork<CellNode, CellStats> CreateNetwork(CellStats cellStats, GameObject cellGO){

            CellNetwork<CellNode, CellStats> network = new CellNetwork<CellNode, CellStats>(
                cellStats, 
                cellGO, 
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
        public bool Add(CellStats cellToBeBoundStat, GameObject CellGO, float boundLength){

            int unOccipiedIndex = GetNearestEmptyIndex();

            if(unOccipiedIndex != -1){

                CellNode boundedCell = new CellNode(cellToBeBoundStat, CellGO);
                BoundStats boundStats = new BoundStats(boundLength);

                Bound bound = new Bound { 

                    NextNode = boundedCell,
                    BoundStats = boundStats

                };

                Nodes[unOccipiedIndex] = bound;

                return true;

            }

            return false;
            
        }

        //We will make the remove operation from parent cell and remove the taret child cell.
        public void Remove(string id){ 

           for (int i = 0; i < Nodes.Length; i++)
            {
                
                if(Nodes[i]?.NextNode.Stats.ID == id){

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
        public BoundStats BoundStats;

    }

}
