using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Game Objects")]
    [Tooltip("This is the camera's transform")]
    public Transform CameraTransform;

    [Tooltip("This is the target you wish to track")]
    public Transform TargetToTrack;

    [Header("Camera Speed Values")]
    [Tooltip("This is how fast the lerp will be")]
    public float CameraFollowSpeed = 0.5f;

    [Header("Offsets")]
    [Tooltip("This is the offset from the camera to the object you are tracking")]
    public Vector3 CameraOffset = new Vector3(0, 10, 10);

    #endregion

    #region Private Variables
    #endregion

    // Start is called before the first frame update
    void Start(){
        
    }

    

    // Update is called once per frame
    void FixedUpdate(){
        TrackTarget();
        
    }

    #region Camera Functions

    /**

    **/
    private void TrackTarget(){

        //This is the camera's CURRENT position
        Vector3 CurrentPosition = CameraTransform.position;

        //This offsets the target position
        Vector3 OffsetPosition = TargetToTrack.position + CameraOffset;
        
        //This is the position from the camera to the target
        Vector3 NewPosition = Vector3.Lerp(CurrentPosition, OffsetPosition, CameraFollowSpeed);
    
        //Sets the camera position to the new one
        CameraTransform.position = NewPosition;
    
    
    }

    #endregion
}
