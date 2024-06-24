using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab; // Ensure this is assigned in the Inspector
    private GameObject markerInstance;
    private ARRaycastManager raycastManager;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();

        // Instantiate markerInstance only if markerPrefab is assigned
        if (markerPrefab != null)
        {
            markerInstance = Instantiate(markerPrefab);
            markerInstance.SetActive(false); // Initially inactive
        }
        else
        {
            Debug.LogError("markerPrefab is not assigned in the Inspector");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(touch.position, hits, TrackableType.Planes);

                if (hits.Count > 0)
                {
                    Pose hitPose = hits[0].pose;
                    markerInstance.transform.position = hitPose.position;
                    markerInstance.SetActive(true); // Activate marker
                }
            }
        }
    }

    public void SetMarkerPosition(string positionData)
    {
        Vector3 position = JsonUtility.FromJson<Vector3>(positionData);
        markerInstance.transform.position = position;
        markerInstance.SetActive(true); // Activate when position is set
        Debug.Log("Marker position set to: " + position);
    }
}
