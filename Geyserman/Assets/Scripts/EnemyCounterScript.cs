using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterScript : MonoBehaviour
{
    
    public int enemyCounter = 0;
    
    public int actualSection = 1;

    public Text counterText;
    public Text missionText;
    public Text toDoText;

    public GameObject[] primeraSeccion;
    public GameObject[] parqueSeccion;
    public GameObject[] segundaSeccion;
    public GameObject[] puenteSeccion;
    public GameObject[] terceraSeccion;
    public GameObject[] estadioSeccion;

    public GameObject volcanoWoman;

    void Start()
    {
        enemyCounter = 0;
        actualSection = 1;
        counterText.text = "Sección " + actualSection.ToString() + ": " + enemyCounter.ToString() + "/" + primeraSeccion.Length.ToString();
        missionText.text = "Misión 1:";
        toDoText.text = "Acaba con todos los enemigos Kihax de las secciones y lugares!";
        volcanoWoman.SetActive(false);
    }

    void Update()
    {

    }

    public void UpdateCounter() 
    {

        if (enemyCounter < primeraSeccion.Length && actualSection == 1)
        {
            enemyCounter += 1;
            counterText.text = "Sección " + actualSection.ToString() + ": " + enemyCounter.ToString() + "/" + primeraSeccion.Length.ToString();
        }
        else if (enemyCounter == primeraSeccion.Length && actualSection == 1)
        {
            enemyCounter = 1;
            actualSection += 1;
            counterText.text = "Parque: " + enemyCounter.ToString() + "/" + parqueSeccion.Length.ToString();
        }
        else if (enemyCounter < parqueSeccion.Length && actualSection == 2)
        {
            enemyCounter += 1;
            counterText.text = "Parque: " + enemyCounter.ToString() + "/" + parqueSeccion.Length.ToString();
        }
        else if (enemyCounter == parqueSeccion.Length && actualSection == 2)
        {
            enemyCounter = 1;
            actualSection += 1;
            counterText.text = "Sección " + (actualSection - 1).ToString() + ": " + enemyCounter.ToString() + "/" + segundaSeccion.Length.ToString();
        }
        else if (enemyCounter < segundaSeccion.Length && actualSection == 3)
        {
            enemyCounter += 1;
            counterText.text = "Sección " + (actualSection - 1).ToString() + ": " + enemyCounter.ToString() + "/" + segundaSeccion.Length.ToString();
        }
        else if (enemyCounter == segundaSeccion.Length && actualSection == 3)
        {
            enemyCounter = 1;
            actualSection += 1;
            counterText.text = "Puente: " + enemyCounter.ToString() + "/" + puenteSeccion.Length.ToString();
        }
        else if (enemyCounter < puenteSeccion.Length && actualSection == 4)
        {
            enemyCounter += 1;
            counterText.text = "Puente: " + enemyCounter.ToString() + "/" + puenteSeccion.Length.ToString();
        }
        else if (enemyCounter == puenteSeccion.Length && actualSection == 4) 
        {
            enemyCounter = 1;
            actualSection += 1;
            counterText.text = "Sección " + (actualSection - 1).ToString() + ": " + enemyCounter.ToString() + "/" + terceraSeccion.Length.ToString();
        }
        else if (enemyCounter < terceraSeccion.Length && actualSection == 5)
        {
            enemyCounter += 1;
            counterText.text = "Sección " + (actualSection - 1).ToString() + ": " + enemyCounter.ToString() + "/" + terceraSeccion.Length.ToString();
        }
        else if (enemyCounter == terceraSeccion.Length && actualSection == 5)
        {
            enemyCounter = 1;
            actualSection += 1;
            counterText.text = "Estadio: " + enemyCounter.ToString() + "/" + estadioSeccion.Length.ToString();
        }
        else if (enemyCounter < estadioSeccion.Length && actualSection == 6)
        {
            enemyCounter += 1;
            counterText.text = "Estadio: " + enemyCounter.ToString() + "/" + estadioSeccion.Length.ToString();
        }
        else if (enemyCounter == estadioSeccion.Length && actualSection == 6)
        {
            missionText.text = "Misión 2:";
            toDoText.text = "Derrota a Volcano Woman!";
            volcanoWoman.SetActive(true);
            enemyCounter = 0;
            actualSection += 1;
            counterText.text = " ";
        }
    }
}
