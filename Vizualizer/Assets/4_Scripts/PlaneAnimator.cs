using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimator : MonoBehaviour
{
    private float _currentPositionTarget;
    private float _currentScaleTarget;
    private float _moveSpeed;
    private float _scaleSpeed;
    private bool _movingRight;
    private bool _growing;
    
    void Start()
    {
        SetNewPositionTarget();
        SetNewScaleTarget();
    }
    
	void Update ()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * (_movingRight ? 1 : -1), 0, 0);
        if (transform.position.x > _currentPositionTarget == _movingRight)
        {
            SetNewPositionTarget();
        }

        transform.localScale = new Vector3(transform.localScale.x + _scaleSpeed * Time.deltaTime * (_growing ? 1 : -1), transform.localScale.y, transform.localScale.z);
        if (transform.localScale.x > _currentScaleTarget == _growing)
        {
            SetNewScaleTarget();
        }
	}

    void SetNewPositionTarget()
    {
        _moveSpeed = Random.Range(0.25f, 2f);
        _currentPositionTarget = Random.Range(-25.0f, 25.0f);
        if (_currentPositionTarget > transform.localPosition.x)
            _movingRight = true;
        else
            _movingRight = false;
    }

    void SetNewScaleTarget()
    {
        _currentScaleTarget = Random.Range(0.01f, 0.4f);
        _scaleSpeed = Random.Range(0.05f, 0.2f);
        if (_currentScaleTarget > transform.localScale.x)
            _growing = true;
        else
            _growing = false;
    }

}
