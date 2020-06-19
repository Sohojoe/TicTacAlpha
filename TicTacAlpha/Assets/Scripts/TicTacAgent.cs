using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using SpawnableEnvs;

public class TicTacAgent : Agent
{
    // Start is called before the first frame update

    [Header("Settings")]
    [Tooltip("The number of rows and columns in the grid.")]
    public int Size = 3;
    [Tooltip("How many units in a row are needed to win the game.")]
    public int WinCount = 3;
    [Tooltip("Pie rule allows the 2nd player to take the position of the 1st players 1st go")]
    public bool PieRule = false;


    SpawnableEnv _spawnableEnv;
    GameBoard _gameBoard;


    override public void Initialize() 
    {
        // grab access to objects
        _spawnableEnv = GetComponentInParent<SpawnableEnv>();
        _gameBoard = _spawnableEnv.GetComponentInChildren<GameBoard>();
        // _mocapController = _spawnableEnv.GetComponentInChildren<MocapController>();

        // to do, error check the behavior paramaters
        
        _gameBoard.InitializeBoard(Size);
    }

    override public void CollectObservations(VectorSensor sensor)
    {
        
    }

    override public void CollectDiscreteActionMasks(DiscreteActionMasker actionMasker)
    {
        
    }

    override public void OnActionReceived(float[] vectorAction) 
    {
        
    }

    override public void OnEpisodeBegin()
    {
        
    }



}
