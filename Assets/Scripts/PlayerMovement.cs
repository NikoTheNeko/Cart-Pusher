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

    [Header("Player Limiters")]
    [Tooltip("This is the minimun weight it can be going less than this will cause nyoom")]
    public int MinWeight = 50;

    [Tooltip("This is the max weight, high values can immobolize players!")]
    public int MaxWeight = 169;

    [Header("Additional Items")]
    [Tooltip("This is where the items will spawn on the cart")]
    public Transform ItemSpawner;

    [Tooltip("List of items")]
    public List<ItemScriptableObject> CartItems = new List<ItemScriptableObject>();

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

    #region Cart Weight Functions

    /**
        This adds weight to the cart by adding mass onto the cart
        If you don't know how that works its because in real life all objects
        have mass, then the gravitational pull to something (in this case Earth)
        is called weight. So if you add more mass (like a fat ass) you will have more
        weight because your ass is so fat neko hnngg 😩

        Hi guys this is how I code
    **/
    public void AddWeight(int WeightToBeAdded){
        
        if(!CheckWeightInBounds(WeightToBeAdded))
            return;

        //Gets the rigidbody mass then adds to it
        PlayerRigidbody.mass += WeightToBeAdded;
    }

    /**
        Does the same as above but removes the weight
    **/

    public void RemoveWeight(int WeightToBeRemoved){

        if(!CheckWeightInBounds(WeightToBeRemoved))
            return;

        //Gets the rigidbody mass then subtracts it
        PlayerRigidbody.mass -= WeightToBeRemoved;
    }

    /**
        Checking the weight so that way it like doesnt' fucking be too heavy or too light
        either will be catastrophic and will immobolize the player or send them straight to 
        Canda

        Input - The weight you will be adding in

        Outputs - If it is in within bounds

        By default it will return true.

        If it does not pass the checks it will return false.

    **/
    public bool CheckWeightInBounds(int WeightChange){

        //Calculates the new weight
        float NewWeight = WeightChange + PlayerRigidbody.mass;

        //Does a check if it out of bounds, returns false if it is
        if(NewWeight < MinWeight || NewWeight > MaxWeight)
            return false;

        //If it is within range, return true
        return true;
    }

    #endregion
    
    #region Cart Item Functions
    
    /**
        This is intended for other objects to call,
        You need to supply a item scriptable object that way it can be 
        properly registered into the the cart
    **/
    public void AddItem(ItemScriptableObject ItemToAdd){
        //Creates a reference of the new item to add
        var ItemBeingAdded = ItemToAdd;
        
        //Spawns the object in the cart
        Quaternion Rotato = new Quaternion(0,0,0,0);
        Vector3 ItemSpawnLocation = ItemSpawner.position;
        var ObjectSpawned = Object.Instantiate(ItemToAdd.ItemPrefab, ItemSpawnLocation, Rotato);

        //Adds the object spawned to the item to add        
        ItemBeingAdded.SceneObject = ObjectSpawned;
        //Adds the item to the cart
        CartItems.Add(ItemBeingAdded);
        
    }

    public void RemoveItem(){
        Object.Destroy(CartItems[0].SceneObject);
        CartItems.RemoveAt(0);
    }

    #endregion

}
