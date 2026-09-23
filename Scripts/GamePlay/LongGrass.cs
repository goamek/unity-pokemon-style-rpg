using UnityEngine;
using UnityEngine.SceneManagement;

// Tall grass: 10% chance of a wild encounter per step (every step in the boss scene)
public class LongGrass : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] string allowedSceneName = "DungeonDos";

    public void OnPlayerTriggered(PlayerController player)
    {
        if (SceneManager.GetActiveScene().name == allowedSceneName)
        {
            GameController.Instance.StartBattle();
            return;
        }

        if (Random.Range(1, 101) <= 10)
            GameController.Instance.StartBattle();
    }
}
