using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlockPathfinding : MonoBehaviour
{
    [SerializeField]
    private PathfindingGrid grid;

    [SerializeField]
    private Vector3 backBottomLeftGridCorner;
    
    [SerializeField]
    private LayerMask gridObstacleMask;


    public void GenerateGrid()
    {
        grid.GenerateGrid(backBottomLeftGridCorner, gridObstacleMask);
        Debug.Log(grid.nodes.Length);
    }

    private void OnDrawGizmosSelected()
    {
        if(grid == null)
            return;

        if (grid.nodes == null)
            return;

        Gizmos.color = Color.gray;
        for(int x = 0; x < grid.nodes.GetLength(0); x++)
        {
            for(int y = 0; y < grid.nodes.GetLength(1); y++)
            {
                for(int z = 0; z < grid.nodes.GetLength(2); z++)
                {
                    if (grid.nodes[x, y, z].obstructed)
                    {
                        Gizmos.DrawCube(grid.nodes[x, y, z].position, grid._nodeSize);
                    }
                    else
                        Gizmos.DrawWireCube(grid.nodes[x, y, z].position, grid._nodeSize);
                }
            }
        }
    }
}


[CustomEditor(typeof( FlockPathfinding))]
public class FlockPathfindingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUI.BeginChangeCheck();

        if(GUILayout.Button("Generate Grid"))
        {
            FlockPathfinding pathfinding = ( FlockPathfinding)target;
            pathfinding.GenerateGrid();
            
            EditorUtility.SetDirty(pathfinding);
            EditorGUI.EndChangeCheck();
            Undo.RecordObject(pathfinding.gameObject, "Generated new grid");
        }
    }
}
