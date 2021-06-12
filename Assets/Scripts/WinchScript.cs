using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchScript : MonoBehaviour
{
    // No-Joint Winch/Crane Script by Wes Jurica

    // Attach this to a GameObject that you want to act as the winch's cable origin point
    // Add LineRenderer to the same GameObject in the editor or in Start()
    // Variable "anchor" is the object that the other end of the cable get's attached to ie. tree for winching or box for craning
    // Add "anchor" tag to any object you'd like to connect to

    [SerializeField]
    private Camera currentCamera;
    [SerializeField]
    private int winchForce = 10000;
    [SerializeField]
    private int liftSpeed = 4;
    [SerializeField]
    private float lowerSpeed = 4;

    private Transform anchor = null;
    private LineRenderer lineRenderer = null;
    private float lastDistance = 0f;
    private float lastSpeed = 0f;
    private bool wasActive = false;
    private Rigidbody rb = null;

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && anchor == null)
        {
            RaycastHit hit;
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //if (hit.transform.tag == "anchor")
                //{
                if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                {
                    // "attaches" winch reel to anchor point
                    anchor = hit.transform;
                    rb = anchor.gameObject.GetComponent<Rigidbody>();
                    lineRenderer.enabled = true;
                    // record intital distance for stopping unreeling
                    lastDistance = Vector3.Distance(anchor.position, transform.position);
                }
                //}
            }
        }
    }

    private void FixedUpdate()
    {
        if (anchor != null)
        {
            // sets the line end points to the anchor and winch reel
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, anchor.position);

            // gets direction from winch reel to the anchor and sets it as the direction of force to be applied
            Vector3 forceDirection = (transform.position - anchor.position).normalized;

            if (Input.GetButton("Fire1") && rb.velocity.magnitude < liftSpeed)
            {
                Debug.Log("raise");
                // reels in winch
                rb.AddForce(forceDirection * winchForce);
                lastDistance = Vector3.Distance(anchor.position, transform.position);
            }
            else if (lastDistance < Vector3.Distance(anchor.position, transform.position) && rb.velocity.y < 0)
            {
                // stops winch from unreeling on its own
                rb.AddForce(forceDirection * winchForce * 2);
            }

            if (Input.GetButton("Fire2"))
            {
                Debug.Log(lastDistance.ToString());
                // unreels winch
                lastDistance += lowerSpeed / 80;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("break");
                // breaks connection / disables winch
                anchor = null;
                lineRenderer.enabled = false;
            }
        }
    }
}

