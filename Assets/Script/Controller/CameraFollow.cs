using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    // offset between target and camera
    [SerializeField]private Vector3 offset;

    [SerializeField] private float speed;


    void Awake()
    {
        // Find the first target with class PlayerController
        target = FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Smoothly move the camera towards the target position with an offset
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime*speed);
    }
}
