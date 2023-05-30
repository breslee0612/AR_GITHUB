using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleObject : MonoBehaviour
{
    private GameObject spawnedObject = null;
    private Vector3 initialScale;

    private GameObject customReticle;
    private float initialDistance;
    public float factor = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnedObject = GameObject.FindWithTag("MAP");
        if (spawnedObject != null)
        {
            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                    touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return; // basically do nothing
                }

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = spawnedObject.transform.localScale;
                    //Debug.Log("Initial Disatance: " + initialDistance + "GameObject Name: "
                    //    + arObjectToSpawn.name); // Just to check in console
                }
                else // if touch is moved
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    //if accidentally touched or pinch movement is very very small
                    if (Mathf.Approximately(initialDistance, 0))
                    {
                        return; // do nothing if it can be ignored where inital distance is very close to zero
                    }

                    factor = currentDistance / initialDistance;
                    spawnedObject.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated
                }
            }
        }
    }
}
