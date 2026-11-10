using UnityEngine;
using TMPro;

public class KeyboardToggle : MonoBehaviour
{
    public GameObject pitPrefab;
    public TextMeshPro textPrefab;
    TextMeshPro temp;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPitStop(Vector3 position, Quaternion rotation)
    {
        Quaternion offset = Quaternion.Euler(0f, 90f, 0f);
        Quaternion finalRotation = rotation * offset;
        GameObject temp = Instantiate(pitPrefab, position, finalRotation);
    }

    public void assignResetPoint(GameObject gameObject) {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        Vector3 tempSet = new Vector3(0, 0.5f, 0);

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
        Vector3 tempSet = new Vector3(0, 0.5f, 0);

        TextMeshPro wayPointTemp = Instantiate(textPrefab, spawnPosition + tempSet, spawnRotation);
        wayPointTemp.text = "WayPoint: " + Vector3.Distance(wayPointTemp.transform.position, spawnPosition);
    }

    public void respawnPoint() {
        gameObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
    }
}