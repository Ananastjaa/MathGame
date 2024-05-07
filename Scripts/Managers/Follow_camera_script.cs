using UnityEngine;

public class Follow_camera_script : MonoBehaviour
{
    [SerializeField] private float _damping = 1f;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Transform _player;
    private float _position_po_y;

    void FixedUpdate()
    {
        if (_player.position.y > _position_po_y)
        {
            Vector3 target = new Vector3(transform.position.x, _player.position.y - _offset.y, transform.position.z);
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, _damping);
            transform.position = currentPosition;
            _position_po_y = _player.position.y;
        }
    }
}
