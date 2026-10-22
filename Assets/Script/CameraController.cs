using UnityEngine;

public class CameraController : MonoBehaviour
{
    //usaré este objeto para conseguir sus coordenadas y establecer que la cámara siempre se mantenga a la misma
    //distancia del player
    public GameObject player;
    //Creamos un vector3 que tendrá guardada la diferencia entre las coordenadas del player y las de la camara
    //para que siempre se mantengan las mismas distancias
    private Vector3 diferenciaCamPlayer;

    
    void Start()
    {
        //Con el transform.position podemos acceder a la posicion de un GameObject y nos devuelve 
        this.diferenciaCamPlayer = this.transform.position - player.transform.position;
    }

    //Se actualiza 1 vez por frame, pero despues de ejecutarse el FixedUpdate que es quien aplica las fisicas
    //por lo tanto el movimiento a la bola, si lo ponemos update primero se mueve la cámara y luego se aplica
    //la fuerza que mueve la bola
    void LateUpdate()
    {
        //Vamos a hacer que la cámara se mueva en cada frame a la posición del player pero sumando su distancia
        //inicial, de esa forma siempre mantendremos la misma distancia con respecto a la bola
        this.transform.position = player.transform.position + this.diferenciaCamPlayer;
    }
}
