using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntuaciones : MonoBehaviour
{

    public TextMeshProUGUI puntuacion;
    // Start is called before the first frame update
    void Start()
    {
        puntuacion.text = "0";
        Bola.SumarPunto += AumentarPuntuacion;
        PlayerPrefs.GetInt("Puntos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AumentarPuntuacion(int punto) 
    {
        int puntuacionActual = int.Parse(puntuacion.text);
        puntuacionActual += punto;
        puntuacion.text = puntuacionActual.ToString();
        PlayerPrefs.SetInt("Puntos", puntuacionActual);
        PlayerPrefs.Save();
    }
}
