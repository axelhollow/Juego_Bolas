using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEscenas : MonoBehaviour
{

 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    public void CargarGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void CararMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }


}
