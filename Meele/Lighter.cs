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
    // Update is called once per frame
    void Update()
    {
        
    }

    void EneableLighterLight()
    {
        light.SetActive(true);
        manager.PlayClip(startLighter);
    }

    public void DisableLighterLight()
    { light.SetActive(false); }

    
}
