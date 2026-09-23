using UnityEngine;

// Spawns the essential objects prefab if none exist yet, so any scene can be played directly
public class EssentialObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        var existingObjects = FindObjectsOfType<EssentialObjects>();

        if (existingObjects.Length == 0)
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
