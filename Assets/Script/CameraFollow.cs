using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    // offset between target and camera
    [SerializeField]private Vector3 offset;

    

    [SerializeField] private float speed;

    [SerializeField] private Vector2 leftBottomBorder;

    [SerializeField] private Vector2 rightTOpBorder;

    private float height;

    private float weight;

    void Awake()
    {
        // Find the first target with class PlayerController
        target = FindFirstObjectByType<PlayerController>().transform;

        height =  Camera.main.orthographicSize;

        weight = height * (Screen.width / Screen.height);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        float clampedX = Mathf.Clamp(targetPosition.x, leftBottomBorder.x + weight*2, rightTOpBorder.x - weight*2);
        float clampedY =  Mathf.Clamp(targetPosition.y, leftBottomBorder.y + height, rightTOpBorder.y - height);
        // Smoothly move the camera towards the target position with an offset
        transform.position = Vector3.Lerp(transform.position, new Vector3(clampedX,clampedY, targetPosition.z), Time.deltaTime*speed);
        
       
    }
}
