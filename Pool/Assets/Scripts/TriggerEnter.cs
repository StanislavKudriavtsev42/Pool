using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TriggerEnter : MonoBehaviour
{
    public Transform exit;
    public Transform cueExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (gameObject.name != "Cue Ball")
            {
                gameObject.transform.position = exit.transform.position;
                gameObject.GetComponent<ScoredBallMovement>().enabled = true;
                gameObject.GetComponent<BallSpeedRegulator>().isScored = true;
            }
            else
            {
                gameObject.transform.position = cueExit.transform.position;
            }
            return;
        }
        else if(other.gameObject.layer == 10)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = new Vector3(2.795f, 1.531685f, 0.0387488f);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().velocity = new Vector3(0, -0.08f, 0);
        }
    }
}
