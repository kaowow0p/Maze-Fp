using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Range(5f, 20f)] private float _speed;    

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = new Vector2(horizontal, vertical);

        transform.position = new Vector3(move.x, move.y, 0f) * _speed * Time.deltaTime;
    }
}
