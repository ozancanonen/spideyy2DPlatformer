using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField] List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    private Vector3 velocity;
    private Camera cam;

    [Header("ZoomInOut")]
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            Debug.LogWarning("There is no target attached to camera");
            return;
        }
        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
