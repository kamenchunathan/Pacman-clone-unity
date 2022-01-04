using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;

    private Animator _playerAnimator;

    private int _currentTravelDirection = 0;
    private readonly int animation1 = Animator.StringToHash("Animation");

    void Start(){
        _playerAnimator = GetComponent<Animator>();
    }

    void Update(){
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(xInput, yInput, 0).normalized;

        // Set the appropriate animation according to direction 
        int newDirection = GetMostSignificantDirection(inputVector);
        if (_currentTravelDirection != newDirection){
            _playerAnimator.SetInteger(animation1, newDirection);
            _currentTravelDirection = newDirection;
        }

        // Move the player
        // TODO: Replace translation with snappier movement system
        transform.Translate(inputVector * playerSpeed * Time.deltaTime);
    }

    private int GetMostSignificantDirection(Vector3 normalizedInputVector){
        float x = normalizedInputVector.x;
        float y = normalizedInputVector.y;

        if ((int) x == 0 && (int) y == 0)
            return _currentTravelDirection;

        return Math.Abs(x) > Math.Abs(y) ? 2 - Convert.ToInt32(x) : 1 + Convert.ToInt32(y);
    }
}