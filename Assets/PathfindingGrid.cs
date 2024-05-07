using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathfindingGrid
{


    [SerializeField, HideInInspector]
    public GridNode[,,] nodes;

    public Vector3Int _size = new Vector3Int(10, 10, 10);
    public Vector3 _nodeSize = new Vector3(1, 1, 1);

    public void GenerateGrid(Vector3 backBottomLeftCorner, LayerMask obstacleMask)
    {
        nodes = new GridNode[_size.x, _size.y, _size.z];
        Vector3 halfSize = _nodeSize / 2;

        for(int x = 0; x < _size.x; x++)
        {
            for(int y = 0; y < _size.y; y++)
            {
                for(int z = 0; z < _size.z; z++)
                {
                    Vector3 newPos = backBottomLeftCorner + new Vector3(x * _nodeSize.x, y * _nodeSize.y, z * _nodeSize.z);
                    nodes[x, y, z] = new GridNode(newPos);
                    nodes[x, y, z].obstructed = Physics.CheckBox(newPos, halfSize, Quaternion.identity, obstacleMask);
                    nodes[x, y, z].neighbours = GetNeighbours(new Vector3Int(x, y, z));
                }
            }
        }
    }

    private GridNode[] GetNeighbours(Vector3Int coordinates)
    {
        List<GridNode> newNodes = new List<GridNode>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                for(int z = -1; z <= 1; z++)
                {
                    Vector3Int newCoordinates = coordinates + new Vector3Int(x, y, z);

                    if ((x != 0 && y != 0 && z != 0) && newCoordinates.x < _size.x && newCoordinates.x >= 0 && newCoordinates.y < _size.y && newCoordinates.y >= 0 && newCoordinates.z < _size.z && newCoordinates.z >= 0)
                        newNodes.Add(nodes[newCoordinates.x, newCoordinates.y, newCoordinates.z]);
                }
            }
        }

        return newNodes.ToArray();
    }
}

[System.Serializable]
public class GridNode
{
    public GridNode(Vector3 position)
    {
        this.position = position;

    }

    public GridNode[] neighbours;

    public bool obstructed;

    public int currentGCost;
    public int currentHCost;
    public int currentFCost;

    public Vector3 position;


    public GridNode previousNodeInPath;
}
