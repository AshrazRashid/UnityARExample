using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;  // Make sure this is assigned in the Inspector
    private GameObject markerInstance;

    void Start()
    {
        if (markerPrefab == null)
        {
            Debug.LogError("Marker prefab is not assigned!");
            return;
        }

        markerInstance = Instantiate(markerPrefab);
        markerInstance.SetActive(false); // Initially inactive
    }

    public void SetMarkerPosition(string positionData)
    {
        if (markerInstance == null)
        {
            Debug.LogError("Marker instance is not created!");
            return;
        }

        Vector3 position = JsonUtility.FromJson<Vector3>(positionData);
        markerInstance.transform.position = position;
        markerInstance.SetActive(true); // Activate when position is set
        Debug.Log("Marker position set to: " + position);
    }
}
