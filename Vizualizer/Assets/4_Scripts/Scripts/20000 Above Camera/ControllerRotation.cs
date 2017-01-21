using UnityEngine;
using System.Collections;
using InControl;

public class ControllerRotation : MonoBehaviour
{
    [SerializeField]
    private CameraController _controller;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _zoomSpeed;
    [SerializeField]
    private float _minZoom;
    [SerializeField]
    private float _maxZoom;

    private bool _slowRotation = false;
    private float _slowRotationMultiplier = 0.05f;

    void Update()
    {
        InputDevice activedevice = InputManager.ActiveDevice;
        Vector2 stickPos = activedevice.RightStick.Value;

        ProcessRotation(stickPos);
        ProcessZoom(activedevice);

        if (activedevice.CommandWasPressed)
        {
            _slowRotation = !_slowRotation;
        }
    }

    private void ProcessRotation(Vector2 stickPos)
    {
        float pitchAngle = _controller.CameraSmoothAdjust.Current.Pitch;
        float yawAngle = _controller.CameraSmoothAdjust.Current.Yaw;

        pitchAngle += stickPos.y * _rotationSpeed / Time.deltaTime * (_slowRotation ? _slowRotationMultiplier : 1);
        yawAngle += stickPos.x * _rotationSpeed / Time.deltaTime * (_slowRotation ? _slowRotationMultiplier : 1);
        pitchAngle = Mathf.Clamp(pitchAngle, -88, 88);

        _controller.CameraSmoothAdjust.Target.Pitch = pitchAngle;
        _controller.CameraSmoothAdjust.Target.Yaw = yawAngle;
    }

    private void ProcessZoom(InputDevice activedevice)
    {
        float zoom = _controller.CameraSmoothAdjust.Current.Zoom;

        zoom += activedevice.LeftTrigger.Value * _zoomSpeed / Time.deltaTime;
        zoom -= activedevice.RightTrigger.Value * _zoomSpeed / Time.deltaTime;

        _controller.CameraSmoothAdjust.Target.Zoom = Mathf.Clamp(zoom, _minZoom, _maxZoom);
    }
}
