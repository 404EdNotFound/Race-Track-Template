using UnityEngine;
using TMPro;

public class KeyboardToggle : MonoBehaviour
{
    public GameObject pitPrefab;
    public TextMeshPro textPrefab;
    TextMeshPro temp;

    [SerializeField] private float coolDownValue;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float PitCoolDown = 0f;
    private float WayPointCoolDown = 0f;
    private int pitStopCounter = 0;
    private int wayPointCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiates a reset point in case the user needs to restart wherever possible
        temp = Instantiate(textPrefab, spawnPosition + new Vector3(0, 0.6f, 0), spawnRotation);
        temp.text = "Spawn Position: " + spawnPosition.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CoolDown();
        Debug.Log(pitStopCounter);
        Debug.Log(wayPointCounter);
    }

    private void CoolDown()
    {
        if (PitCoolDown == 0) { return; }
        PitCoolDown = Mathf.MoveTowards(PitCoolDown, 0f, Time.deltaTime);
        Debug.Log(PitCoolDown);

        if (WayPointCoolDown == 0) { return; }
        WayPointCoolDown = Mathf.MoveTowards(WayPointCoolDown, 0f, Time.deltaTime);
        Debug.Log(WayPointCoolDown);
    }

    // Used to spawn a pitstop relative to the transformation of the car
    public void spawnPitStop(Vector3 position, Quaternion rotation)
    {
        if (PitCoolDown == 0)
        {
            Quaternion offset = Quaternion.Euler(0f, 90f, 0f);
            Quaternion finalRotation = rotation * offset;
            GameObject temp = Instantiate(pitPrefab, position, finalRotation);
            PitCoolDown = coolDownValue;
            pitStopCounter++;
        }

        else return;
    }

    // Used to set a marker point for the user the respawn
    public void assignResetPoint(GameObject gameObject) {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        Vector3 tempSet = new Vector3(0, 0.6f, 0);

        temp.transform.SetPositionAndRotation(spawnPosition + tempSet, spawnRotation);
        temp.text = "Spawn Position: " + spawnPosition.ToString();
    }

    // Used to assign a waypoint
    public void assignWayPoint(GameObject gameObject)
    {
        if (WayPointCoolDown == 0)
        {
            spawnPosition = gameObject.transform.position;
            spawnRotation = gameObject.transform.rotation;
            Vector3 tempSet = new Vector3(0, 0.6f, 0);

            TextMeshPro wayPointTemp = Instantiate(textPrefab, spawnPosition + tempSet, spawnRotation);
            wayPointTemp.text = "WayPoint: " + Vector3.Distance(spawnPosition, textPrefab.transform.position);

            WayPointCoolDown = coolDownValue;
            wayPointCounter++;
        }

        else return;

    }

    // Respawns the car at the assigned position and rotation
    public void respawnPoint() 
    {
        gameObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
    }
}

// Try to make the track editable in runtime
// Respawn Automatically (and also try to hold the reset button for at least 2 seconds instead of continously pressing)
// Implement Cooldown Timer for Spawning pits to prevent many pits spawning at once (same with assigning waypoints)
// Try to calculate real time distance with these waypoints!