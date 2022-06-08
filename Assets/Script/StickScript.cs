using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickScript : MonoBehaviour
{
  //  public Transform customPivot;
    public Rigidbody rigidbody;
    Vector3 m_EulerAngleVelocity;
    [SerializeField] int time = 99;
    public float m_NewForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.RotateAround(customPivot.position, Vector3.up, 9999 * Time.deltaTime);
       
           
    }
    void FixedUpdate()
    {
          Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime*time);
          rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
         rigidbody.AddForce(m_NewForce, m_NewForce, m_NewForce, ForceMode.Acceleration);
       // rigidbody.AddTorque(new Vector3(0f, 500f, 0f), ForceMode.Acceleration);
    }
}
