using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static Manager;

public class Bola : MonoBehaviour
{
    public bool activar = false;
    public GameObject Escena;
    public Bola bolaGenerada;

    public delegate void TriggerdPuntos(int punto);
    public static event TriggerdPuntos SumarPunto;

    public delegate void ActionTriggered(Vector3 value, string tipo);
    public static event ActionTriggered CrearBola;

    private static HashSet<GameObject> processedBalls = new HashSet<GameObject>(); // Global para todas las bolas
    private bool isProcessing = false; // Asegura que la bola no procese más de una vez

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;

        //FUSIONES

        //Humano + Palo
        if (otherBall.tag == "Humano" && this.gameObject.tag == "Palo" || otherBall.tag == "Palo" && this.gameObject.tag == "Humano")
        {
            CrearBola?.Invoke(transform.position, "granjero");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        // Goblin + Palo
        if (otherBall.tag == "Goblin" && this.gameObject.tag == "Palo" || otherBall.tag == "Palo" && this.gameObject.tag == "Goblin")
        {
            CrearBola?.Invoke(transform.position, "trasgo");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        //Humano + Espada
        if (otherBall.tag == "Granjero" && this.gameObject.tag == "Espada" || otherBall.tag == "Espada" && this.gameObject.tag == "Granjero")
        {
            CrearBola?.Invoke(transform.position, "guerrero");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        // Goblin + Espada
        if (otherBall.tag == "Trasgo" && this.gameObject.tag == "Espada" || otherBall.tag == "Espada" && this.gameObject.tag == "Trasgo")
        {
            CrearBola?.Invoke(transform.position, "orco");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        //Pico + Piedra
        if (otherBall.tag == "Pico" && this.gameObject.tag == "Piedra" || otherBall.tag == "Piedra" && this.gameObject.tag == "Pico")
        {
          
            Destroy(otherBall);
            Destroy(gameObject);
        }

        //Piedras + Piedra
        if (otherBall.tag == "Piedra" && this.gameObject.tag == "Piedra")
        {
            CrearBola?.Invoke(transform.position, "piedraGrande");
            Destroy(otherBall);
            Destroy(gameObject);
        }
        // Pico + Humano
        if (otherBall.tag == "Pico" && this.gameObject.tag == "Humano" || otherBall.tag == "Humano" && this.gameObject.tag == "Pico")
        {
            CrearBola?.Invoke(transform.position, "minero");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        //Minero + Piedra
        if (otherBall.tag == "Minero" && this.gameObject.tag == "Piedra" || otherBall.tag == "Piedra" && this.gameObject.tag == "Minero")
        {

            if (this.gameObject.tag == "Piedra") Destroy(gameObject);
            else Destroy(otherBall);
        }


        // Pico + Pico
        if (otherBall.tag == "Pico" && this.gameObject.tag == "Pico")
        {
            CrearBola?.Invoke(transform.position, "drill");
            Destroy(otherBall);
            Destroy(gameObject);
        }

        //Drill + PiedraGrande
        if (otherBall.tag == "Drill" && this.gameObject.tag == "PiedraGrande" || otherBall.tag == "PiedraGrande" && this.gameObject.tag == "Drill")
        {

            Destroy(otherBall);
            Destroy(gameObject);
        }

        //COMBATE

        // Granjero > Goblin
        if (otherBall.tag == "Granjero" && this.gameObject.tag == "Goblin" || otherBall.tag == "Goblin" && this.gameObject.tag == "Granjero")
        {
           if(this.gameObject.tag == "Goblin") Destroy(gameObject);
           else Destroy(otherBall);
        }

        //Trasgo > Humano
        if (otherBall.tag == "Trasgo" && this.gameObject.tag == "Humano" || otherBall.tag == "Humano" && this.gameObject.tag == "Trasgo")
        {
            if (this.gameObject.tag == "Humano") Destroy(gameObject);
            else Destroy(otherBall);
        }

        //Guerrero > Trasgo
        if (otherBall.tag == "Guerrero" && this.gameObject.tag == "Trasgo" || otherBall.tag == "Trasgo" && this.gameObject.tag == "Guerrero")
        {
            if (this.gameObject.tag == "Trasgo") Destroy(gameObject);
            else Destroy(otherBall);
        }

        //Orco > Granjero
        if (otherBall.tag == "Orco" && this.gameObject.tag == "Granjero" || otherBall.tag == "Granjero" && this.gameObject.tag == "Orco")
        {
            if (this.gameObject.tag == "Granjero") Destroy(gameObject);
            else Destroy(otherBall);
        }


        //if (otherBall.CompareTag(gameObject.tag) && !isProcessing && !processedBalls.Contains(otherBall))
        //{
        //    // Marcar esta bola y la otra como procesadas
        //    processedBalls.Add(gameObject);
        //    processedBalls.Add(otherBall);


        //    isProcessing = true; // Evita múltiples colisiones para esta bola

        //    // Invocar el evento para crear una nueva bola
        //    //CrearBola?.Invoke(transform.position, gameObject.tag);

        //    // Otorgar puntos según el tipo de bola
        //    switch (gameObject.tag)
        //    {
        //        case "Bola1":
        //            SumarPunto?.Invoke(100);
        //            break;
        //        case "Bola2":
        //            SumarPunto?.Invoke(200);
        //            break;
        //        case "Bola3":
        //            SumarPunto?.Invoke(300);
        //            break;
        //        case "Bola4":
        //            SumarPunto?.Invoke(400);
        //            break;
        //        case "Bola5":
        //            SumarPunto?.Invoke(500);
        //            break;
        //    }

        //    // Destruir ambas bolas con un retraso para asegurar que no procesen más eventos
        //    Destroy(otherBall);
        //    Destroy(gameObject);
        //}
    }

    private void OnDestroy()
    {
        // Limpiar el registro de bolas procesadas al destruir
        processedBalls.Remove(gameObject);
    }
}
