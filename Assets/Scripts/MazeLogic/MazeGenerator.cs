using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _mazeCellPrefab;
    [SerializeField] private int _mazeWeight;
    [SerializeField] private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    private void Start()
    {
        _mazeGrid = new MazeCell[_mazeWeight, _mazeDepth];

        for (int x = 0; x < _mazeWeight; x++)
            for (int z = 0; z < _mazeDepth; z++)
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);

        GenerateMaze();
    }

    private void GenerateMaze()
    {
        Stack<Vector2Int> visitedMaze = new Stack<Vector2Int>();

        var start = new Vector2Int(0, 0);
        _mazeGrid[start.x, start.y].Visit();

        visitedMaze.Push(start);

        while (visitedMaze.Count > 0)
        {
            Vector2Int current = visitedMaze.Peek();
            List<Vector2Int> unvisited = GetUnvisitedNeighbors(current);

            if (unvisited.Count == 0)
            {
                visitedMaze.Pop();
                continue;
            }


            Vector2Int next = unvisited[UnityEngine.Random.Range(0, unvisited.Count)];
            ClearWalls(current, next);
            _mazeGrid[next.x, next.y].Visit();
            visitedMaze.Push(next);
        }
    }

    private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        var neighbors = new List<Vector2Int>();

        if (cell.x + 1 < _mazeWeight && !_mazeGrid[cell.x + 1, cell.y].IsVisited)
            neighbors.Add(new Vector2Int(cell.x + 1, cell.y));

        if (cell.x - 1 >= 0 && !_mazeGrid[cell.x - 1, cell.y].IsVisited)
            neighbors.Add(new Vector2Int(cell.x - 1, cell.y));

        if (cell.y + 1 < _mazeDepth && !_mazeGrid[cell.x, cell.y + 1].IsVisited)
            neighbors.Add(new Vector2Int(cell.x, cell.y + 1));

        if (cell.y - 1 >= 0 && !_mazeGrid[cell.x, cell.y - 1].IsVisited)
            neighbors.Add(new Vector2Int(cell.x, cell.y - 1));

        return neighbors;
    }

    private void ClearWalls(Vector2Int current, Vector2Int next)
    {
        MazeCell currentCell = _mazeGrid[current.x, current.y];
        MazeCell nextCell = _mazeGrid[next.x, next.y];

        if (next.x > current.x)
        {
            currentCell.ClearRightWall();
            nextCell.ClearLeftWall();
        }
        else if (next.x < current.x)
        {
            currentCell.ClearLeftWall();
            nextCell.ClearRightWall();
        }
        else if (next.y > current.y)
        {
            currentCell.ClearFrontWall();
            nextCell.ClearBackWall();
        }
        else if (next.y < current.y)
        {
            currentCell.ClearBackWall();
            nextCell.ClearFrontWall();
        }
    }
}