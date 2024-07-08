using UnityEngine;
using static WeaponsManager;

public class Notes : MonoBehaviour
{
    [SerializeField] float distanceToEneableTakeWeapon;
    [SerializeField] GameObject notificationCanvas;


    [SerializeField] string title;

    [TextAreaAttribute]
    [SerializeField] string note;


    bool noteCanBeOpen;
    Transform player;
    NotesManager notesManager;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notesManager = FindFirstObjectByType<NotesManager>();
    }

    void Update()
    {
        
        // jezeli notatka moze byc przeczytana i gracz nasicnie 'e' otworz notatke
        if (noteCanBeOpen)
        {
            notificationCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                notesManager.SetNote(title, note);
                notificationCanvas.SetActive(false);
            }
        }
        else notificationCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // jezeli gracz znajduje sie w strefie notatki wlacz mozliwosc jej przeczytania
        if(other.gameObject.CompareTag("Player"))
            noteCanBeOpen = true;
    }

    // wylacz mozliwosc przeczytania notatki
    private void OnTriggerExit(Collider other)
    {
        noteCanBeOpen = false;
    }
}
