using UnityEngine;
using TMPro;
public class NotesManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title, note;
    [SerializeField] GameObject noteObject;

    bool noteIsOpen;

    void Update()
    {
        // jezeli notaka jest towarta i zostanie nacisniete Escape wylacz zamknij notatke
        if(noteIsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            noteIsOpen = false;
            noteObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetNote(string title, string note)  
    {
        // ustaw wartosci notatki do wyswietlenia
        this.title.text = title;
        this.note.text = note;
        noteIsOpen = true;
        noteObject.SetActive(true);
        Time.timeScale = 0;

    }

    
}
