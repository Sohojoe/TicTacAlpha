using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameBoard : MonoBehaviour
{
    public int[] CellStates;
    public GameObject Cell;

    List<List<GameObject>> _cells;
    public int Size;
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
        CellStates = Enumerable.Range(0, size*size).Select(x=>0).ToArray();
        _cells = new List<List<GameObject>>();
        Vector3 position = this.transform.position;
        position.x -= ((float)size-1) / 2f;
        position.y = 0f;
        position.z += ((float)size-1) / 2f;
        for (int x = 0; x < size; x++)
        {
            var row = new List<GameObject>();
            for (int z = 0; z < size; z++)
            {
                var cell = GameObject.Instantiate(Cell, position, this.transform.rotation);
                cell.transform.parent = this.transform;
                position.x += 1f;
                row.Add(cell);
            }
            position.x -= (float)size;
            position.z -= 1f;
            _cells.Add(row);
        }
    }
}
