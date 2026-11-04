using UnityEngine;

public class KeyboardToggle : MonoBehaviour
{
    public GameObject pitPrefab;

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
    }

    public void respawnPoint() {
        gameObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
    }
}