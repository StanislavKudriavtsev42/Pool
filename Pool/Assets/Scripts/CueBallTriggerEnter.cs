using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallTriggerEnter : MonoBehaviour
{
    public Transform exit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cue Entrance") {
            gameObject.transform.position = exit.position;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
