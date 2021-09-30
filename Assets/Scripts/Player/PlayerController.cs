using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	float playerSpeed = 10.0f;

	Animator playerAnimator;

	int currentTravelDirection = 0;

	void Start(){
		playerAnimator = GetComponent<Animator>();
	}

	void Update(){
		float xInput = Input.GetAxis("Horizontal");
		float yInput = Input.GetAxis("Vertical");

		Vector3 inputVector = new Vector3(xInput, yInput, 0).normalized;

		// Set the appropriate animation according to direction 
		int newDirection = getMostSignificantDirection(inputVector);
		if (currentTravelDirection != newDirection){
			playerAnimator.SetInteger("Animation", newDirection);
			currentTravelDirection = newDirection;
		}

		// Move the player
		// TODO: Replace translation with snappier movement system
		transform.Translate(inputVector * playerSpeed * Time.deltaTime);
	}

	int getMostSignificantDirection(Vector3 normalizedInputVector){
		float x = normalizedInputVector.x;
		float y = normalizedInputVector.y;

		if((int)x == 0 && (int)y==0)
			return currentTravelDirection;

		return Math.Abs(x) > Math.Abs(y) ? 2 - Convert.ToInt32(x) : 1 + Convert.ToInt32(y);
	}

}
