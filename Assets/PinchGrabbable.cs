using GoatRock;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.MagicLeap;

[RequireComponent(typeof(FloatToVector3Behavior))]
public class PinchGrabbable : MonoBehaviour {

    public LayerMask GrabLayers;
    public LayerMask PlantLayers;
    [Space(5)]
    public Animator ThisAnimator;
    public EmailUnfold FoldEmail;
    [Space(5)]
    public static int LeftFingerLayer = 12;
    public static int RightFingerLayer = 13;
    [Space(5)]
    public FloatToVector3Behavior FloatingBehavior;
    public bool Grabbed = false;
    ///-----------Jacks questionable code ----------------------------------------
    public UnityEvent onPicked;
    public UnityEvent onDefaultGrab;
    public UnityEvent onGrabEnd;
    public bool onPlant = true;
    ///-----------Jacks questionable code ----------------------------------------

    public GameObject PlantZone;
    public List<GameObject> FingerTriggers = new List<GameObject>();

    private void Reset()
    {
        FloatingBehavior = GetComponent<FloatToVector3Behavior>();
        FloatingBehavior.enabled = false;
    }

    private void Start()
    {
        FloatingBehavior.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // pinching
        if(Helpers.IsInLayerMask(other.gameObject.layer, GrabLayers))
        {
            if(!FingerTriggers.Contains(other.gameObject))
            {
                FingerTriggers.Add(other.gameObject);
                Debug.Log("Finger entered! " + other.gameObject.name);
            }
        }

        // plant zone
        if (Helpers.IsInLayerMask(other.gameObject.layer, PlantLayers))
        {
            if(PlantZone != other.gameObject)
            {
                PlantZone = other.gameObject;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Helpers.IsInLayerMask(other.gameObject.layer, GrabLayers))
        {
            if (!FingerTriggers.Contains(other.gameObject))
            {
                FingerTriggers.Add(other.gameObject);
                Debug.Log("Finger entered! " + other.gameObject.name);
            }
        }

        // plant zone
        if (Helpers.IsInLayerMask(other.gameObject.layer, PlantLayers))
        {
            if (PlantZone != other.gameObject)
            {
                PlantZone = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // plant zone
        if (Helpers.IsInLayerMask(other.gameObject.layer, PlantLayers))
        {
            if (PlantZone == other.gameObject)
            {
                PlantZone = null;
            }
        }

        // pinching
        if (Helpers.IsInLayerMask(other.gameObject.layer, GrabLayers))
        {
            if (FingerTriggers.Contains(other.gameObject))
            {
                FingerTriggers.Remove(other.gameObject);
            }
        }
    }

    /*
    void OnTriggerExit(Collider other)
    {
        if (Helpers.IsInLayerMask(other.gameObject.layer, GrabLayers))
        {
            if (FingerTriggers.Contains(other.gameObject))
            {
                FingerTriggers.Remove(other.gameObject);
                Debug.Log("Finger removed! " + other.gameObject.name);
            }
        }
    }
    */

    private void FixedUpdate()
    {
 
        // If two finger triggers are here, we're being pinched!
        if (!Grabbed)
        {
            

            if (FingerTriggers.Count > 1)
            {
                if (GetGesture(MLHands.Left, MLHandKeyPose.Ok) || GetGesture(MLHands.Left, MLHandKeyPose.Pinch) || GetGesture(MLHands.Right, MLHandKeyPose.Ok) || GetGesture(MLHands.Right, MLHandKeyPose.Pinch))
                {
                    TryGrab();
                }
            }
        }
        else
        {   
            if(FingerTriggers.Count > 0)
            {
                if(GetGesture(MLHands.Left, MLHandKeyPose.Ok) || GetGesture(MLHands.Left, MLHandKeyPose.Pinch) || GetGesture(MLHands.Right, MLHandKeyPose.Ok) || GetGesture(MLHands.Right, MLHandKeyPose.Pinch))
                {
                    FloatingBehavior.TargetPosition = FingerTriggers[0].transform.position;
                }

                
                if (FingerTriggers[0].layer == LeftFingerLayer)
                {
                    if (!GetGesture(MLHands.Left, MLHandKeyPose.Ok) && !GetGesture(MLHands.Left, MLHandKeyPose.Pinch))
                    {
                        TryRelease();
                    }
                }
                else
                {
                    if (!GetGesture(MLHands.Right, MLHandKeyPose.Ok) && !GetGesture(MLHands.Right, MLHandKeyPose.Pinch))
                    {
                        TryRelease();
                    }
                }
            }
        }
    }

    void TryGrab()
    {
        Debug.Log("PINCHED!");
        FloatingBehavior.enabled = true;
        Grabbed = true;
        ThisAnimator.SetBool("Grabbed", Grabbed);

        if (onPlant)
        {
            onPicked.Invoke();
        }
        else
        {
            onDefaultGrab.Invoke();
        }

onPlant = false;

        if (!PlantZone)
        {
            FoldEmail.Unfold();
        }
    }

    void TryRelease()
    {
        onGrabEnd.Invoke();

        Debug.Log("RELEASED!");
        FloatingBehavior.enabled = false;
        Grabbed = false;
        ThisAnimator.SetBool("Grabbed", Grabbed);
        FingerTriggers.Clear();

        if (!PlantZone)
        {
            FoldEmail.Fold();
        }
    }

    private bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.5f)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
