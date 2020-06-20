using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.MLAgents.Sensors;

public class GameBoard : MonoBehaviour
{
    public Cell Cell;

    List<Cell> _cells;
    public int Size;

    bool _hasInitializedBoard;
    int _nextPlayerId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InitializeBoard(int size)
    {
        if (_hasInitializedBoard)
        {
            // check is same size
            return;
        }
        _cells = new List<Cell>();
        Vector3 position = this.transform.position;
        position.x -= ((float)size-1) / 2f;
        position.y = 0f;
        position.z += ((float)size-1) / 2f;
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                var cell = GameObject.Instantiate(Cell, position, this.transform.rotation);
                cell.transform.parent = this.transform;
                position.x += 1f;
                _cells.Add(cell);
                cell.Action = _cells.IndexOf(cell);
                cell.Row = row;
                cell.Column = column;
            }
            position.x -= (float)size;
            position.z -= 1f;
        }
        ResetBoard();
        _hasInitializedBoard = true;
    }
    public void ResetBoard()
    {
        foreach (var cells in _cells)
        {
            cells.TeamId = 0;
        }
        _nextPlayerId = 1;
    }

    public void CollectObservationsForPlayer(VectorSensor sensor, int playerId)
    {
        if (playerId == 1)
        {
            DoCollectObservationsForPlayer(sensor, 1);
            DoCollectObservationsForPlayer(sensor, 2);
        }
        else if (playerId == 2)
        {
            DoCollectObservationsForPlayer(sensor, 2);
            DoCollectObservationsForPlayer(sensor, 1);
        }
        else
        {
            throw new System.ArgumentException($"{nameof(playerId)}");
        }
    }
    void DoCollectObservationsForPlayer(VectorSensor sensor, int playerId)
    {
        foreach (var cell in _cells)
        {
            bool status = cell.TeamId == playerId;
            sensor.AddObservation(status);
        }
    }

    public List<Cell> GetFreeSpaces()
    {
        var freeSpaces = _cells
            .Where(x=>x.TeamId == 0)
            .ToList();
        return freeSpaces;
    }


    public void TakeAction(int action, int playerId)
    {
        var cell = _cells.First(x=>x.Action == action);
        cell.TeamId = playerId;
        _nextPlayerId = playerId == 1 ? 2 : 1;
    }

    public bool HasEnded()
    {
        var freeSpace = _cells.FirstOrDefault(x=>x.TeamId == 0);
        return freeSpace == null;
    }

    public bool ShouldRequestDecision(int playerId)
    {
        // TODO if human, return false
        if (HasEnded())
            return false;
        return _nextPlayerId == playerId;
    }
}
