using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using UnityEngine;

public class NetworkTester : MonoBehaviour
{
    private CellNetwork cellNetwork;

    public List<GameObject> cells; //first one is main cell
    private List<CellStats> cellStats; //normally we wouldnt need to hold them in a list but for testing purposes, we will.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _addRandom();
        
    }

    private void _addLinear(){

        cellStats = new List<CellStats>();

        if(cells.Count <= 0 || cells == null || cellStats == null){

            new Exception("Cells does not contain anything!");
            return;
        }

        float randomBoundLength = -1f;

        CellStats stats = new CellStats();
        cellNetwork = new CellNetwork(stats, cells.First());

        cellStats.Add(stats);

        string add = cells.First().name + "++";


        for (int i = 1; i < cells.Count; i++)
        {

            randomBoundLength = UnityEngine.Random.Range(0.25f, 5f);
            stats = new CellStats();
            cellStats.Add(stats);
            
            cellNetwork.Add(cellStats[i - 1].ID, stats, cells[i], randomBoundLength);

            add += cells[i].name + "++";
        
        }

        string add2 = "**";

        cellNetwork.Display(cellNetwork.MainCellNode, ref add2);

        Debug.Log(add);
        Debug.Log("//////");
        Debug.Log(add2);

    }

    private void _addRandom(){

        cellStats = new List<CellStats>();

        if(cells.Count <= 0 || cells == null || cellStats == null){

            new Exception("Cells does not contain anything!");
            return;
        }

        float randomBoundLength = -1f;

        CellStats stats = new CellStats();
        cellNetwork = new CellNetwork(stats, cells.First());

        cellStats.Add(stats);

        string add = cells.First().name + "++";


        for (int i = 1; i < cells.Count; i++)
        {

            randomBoundLength = UnityEngine.Random.Range(0.25f, 5f);
            stats = new CellStats();
            cellStats.Add(stats);

            int randomIndex = UnityEngine.Random.Range(0, cellStats.Count);
            
            cellNetwork.Add(cellStats[randomIndex].ID, stats, cells[i], randomBoundLength);

            add += cells[i].name + "++";
        
        }

        string add2 = "**";

        cellNetwork.Display(cellNetwork.MainCellNode, ref add2);

        Debug.Log(add);
        Debug.Log("//////");
        Debug.Log(add2);

    }
    
}
