using UnityEngine;

public class Rotator : MonoBehaviour
{
    //Vamos a hacer que el objeto rote sobre su propio eje, por tanto no necesitamos
    //coordenadas de ningun otro objeto. Usamos el método Update porque  las físicas no tienen
    //nada que ver y queremos que se ejecute cuando cambia el frame

    // Update is called once per frame
    void Update()
    {
        //Vamos a rotar el objeto en todos sus ejes, para ello usamos transform.Rotate que pide como
        //como argumento un Vector 3D con los valores de rotacion. Luego multiplicaremos ese valor por
        //Time.deltaTime para que se calcule el tiempo entre Frame y Frame y dar suavidad a la rotacion
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime*1.5f);
    }
}
