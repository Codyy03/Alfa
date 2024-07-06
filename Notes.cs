using UnityEngine;
using static WeaponsManager;

public class Notes : MonoBehaviour
{
    [SerializeField] float distanceToEneableTakeWeapon;
    [SerializeField] GameObject notificationCanvas;


    [SerializeField] string title;

    [TextAreaAttribute]
    [SerializeField] string note;


    Transform player;
    NotesManager notesManager;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notesManager = FindFirstObjectByType<NotesManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= distanceToEneableTakeWeapon)
        {
            notificationCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                notesManager.SetNote(title, note);
            }
        }
        else notificationCanvas.SetActive(false);
    }
}
