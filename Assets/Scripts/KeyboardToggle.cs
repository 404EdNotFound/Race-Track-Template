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
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Will Change this to spawn based on the position of the car
    public void spawnPitStop(Vector3 position, Quaternion rotation)
    {
        for (float t = 0; t <= 1; t += 0.1f)
        {
            Vector3 splinePos = splineContainer.EvaluatePosition(t);
            Vector3 splineTan = splineContainer.EvaluateTangent(t);
            Quaternion rotationOffset = Quaternion.Euler(0, 90, 0);
            Vector3 finalRotation = rotationOffset * splineTan.normalized;
            Vector3 positionOffset = new Vector3(2f, 0f, 1f);
            Vector3 finalPosition = splinePos + finalRotation * -1f;
            GameObject temp = Instantiate(pitPrefab, finalPosition, Quaternion.LookRotation(splineTan) * rotationOffset);
        }
    }

    public void assignResetPoint(GameObject gameObject) {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        Vector3 tempSet = new Vector3(0, 0.6f, 0);

        if (temp == null)
        {
            temp = Instantiate(textPrefab, spawnPosition + tempSet, spawnRotation);
        }
        else
        {
            temp.transform.SetPositionAndRotation(spawnPosition + tempSet, spawnRotation);
        }
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
            gameObject.transform.SetPositionAndRotation(temp.transform.position, temp.transform.rotation);
        }
    }
}

// Try to make the track editable in runtime
// Respawn Automatically (and also try to hold the reset button for at least 2 seconds instead of continously pressing)
// Spawn Pit Stops based on the car position
// Remove existing checkpoints
// Think about Spawning Required Checkpoints