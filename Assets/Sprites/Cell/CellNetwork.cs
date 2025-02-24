using System;
using UnityEngine;

namespace Network
{
    public class CellNetwork
    {
        private CellNode _mainCellNode;
        public CellNode MainCellNode{ get => _mainCellNode; }

        public CellNetwork(CellStats cellStats, GameObject CellGO){

            _mainCellNode = new CellNode(cellStats, CellGO);

        }

        //We will add the "cells" based on which cell bounded them.
        public void Add(string parentCellID, CellStats cellToBeBoundStat, GameObject CellGO, float boundLength){

            CellNode? parentCellNode = _searchRecursive(_mainCellNode, parentCellID);

            if(parentCellNode != null){

                parentCellNode.Add(cellToBeBoundStat, CellGO, boundLength);

            }

        }

        public void Remove(string taretCellID){

            CellNode? parentCellNode = _searchRecursive(_mainCellNode, taretCellID);

            if(parentCellNode != null){

                parentCellNode.Remove(taretCellID);

            }

        }

        public void Display(CellNode currNode, ref string displayString){


            for (int i = 0; i < currNode.Nodes.Length; i++)
            {
                
                if(currNode.Nodes[i]?.NextNode != null){

                    Display(currNode.Nodes[i]?.NextNode, ref displayString);

                }

            }

            displayString+= currNode.CellGO.name +  "-";

        }


        /// <summary>
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="targetCellID"></param>
        /// <returns>Gets parent cell.</returns>
        private CellNode _searchRecursive(CellNode currNode, string targetCellID){

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

    /// <summary>
    /// Used in only CellNetwork.
    /// </summary>
    public class CellNode{

        public Bound?[] Nodes = new Bound?[3]; // based on ternary tree
        public CellStats Stats;
        public GameObject CellGO; //Might add move script here instead of GO.

        public CellNode(CellStats cellStats, GameObject CellGO){

            Stats = cellStats;
            this.CellGO = CellGO;

        }

        /// <summary>
        /// Creates cell and bound and adds them to parent nodes.
        /// </summary>
        /// <param name="cellToBeBoundStat"></param>
        /// <param name="CellGO"></param>
        /// <param name="boundLength"></param>
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
