using UnityEngine;

// Root of objects that persist across scenes (player, game controller, UI)
public class EssentialObjects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
