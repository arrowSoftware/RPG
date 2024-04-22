using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // TODO https://www.youtube.com/watch?v=_ICCSDmLCX4 better way to do this without creting so many game objects
    public float destroyTime = 3.0f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);
    private Animator anim;

    float smoothSpeed = 10.0f;
    float targetSpeed = 0.0f;
    float speedRef = 0.0f;
    float speedZRef = 0.0f;
    float smoothZSpeed = 10.0f;
    Vector3 targetDir;
    Vector3 randomDirection;

    void Start()
    {
        // Trigger random floating text animation
        anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        int ran = Random.Range(0, 2);
        anim.SetInteger("Index", ran);
        anim.SetTrigger("Animate");

        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randomizeIntensity.x, randomizeIntensity.x), 
            Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
        transform.LookAt(Camera.main.transform.position);

        smoothZSpeed = Random.Range(-smoothSpeed, smoothSpeed);
        Debug.Log(transform.name + " Random z" + smoothZSpeed);

        randomDirection = new Vector3(0, Random.Range(0, 5), Random.Range(-5, 5));
    }

    void Update() {
        smoothSpeed = Mathf.SmoothDamp(smoothSpeed, targetSpeed, ref speedRef, destroyTime);
        float step =  smoothSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, randomDirection, step);
//        transform.position = Vector3.MoveTowards(transform.position, targetDir, step);
//        transform.position += randomDirection * Time.deltaTime;
        //smoothZSpeed = Mathf.SmoothDamp(smoothZSpeed, targetSpeed, ref speedZRef, destroyTime/2);

        //transform.position += new Vector3(0, smoothSpeed/2, smoothZSpeed) * Time.deltaTime;
        // Object was spawned at the transfoms feet.
        // Move the object in a random direction up from the feet.
        // Slow the movement speed over time.
        // Stop a little bit before the destry timer, so that it appears to float for a momeny.
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
}
