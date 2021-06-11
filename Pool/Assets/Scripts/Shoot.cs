using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Rigidbody rb;
    private float power = 0;
    private Vector3 shootingDirection;
    private Vector3 hitPoint;
    private Vector3 targetPoint;
    private RaycastHit hit;
    private GameManager gameManager;
    private Pause pauseScript;
    private CueOrbit cueOrbit;
    private bool shootingIsAuthorized;

    public float maxPower = 3f;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        pauseScript = GameObject.Find("Game Manager").GetComponent<Pause>();
        cueOrbit = GameObject.Find("Cue").GetComponent<CueOrbit>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (gameManager.ballsAreStationary && !pauseScript.paused)
        {
            ShootBall();
        }
    }

    private void ShootBall()
    {
        if (RaycastSuccessful())
        {
            hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            if (Input.GetMouseButtonDown(0))
            {
                targetPoint = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                gameManager.allowRotation = false;
                cueOrbit.SetOriginalPosition();
                shootingIsAuthorized = true;
            }
            else if (Input.GetMouseButton(0) && shootingIsAuthorized)
            {
                SetPower();
                cueOrbit.Pull();
            }
            else if (Input.GetMouseButtonUp(0) && shootingIsAuthorized)
            {
                Vector3 tempVector = new Vector3(-(targetPoint.x - transform.position.x), 0,
                                                 -(targetPoint.z - transform.position.z));
                shootingDirection = Vector3.ClampMagnitude(tempVector, 0.1f);
                if (power > 4f)
                    rb.AddForce(shootingDirection * power, ForceMode.Impulse);
                else
                    power = 0f;
                gameManager.allowRotation = true;
                shootingIsAuthorized = false;
            }
        }
    }

    private bool RaycastSuccessful()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Ray ray;
        ray = Camera.main.ScreenPointToRay(mouse);
        return Physics.Raycast(ray, out hit, 9);
    }

    private void SetPower()
    {
        power = Math.Abs(transform.position.x - hitPoint.x) + Math.Abs(transform.position.z - hitPoint.z);
        power *= 50f;
        if (power > maxPower)
        {
            power = maxPower;
        }

        float targetX = targetPoint.x - transform.position.x;
        float targetZ = targetPoint.z - transform.position.z;

        float hitX = hitPoint.x - transform.position.x;
        float hitZ = hitPoint.z - transform.position.z;

        int coefX = 1;
        int coefZ = 1;

        if (targetX < 0)
            coefX = -1;
        if (targetZ < 0)
            coefZ = -1;

        if (targetX + (coefX * hitX) < targetX || targetZ + (coefZ * hitZ) < targetZ)
            power = 0;
    }

    public float GetPower()
    {
        return power;
    }

    public Vector3 GetHitPoint()
    {
        return hitPoint;
    }
}
