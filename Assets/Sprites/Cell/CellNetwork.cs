using System;
using UnityEngine;

namespace Network
{
    public class CellNetwork
    {
        public CellNode MainCellNode;

        public CellNetwork(CellStats cellStats, GameObject CellGO){

            MainCellNode = new CellNode(cellStats, CellGO);

        }

        //We will add the "cells" based on their bounding order. Which cell is bounded first would be
        //the nearest left node. 
        public void Add(string currCellID, CellStats cellToBeBoundStat, GameObject CellGO, float boundLength){

            CellNode? targetCellNode = _searchRecursive(MainCellNode, currCellID);

            if(targetCellNode != null){

                targetCellNode.Add(cellToBeBoundStat, CellGO, boundLength);

            }

        }

        private CellNode _searchRecursive(CellNode currNode, string targetCellID){

            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i] != null && currNode.Nodes[i]?.NextNode.Stats.ID != targetCellID){

                    _searchRecursive(currNode.Nodes[i]?.NextNode, targetCellID);

                }
                else{

                    return currNode.Nodes[i]?.NextNode;

                }

            }

            new ArgumentNullException("currNode", "Couldnt find the target Cell!");
            return null; //normally it shouldnt be null.

        }

    }

    public class CellNode{

        public Bound?[] Nodes = new Bound?[3]; // based on ternary tree
        public CellStats Stats;
        public GameObject CellGO; //Might add move script here instead of GO.

        public CellNode(CellStats cellStats, GameObject CellGO){

            Stats = cellStats;
            this.CellGO = CellGO;

        }

        public void Add(CellStats cellToBeBoundStat, GameObject CellGO, float boundLength){

            int unOccipiedIndex = GetNearestEmptyIndex();

            if(unOccipiedIndex != -1){

                CellNode boundedCell = new CellNode(cellToBeBoundStat, CellGO);
                BoundStats boundStats = new BoundStats(boundLength);

                Bound bound = new Bound { 

                    NextNode = boundedCell,
                    BoundStats = boundStats

                };

                Nodes[unOccipiedIndex] = bound;

            }

            
            
        }

        /// <summary>
        /// </summary>
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

    public struct Bound{

        public CellNode NextNode;
        public BoundStats BoundStats;

    }

}
