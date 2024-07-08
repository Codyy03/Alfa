using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClampText : MonoBehaviour
{

    [SerializeField] Vector3 offset;
    public  TextMeshProUGUI textLabel;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    // wyswietla tekst na ekranie niezale¿nie od pozycji gracza
    void Update()
    {
        Vector3 position = cam.WorldToScreenPoint(this.transform.position);
       
        textLabel.transform.position = position + offset;
    }
}
