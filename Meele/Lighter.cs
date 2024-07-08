using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] AudioClip startLighter;

    AudioManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<AudioManager>();
    }

    // w³¹cz zapalniczke
    void EneableLighterLight()
    {
        light.SetActive(true);
        manager.PlayClip(startLighter);
    }
    // wy³¹cz zapalniczke
    public void DisableLighterLight()
    { light.SetActive(false); }

    
}
