using UnityEngine;

public class SetFire : MonoBehaviour
{
    [SerializeField] int percentToStartAFire;
    [SerializeField] GameObject firePrefab;
    [SerializeField] AudioClip fireClip;


   

    private void Awake()
    {
       
    }
    public bool CanBurn()
    {

        int random = Random.Range(0, 100);

        if(random <= percentToStartAFire)
            return true;

        return false;


    }

    public void CreateFire(Transform parent, Vector3 plusPosition,AudioSource audioSource)
    {
        GameObject fire;
        fire = Instantiate(firePrefab,parent);
        fire.transform.localPosition += plusPosition;
        audioSource.PlayOneShot(fireClip);
    }


}