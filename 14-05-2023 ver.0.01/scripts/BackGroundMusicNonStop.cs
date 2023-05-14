using UnityEngine;

public class BackGroundMusicNonStop : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
