using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLevitate : MonoBehaviour
{
    public Vector3 rotationOrientation;

    public float levitateValue;

    float _maxY, _minY;

    private void Awake()
    {
        _maxY = transform.position.y + .1f;
        _minY = transform.position.y - .1f;
    }

    void FixedUpdate()
    {
        transform.Rotate(rotationOrientation);

        if (transform.position.y >= _maxY || transform.position.y <= _minY)
            levitateValue = -levitateValue;

        transform.position += new Vector3(0f, levitateValue, 0f);
    }
}
