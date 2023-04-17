using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    #region Public Variables
    
    [Header("Object Properties")]
    [Tooltip("The player's Rigibody")]
    public Rigidbody PlayerRigidbody;
    [Tooltip("The player's transform")]
    public Transform PlayerTransform;

    [Header("Player Forces")]
    [Tooltip("This is the the angular force (torque)")]
    public int TurnTorque = 100;

    [Tooltip("This is the force you will be pushing and pulling with")]
    public int PushPullForce = 100;

    [Tooltip("This is the force you will be Strafing with")]
    public int StrafeForce = 100;

    #endregion

    #region Private Variables


    #endregion

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        CartMovement();

    }

    #region Cart Movement Functions

    private void CartMovement(){
        RotateCart();
        PushPullCart();
        StrafeCart();


    }

    /**
        This rotates the cart by adding a force for torque
        ignore the fact that this caused hell earlier neko
        you're so hot dude
    **/
    private void RotateCart(){
        /**
            This gets the angle we're going to add to the torque by
            just getting the direction you're moving (horizontal) and adding
            the amount of torque desired, then equalizing it with delta time
        **/
        float TurnF = Input.GetAxis("Horizontal") * TurnTorque;
        
        PlayerRigidbody.AddTorque(0, TurnF, 0);
    }

    /**
        This pushes and pulls the cart with the transform forward
        Yeah
        hehe x3c you're so cute neko
    **/
    private void PushPullCart(){
        /**
            This gets the push pull force vector by getting the forward, multipling by the direction and the force
            then multiplying in delta time
        **/
        Vector3 PushPullVector = PlayerTransform.forward * PushPullForce * Input.GetAxis("Vertical");
    
        //This this adds the force vector to the rigid body
        PlayerRigidbody.AddForce(PushPullVector);
    }

    /**
        This makes it so we can strafe the cart left and right
        we'll be using transform.right for this.
    **/

    private void StrafeCart(){
        /** 
            This gets the Strafe vector by getting the right, multipling by the direction and the force
            then multiplying in delta time
        **/

        Vector3 StrafeVector = PlayerTransform.right * StrafeForce * Input.GetAxis("Strafe");
    
        //This this adds the force vector to the rigid body
        PlayerRigidbody.AddForce(StrafeVector);
    }

    #endregion

    #region Slopes



    #endregion
    
}
