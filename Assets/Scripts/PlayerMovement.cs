using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    
    public static float speed;
    public static float speedBoostDuration;
    private bool speedAbilityActive = false;

    private void Awake()
    {
        //assigns Rigidbody component of player to rb
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SpeedAbility.onSpeedAbilityTriggered += ChangeSpeed;
    }

    private void OnDisable()
    {
        SpeedAbility.onSpeedAbilityTriggered -= ChangeSpeed;
    }
    
    private void FixedUpdate()
    {
        //gets player movement value from InputManager inputMap
        Vector2 movementValue = InputManager.Instance.inputMap.Player.Movement.ReadValue<Vector2>();
        //corrects Vector2 to Vector3 movement
        Vector3 movementCorrection = new Vector3(movementValue.x, 0f, movementValue.y).normalized;
        Vector3 movement;
        
        if (speedAbilityActive)
            //calculates movement with double speed
            movement = movementCorrection * speed * 2 * Time.fixedDeltaTime;
        else
            //calculates movement normally
            movement = movementCorrection * speed * Time.fixedDeltaTime;

        //moves player
        rb.MovePosition(rb.position + movement);

        //changes face direction only when the player is moving so it doesn't change when the player is not moving
        if (movement.magnitude > 0.1f)
        {
            //gets value of movement direction from movement vector and makes it a Quaternion
            Quaternion lookRotation = Quaternion.LookRotation(movement);
            
            //rotates player to look in the direction of movement
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, lookRotation, 20f));
        }
    }
    
    #region Speed
    private void ChangeSpeed()
    {
        speedAbilityActive = true;

        Speed(speedBoostDuration);
    }

    IEnumerator Speed(float duration)
    {
        yield return new WaitForSeconds(duration);

        speedAbilityActive = false;
    }
    #endregion
}
