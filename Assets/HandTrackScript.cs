using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackScript : MonoBehaviour
{
    public enum MagicLeapHand { Left, Right };

    #region  Public Variables
    public MagicLeapHand ThisMLHand;
    public MLHand ThisHand;

    public enum HandPoses { Ok, Pinch, Finger, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    public Vector3[] pos;
    public GameObject sphereThumb, sphereIndex, sphereWrist;

    #endregion

    #region Private Variables
    private MLHandKeyPose[] _gestures;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        MLHands.Start();
        _gestures = new MLHandKeyPose[4];
        _gestures[0] = MLHandKeyPose.Ok;
        _gestures[1] = MLHandKeyPose.Pinch;
        _gestures[2] = MLHandKeyPose.Finger;
        _gestures[3] = MLHandKeyPose.NoPose;
        MLHands.KeyPoseManager.EnableKeyPoses(_gestures, true, false);
        pos = new Vector3[3];

        if(ThisMLHand == MagicLeapHand.Left)
        {
            ThisHand = MLHands.Left;
        }
        else
        {
            ThisHand = MLHands.Right;   
        }
    }

    private void OnDestroy()
    {
        MLHands.Stop();
    }

    private void Update()
    {
        if (GetGesture(ThisHand, MLHandKeyPose.Ok))
        {
            pose = HandPoses.Ok;
        }
        else if (GetGesture(ThisHand, MLHandKeyPose.Finger))
        {
            pose = HandPoses.Finger;
        }
        else if (GetGesture(ThisHand, MLHandKeyPose.Pinch))
        {
            pose = HandPoses.Pinch;
        }
        else
        {
            pose = HandPoses.NoPose;
        }

        if (pose != HandPoses.NoPose) ShowPoints();
    }
    #endregion



    #region Private Methods
    private void ShowPoints()
    {

        // Left Hand Thumb tip
        pos[0] = MLHands.Left.Thumb.KeyPoints[0].Position;
        // Left Hand Index finger tip 
        pos[1] = MLHands.Left.Index.KeyPoints[0].Position;
        // Left Hand Wrist 
        pos[2] = MLHands.Left.Wrist.KeyPoints[0].Position;
        sphereThumb.transform.position = pos[0];
        sphereIndex.transform.position = pos[1];
        sphereWrist.transform.position = pos[2];
    }

    private bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

}