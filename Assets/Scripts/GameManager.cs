using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;

    private void Start()
    {
        Vector3 startPosition = new Vector3(0, 1, 0);
        Quaternion startRotation = Quaternion.identity;
        Instantiate(_playerPrefab , startPosition , startRotation);

        Debug.Log(startPosition);
    }
}
