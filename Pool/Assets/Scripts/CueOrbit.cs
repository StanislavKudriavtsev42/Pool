using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueOrbit : MonoBehaviour
{
    public GameObject orbitPoint;
    public GameObject homePosition;
    public float speed = 5f;
    public float cueOffset = 0;

    private GameManager gameManager;
    private Pause pauseScript;
    private Vector3 originalPosition;
    private double cueDeg;
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        pauseScript = GameObject.Find("Game Manager").GetComponent<Pause>();
        MoveToOrbitPoint();
        gameManager.turnCount++;
        gameManager.ChangeTurn();
    }
    void Update()
    {
        if (gameManager.ballsAreStationary && gameManager.allowRotation && !pauseScript.paused)
        {
            OrbitAround();
        }
    }

    public void OrbitAround()
    {
        Vector3 cueDirection = orbitPoint.GetComponent<Shoot>().GetHitPoint();
        double deltaX = cueDirection.x - orbitPoint.transform.position.x;
        double deltaZ = cueDirection.z - orbitPoint.transform.position.z;
        double rad = Math.Atan2(deltaZ, deltaX);
        double deg = rad * (180 / Math.PI);
        cueDeg = gameObject.transform.rotation.eulerAngles.y + 90;

        if (deg < 0)
            deg = 360 + deg;

        if (cueDeg > 360)
            cueDeg -= 360;

        if (cueDeg - (360 - deg) > 358 || cueDeg - (360 - deg) < -358 || Math.Abs(cueDeg - (360 - deg)) < 2)
            speed = 25;
        else if (cueDeg - (360 - deg) > 350 || cueDeg - (360 - deg) < -350 || Math.Abs(cueDeg - (360 - deg)) < 10)
            speed = 80;
        else if (cueDeg - (360 - deg) > 300 || cueDeg - (360 - deg) < -300 || Math.Abs(cueDeg - (360 - deg)) < 60)
            speed = 400;
        else if (cueDeg - (360 - deg) > 200 || cueDeg - (360 - deg) < -200 || Math.Abs(cueDeg - (360 - deg)) < 160)
            speed = 600;
        else
            speed = 1200;

        if(cueDeg > 315 && deg > 315)
            gameObject.transform.RotateAround(orbitPoint.transform.position, Vector3.up, speed * Time.deltaTime);
        else if (cueDeg < 45 && deg < 45)
            gameObject.transform.RotateAround(orbitPoint.transform.position, Vector3.down, speed * Time.deltaTime);
        else if (360 - deg > cueDeg + cueOffset)
            gameObject.transform.RotateAround(orbitPoint.transform.position, Vector3.up, speed * Time.deltaTime);
        else if (360 - deg < cueDeg - cueOffset)
            gameObject.transform.RotateAround(orbitPoint.transform.position, Vector3.down, speed * Time.deltaTime);
    }

    public void MoveToOrbitPoint()
    {
        Transform orbitTransform = orbitPoint.transform;
        Vector3 newPosition = new Vector3(orbitTransform.position.x + 1.535804f,
                                          orbitTransform.position.y + 0.15f, 
                                          orbitTransform.position.z);
        gameObject.transform.position = newPosition;

        Vector3 newRotation = new Vector3(6f, -90f, 0);
        gameObject.transform.rotation = Quaternion.Euler(newRotation);
    }
    public void MoveToHomePosition()
    {
        gameObject.transform.position = homePosition.transform.position;
        Vector3 newRotation = new Vector3(0, -90f, 0);
        gameObject.transform.rotation = Quaternion.Euler(newRotation);

    }
    public void Pull()
    {
        float power = orbitPoint.GetComponent<Shoot>().GetPower() / 20f;
        float divConst = 12;

        float x;
        if (cueDeg <= 360 && cueDeg >= 270 || cueDeg >= 0 && cueDeg <= 90)
            x = originalPosition.x + power / divConst * Mathf.Abs(originalPosition.x - orbitPoint.transform.position.x);
        else
            x = originalPosition.x - power / divConst * Mathf.Abs(originalPosition.x - orbitPoint.transform.position.x);

        float z;
        if (cueDeg <= 360 && cueDeg >= 180)
            z = originalPosition.z + power / divConst * Mathf.Abs(originalPosition.z - orbitPoint.transform.position.z);
        else
            z = originalPosition.z - power / divConst * Mathf.Abs(originalPosition.z - orbitPoint.transform.position.z);

        Vector3 targetPoint = new Vector3(x, originalPosition.y, z);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, 0.5f);
    }

    public void SetOriginalPosition()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
