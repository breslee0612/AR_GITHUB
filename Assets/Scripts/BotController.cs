using System.Collections;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UIElements;

public class BotController : MonoBehaviour
{
    Vector3 curr;
    [SerializeField]
    private float maxSpeed;
    float speed = 10;
    float ldistance;
    float rdistance;
    float timer = 0.15f;
    float angle;
    bool collided = false;
    Ray leftRay;
    Ray rightRay;
    RaycastHit lhit;
    RaycastHit rhit;
    public int laps = 0;
    List<GameObject> roadList = new List<GameObject>();
    public LayerMask mask;
    [SerializeField]
    private Rigidbody carRigidBody = null;
    public bool CanStart = false;

    // Start is called before the first frame update
    void Start()
    {
        curr = transform.forward;
    }

    private void OnCollisionEnter(Collision other)
    {
        collided = true;
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name!="Finish")
        {
            curr = other.transform.GetChild(0).forward;
            if(roadList.Contains(other.gameObject)==false)
            {
                roadList.Add(other.gameObject);
            }
        }     
        else
        {
            if(roadList.Count>=8)
            {
                laps+=1;
                roadList.Clear();
            }
        }
    }

    void CheckDistance()
    {
        if (Physics.Raycast(leftRay, out lhit,2f,mask))
        {
            ldistance = lhit.distance;
            // Debug.Log(ldistance.ToString()+"  "+rdistance.ToString());
        }
        if (Physics.Raycast(rightRay, out rhit,2f,mask))
        {
            rdistance = rhit.distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanStart)
        {
            angle = Vector3.SignedAngle(transform.forward, curr, Vector3.up);
            leftRay = new Ray(transform.position + new Vector3(0, -0.015f, 0), -transform.right);
            rightRay = new Ray(transform.position + new Vector3(0, -0.015f, 0), transform.right);
            CheckDistance();

            // BOTCarController controller = GetComponent<BOTCarController>();
            BOTCarController controller = GetComponent<BOTCarController>();

            if (angle > 2)
            {
                controller.TurnRight();
            }
            else if (angle < -2)
            {
                controller.TurnLeft();
            }

            if (ldistance < 0.2)
            {
                controller.TurnRight();
            }
            if (rdistance < 0.2)
            {
                controller.TurnLeft();
            }
            if (collided)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    if (carRigidBody.velocity.magnitude < maxSpeed)
                    {
                        controller.Reverse();
                    }
                }
                else
                {
                    collided = false;
                    timer = 0.15f;
                }
            }
            else
            {
                if (carRigidBody.velocity.magnitude < maxSpeed)
                {
                    controller.Accelerate();
                }
            }
        }
        //controller.Accelerate();
    }
}
