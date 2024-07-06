using UnityEngine;
using TMPro;
public class NotesManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title, note;
    [SerializeField] GameObject noteObject;

    bool noteIsOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(noteIsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            noteIsOpen = false;
            noteObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetNote(string title, string note)  
    {
        this.title.text = title;
        this.note.text = note;
        noteIsOpen = true;
        noteObject.SetActive(true);
        Time.timeScale = 0;

    }

    
}
