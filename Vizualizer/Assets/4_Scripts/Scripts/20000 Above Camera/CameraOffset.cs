using UnityEngine;
using System.Collections;
using InControl;

public class CameraOffset : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;
    private Vector3 _targetPosition;

	
	void Update ()
    {
        ProcessInput();
        MoveTransform();


    }

    private void ProcessInput()
    {
        InputDevice activedevice = InputManager.ActiveDevice;
        Vector2 pos = activedevice.LeftStick.Value;
        _targetPosition = new Vector3(pos.x, pos.y) * _maxDistance;
    }

    private void MoveTransform()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * _speed);
    }
}
