using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private List<GameObject> _pointsToMove;

    [Space(10)]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    
    private bool _move;
    private bool _look;
    private int _index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if(!_look && !_move) _look = true;
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            if(!_move) _move = true;
        }


#elif UNITY_ANDROID
     if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            if(!_look && !_move) _look = true;
        };

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            if(!_move) _move = true;
        };
#endif

        if (_look)
        {
            _look = !LookAtTarget();
        };

        if (_move)
        {
            _move = MoveToTarget();
            if (!_move) _look = false;
        };

    }

    private bool MoveToTarget()
    {
        Vector3 target = _pointsToMove[_index].transform.position;
        float distance = Vector3.Distance(transform.position,target);
        
        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                _speed * Time.deltaTime
                );
        }
        else
        {
            _index = _index < _pointsToMove.Count - 1 ? _index + 1 : 0;

            return false;
        }

        return true;
    }

    private bool LookAtTarget()
    {
        Vector3 direction = _pointsToMove[_index].transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            return true;
        }

        return false;
    }
}
