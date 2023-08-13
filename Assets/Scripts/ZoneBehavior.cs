using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBehavior : MonoBehaviour{

    #region Public Variables

    [Header("Player Cart")]

    [Tooltip("This is the player cart so the main player")]
    public PlayerMovement PlayerCart;

    [Header("Timer Properties")]
    [Tooltip("How long the timer for picking up items will take")]
    public float TimerLength = 5f;

    [Header("Item Properties")]
    public ItemScriptableObject ZoneItem;

    #endregion

    #region Private Variables
    //This is the timer that will be counted down from
    private float RunningTimer;

    //This is a variable that controls if the timer is going to run or not
    private bool RunTimer = false;

    //This makes it so we can check if we can add an item to the cart or not
    private bool CanAddItem = true;

    #endregion

    private void Awake() {
        //Sets the running timer to the length of the timer
        RunningTimer = TimerLength;        
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //This runs the timer if you can add to the cart
        TimerFunction();

        Debug.Log(RunningTimer);
    }

    //This runs the timer, it counts down if you are able to run the timer
    private void TimerFunction(){
        if(RunTimer)
            RunningTimer -= Time.deltaTime;

        //If the timer is less than 0, then add the weight and reset the running
        if(RunningTimer <= 0){
            PlayerCart.AddWeight(ZoneItem.Weight);
            RunningTimer = TimerLength;
            PlayerCart.AddItem(ZoneItem);
        }
    }

    

    #region Trigger functions
    /**
        This when an object enters the trigger
    **/
    private void OnTriggerEnter(Collider other) {
        
        if(other.tag.Equals("Player")){
            //Checks to see if it's even possible to add an item
            var NewMass = PlayerCart.PlayerRigidbody.mass + ZoneItem.Weight;
    
            if(NewMass >= PlayerCart.MaxWeight){
                //If the new mass is bigger than the max, then don't
                CanAddItem = false;
            } else {
                //If the new mass is within range, then add
                CanAddItem = true;
            }
        }

        //If we CAN add the item, then run the timer.
        if(CanAddItem)
            RunTimer = true;
    
    }

    private void OnTriggerExit(Collider other) {
        //This resets the timer
        RunningTimer = TimerLength;

        //This sets it so the timer stops ticking down        
        RunTimer = false;
    }
    #endregion
}
