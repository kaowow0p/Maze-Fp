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
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab , new Vector3(x , 0 , z) , Quaternion.identity);
            }
        }
    }
}
