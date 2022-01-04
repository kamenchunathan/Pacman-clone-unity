using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;

    private Animator _playerAnimator;
    private readonly int _animation1 = Animator.StringToHash("Animation");

    /// <summary>
    /// Used to track direction changes to set the relevant animation for the direction of travel 
    /// Corresponds to the transition condition of the animation
    ///      0 ->  forward animation
    ///      1 ->  right animation  
    ///      2 ->  back animation
    ///      3 ->  lef animation
    /// </summary>
    private int _currentTravelDirection = 0;

    private void Start(){
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update(){
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(xInput, yInput, 0).normalized;

        // Set the appropriate animation according to direction 
        int newDirection = GetMostSignificantDirection(inputVector);
        if (_currentTravelDirection != newDirection){
            _playerAnimator.SetInteger(_animation1, newDirection);
            _currentTravelDirection = newDirection;
        }

        // Move the player
        // TODO: Replace translation with snappier movement system
        transform.Translate(inputVector * playerSpeed * Time.deltaTime);
    }

    /// <summary>
    /// maps the normalized input vector to an integer in the range 0 to 3 that represents the
    /// most significant direction
    ///    0 ->  forward 
    ///    1 ->  right   
    ///    2 ->  back 
    ///    3 ->  left 
    /// </summary>
    /// <param name="normalizedInputVector"></param>
    /// <returns></returns>
    private int GetMostSignificantDirection(Vector3 normalizedInputVector){
        float x = normalizedInputVector.x;
        float y = normalizedInputVector.y;

        if ((int) x == 0 && (int) y == 0)
            return _currentTravelDirection;

        return Math.Abs(x) > Math.Abs(y) ? 2 - Convert.ToInt32(x) : 1 + Convert.ToInt32(y);
    }
}