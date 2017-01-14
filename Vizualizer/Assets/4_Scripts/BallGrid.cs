using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrid : MonoBehaviour {

    [SerializeField] private GameObject _ballPrefab;

    private int gridSize = 11;

	void Start ()
    {
        float triangleHeight = Mathf.Sqrt(3)/2;

        GameObject ballParent = new GameObject("BallParent");
        ballParent.transform.parent = transform;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < i + (gridSize - 4) + 2 * i; j++)
            {
                Vector3 position = transform.position + new Vector3(j - (float)(i + (gridSize - 4) + 2 * i - 1) / 2, i * triangleHeight, 4);
                GameObject ball = GameObject.Instantiate(_ballPrefab, position, transform.rotation);
                ball.transform.parent = ballParent.transform;
            }
        }
        ballParent.transform.rotation = Quaternion.Euler(54, 0, 0);
        ballParent.transform.localPosition = new Vector3(0, 2.3f, 0);
    }
}
