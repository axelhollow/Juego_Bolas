using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    private Vector3 posicion=new Vector3(0,0,0);
    private string tipoBola;

    //Sprites
    public Sprite sprite_humano;
    public Sprite sprite_goblin;
    public Sprite sprite_granjero;
    public Sprite sprite_trasgo;
    public Sprite sprite_palo;
    public Sprite sprite_pico;
    public Sprite sprite_piedra;
    public Sprite sprite_espada;
    public Sprite sprite_guerrero;
    public Sprite sprite_orco;

    //Persoanjes
    public Bola bola_humano;
    public Bola bola_goblin;
    public Bola bola_granjero;
    public Bola bola_trasgo;
    public Bola bola_guerrero;
    public Bola bola_orco;
    public Bola bola_minero;


    //Herramientas y Armas
    public Bola bola_palo;
    public Bola bola_pico;
    public Bola bola_espada;

    //Elementos Naturales
    public Bola bola_piedra;
    public Bola bola_piedraGrande;
    public Bola bola_drill;

    //Otros
    private Bola bolaCreada;
    private Bola bolaCreada2;
    public int bolasiguiente=1;
    private int bolasiguienteAux=1;
    private Vector3 posicionBolaNueva;
    private bool permisoDeCreacion=false;
    public Camera cam;
    public SpriteRenderer iconoBolaSiguiente;
    float coolDown=0.3f;
    float lastTime=0;

    // Start is called before the first frame update
    void Start()
    {
        //iconoBolaSiguiente = iconoBolaSiguiente.GetComponent<SpriteRenderer>();
        Bola.CrearBola += GenerarBola;
    }


    public delegate void ActionTriggered(int value);
    public static event ActionTriggered MarcarBolaSiguiente;
    
    // Update is called once per frame
    void Update()
    {
        bolasiguiente = bolasiguienteAux;
        posicion = cam.ScreenToWorldPoint(Input.mousePosition);
        posicion.z = 0;
        posicion.y = 5.0f;

        if (Input.GetMouseButtonDown(0) && Time.time>lastTime+ coolDown && posicion.x> -21.5f && posicion.x < 5f)
        {

            if (bolasiguiente<11)bolaCreada = Instantiate(bola_humano, posicion, transform.rotation);
            if (bolasiguiente>10 && bolasiguiente < 41) bolaCreada = Instantiate(bola_goblin, posicion, transform.rotation);
            if (bolasiguiente > 40 && bolasiguiente < 51) bolaCreada = Instantiate(bola_humano, posicion, transform.rotation);
            if (bolasiguiente > 50 && bolasiguiente < 61) bolaCreada = Instantiate(bola_trasgo, posicion, transform.rotation);
            if (bolasiguiente > 60 && bolasiguiente < 71) bolaCreada = Instantiate(bola_palo, posicion, transform.rotation);
            if (bolasiguiente > 70 && bolasiguiente < 81) bolaCreada = Instantiate(bola_pico, posicion, transform.rotation);
            if (bolasiguiente > 80 && bolasiguiente < 91) bolaCreada = Instantiate(bola_piedra, posicion, transform.rotation);
            if (bolasiguiente > 90 && bolasiguiente < 101) bolaCreada = Instantiate(bola_espada, posicion, transform.rotation);
            //switch (bolasiguiente)
            //    {
            //        case 1:
            //            bolaCreada = Instantiate(bola_humano, posicion, transform.rotation);
            //            break;
            //        case 2:
            //            bolaCreada = Instantiate(bola_goblin, posicion, transform.rotation);

            //            break;
            //        case 3:
            //            bolaCreada = Instantiate(bola_granjero, posicion, transform.rotation);
            //            break;
            //        case 4:
            //            bolaCreada = Instantiate(bola_trasgo, posicion, transform.rotation);
            //            break;
            //        case 5:
            //            bolaCreada = Instantiate(bola_palo, posicion, transform.rotation);
            //            break;
            //        case 6:
            //            bolaCreada = Instantiate(bola_pico, posicion, transform.rotation);
            //            break;
            //        case 7:
            //            bolaCreada = Instantiate(bola_piedra, posicion, transform.rotation);
            //            break;
            //        case 8:
            //            bolaCreada = Instantiate(bola_espada, posicion, transform.rotation);
            //            break;
            //}
                
            
            bolaCreada.activar = true;
            bolaCreada.GetComponent<Rigidbody2D>().simulated = false;
            EsferaSiguiente();
            //MarcarBolaSiguiente.Invoke(bolasiguiente);
            PrediccionDeBola(bolasiguiente);
            lastTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            bolaCreada.GetComponent<Rigidbody2D>().simulated = true;
            bolaCreada.activar = false;

        }
        if (bolaCreada != null) MoverEnX(posicion);

        if (permisoDeCreacion == true)
        {
            if (tipoBola == "granjero")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_granjero, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "trasgo")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_trasgo, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "guerrero")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_guerrero, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "orco")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_orco, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "piedraGrande")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_piedraGrande, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "drill")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_drill, posicionBolaNueva, transform.rotation);
            }
            if (tipoBola == "minero")
            {
                bola_goblin.GetComponent<Rigidbody2D>().simulated = true;
                bolaCreada2 = Instantiate(bola_minero, posicionBolaNueva, transform.rotation);
            }


            permisoDeCreacion = false;
        }


    }
    public void MoverEnX(Vector3 vector3)
    {
        float x=vector3.x;
        float y = vector3.y;
        float z=vector3.z;
        if (bolaCreada.activar)
        {
            bolaCreada.transform.position = new Vector3(Mathf.Clamp(x,-20.8f,4.3f),y,z);
        }
    }


    public void EsferaSiguiente() 
    {
        //numeros entre el 1 y el 100
        int rng = Random.Range(1, 101);
        bolasiguiente = rng;
        bolasiguienteAux= rng;
        

    }

    public void PrediccionDeBola(int numeroSprite)
    {

        //switch (numeroSprite)
        //{
        //    case 1:
        //        iconoBolaSiguiente.sprite = sprite_humano;
        //        break;
        //    case 2:
        //        iconoBolaSiguiente.sprite = sprite_goblin;
        //        break;
        //    case 3:
        //        iconoBolaSiguiente.sprite = sprite_granjero;
        //        break;
        //    case 4:
        //        iconoBolaSiguiente.sprite = sprite_trasgo;
        //        break;
        //    case 5:
        //        iconoBolaSiguiente.sprite = sprite_palo;
        //        break;
        //    case 6:
        //        iconoBolaSiguiente.sprite = sprite_pico;
        //        break;
        //    case 7:
        //        iconoBolaSiguiente.sprite = sprite_piedra;
        //        break;
        //    case 8:
        //        iconoBolaSiguiente.sprite = sprite_espada;
        //        break;

        //}



        if (bolasiguiente < 11) iconoBolaSiguiente.sprite = sprite_humano;
        if (bolasiguiente > 10 && bolasiguiente < 41) iconoBolaSiguiente.sprite = sprite_goblin;
        if (bolasiguiente > 40 && bolasiguiente < 51) iconoBolaSiguiente.sprite = sprite_granjero;
        if (bolasiguiente > 50 && bolasiguiente < 61) iconoBolaSiguiente.sprite = sprite_trasgo;
        if (bolasiguiente > 60 && bolasiguiente < 71) iconoBolaSiguiente.sprite = sprite_palo;
        if (bolasiguiente > 70 && bolasiguiente < 81) iconoBolaSiguiente.sprite = sprite_pico;
        if (bolasiguiente > 80 && bolasiguiente < 91) iconoBolaSiguiente.sprite = sprite_piedra;
        if (bolasiguiente > 90 && bolasiguiente < 101) iconoBolaSiguiente.sprite = sprite_espada;


    }


    public void GenerarBola(Vector3 vector3, string tipo) 
    {
        tipoBola = tipo;
        posicionBolaNueva = vector3;
        permisoDeCreacion = true;
    }
}
