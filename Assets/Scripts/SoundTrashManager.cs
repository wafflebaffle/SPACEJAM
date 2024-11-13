using UnityEngine;

public class SoundTrashManager : MonoBehaviour
{
    void Start()
    {
        float range = Random.Range(0f, 0.1f);
        range -= 0.05f;
        GetComponent<AudioSource>().pitch += range;
        Invoke(nameof(DeleteThis), 1.0f);
    }

    private void DeleteThis()
    {
        Destroy(gameObject);
    }
}
