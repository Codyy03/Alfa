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

    // w��cz zapalniczke
    void EneableLighterLight()
    {
        light.SetActive(true);
        manager.PlayClip(startLighter);
    }
    // wy��cz zapalniczke
    public void DisableLighterLight()
    { light.SetActive(false); }

    
}
