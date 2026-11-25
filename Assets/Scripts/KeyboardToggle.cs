using UnityEngine;
using TMPro;

using UnityEngine.Splines;

public class KeyboardToggle : MonoBehaviour
{
    public GameObject pitPrefab;
    public TextMeshPro textPrefab;
    TextMeshPro temp;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    public SplineContainer splineContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temp = Instantiate(textPrefab, spawnPosition + new Vector3(0, 0.6f, 0), spawnRotation);
        temp.text = "Spawn Position: " + spawnPosition.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Will Change this to spawn based on the position of the car
    public void spawnPitStop(Vector3 position, Quaternion rotation)
    {
        Quaternion offset = Quaternion.Euler(0f, 90f, 0f);
        Quaternion finalRotation = rotation * offset;
        GameObject temp = Instantiate(pitPrefab, position, finalRotation);
    }

    public void assignResetPoint(GameObject gameObject) {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        Vector3 tempSet = new Vector3(0, 0.6f, 0);

        temp.transform.SetPositionAndRotation(spawnPosition + tempSet, spawnRotation);
        temp.text = "Spawn Position: " + spawnPosition.ToString();
    }

    public void assignWayPoint(GameObject gameObject)
    {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        Vector3 tempSet = new Vector3(0, 0.6f, 0);

        TextMeshPro wayPointTemp = Instantiate(textPrefab, spawnPosition + tempSet, spawnRotation);
        wayPointTemp.text = "WayPoint: " + Vector3.Distance(spawnPosition, textPrefab.transform.position);
    }

    public void respawnPoint() {
        if (temp != null)
        {
            gameObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
        }
    }
}

// Try to make the track editable in runtime
// Respawn Automatically (and also try to hold the reset button for at least 2 seconds instead of continously pressing)
// Spawn Pit Stops based on the car position