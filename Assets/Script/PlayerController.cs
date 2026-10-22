//En C# las librerias se llaman namespaces y se añaden utilizando using, es como el import de Java.
//L libreria InputSystem se utiliza para manejar las entradas de Jugador, como pulsat una tecla o mover
//el joystick. IMPORTANTE: Necesitamos añadir un PlayerInput en el panel inspector asociado al elemento
//que deseamos mover, en este caso el Player.
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //En esta zona vamos a declarar los atributos de la clase, vamos a declarar un atributo de tipo
    //Rigidbody, lo haremos para que no se pueda acceder desde otro Script o desde el inspector de Unity
    //Lo utilizamos para interactuar con las fisica del motor de Unity, colisiones y fuerzas.
    //Podemos mover de dos formas la pelota, cambiando su posicion x,y,z o aplicando una fuerza, si usamos
    //segunda podremos interactuar con otros objetos de mejor forma.
    private Rigidbody rb;
    //Va a contar cuantos pickup he cogido
    private int count;
    //Creamos una variable para almacenar el movimiento horizontal X.
    private float movementX;
    //Creamos otra para almacenar el movimiento vertical(y pero sabemos que en realidad en el plano 3d movemos
    //la z
    private float movementY;
    //Creamos una variable speed para ajustar la velocidad, la creamos pública para poder cambiarla desde el 
    //inspector
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public int nPickUps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Camos a traernos el RigidBody del GameObject que llama a este Script, es importante que ese GameObject
        //tenga asociado un RigidBody, sino fallará. Podemos acceder a ls elementos físicos del GameObject
        //usando el RigidBody
        this.rb = GetComponent<Rigidbody>();
        this.count = 0;
        nPickUps = GameObject.FindGameObjectsWithTag("PickUp").Length;
        SetCountText();
        winTextObject.SetActive(false);
        
    }

    //El metodo OnMove será llamado cuando se pulse una tecla o se toque el joystick que tiene asociado
    //el PlayerInput que hemos añadido al objeto que llama a este Script. El metodo recibe un parámetro de
    //tipo InputValue que guardará la información de la pulsación
    void OnMove(InputValue movementValue)
    {
        //Sacamos por consola la deteccion de la pulsación de una tecla
        Debug.Log($"Movimiento detectado: {movementValue}");
        //Vamos a crear un vector de 2 dimensiones que almacene en coordenadas las pulsaciones que
        //me llegan del InputValue
        Vector2 movementVector = movementValue.Get<Vector2>();
        //guardamos en los atributos movement X e Y , lo hacemos porque los vamos
        //a usar fuera de este método
        this.movementX = movementVector.x;
        this.movementY = movementVector.y;
        //Vamos a ver los valores en la consola
        Debug.Log($"Valor del vector X: {movementX}");
        Debug.Log($"Valor del vector Y: {movementY}");

    }
    void SetCountText()
    {
        this.countText.text = "Score: " + this.count.ToString();
        if (this.count == this.nPickUps)
        {
            this.winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Time.timeScale = 0f;
        }
    }
    //Usamos el metodo FixedUpdate siempre que queremos hacer actualizaciones físicas en nuestros elementos
    private void FixedUpdate()
    {
        //Vamos a crear un Vector3 (3D) porque lo necesitamos para aplicar fuerzas con RigidBody 
        //Sin embargo este Vector tiene componente X, Y, Z. La Y representa el plano vertical que nuestro juego
        //no contempla (nos movemos en 2D) por lo que dejamos el plano Y del V· a 0 y el Z lo ponemos
        //a lo que en nuestro Script es el movementY
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //Aplicamos una fuerza multiplicada por la velocidad sobre el Rigidbody del GameObject
        //para controlar la intensidad del movimiento, 
        //
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Para evitar qeue desaparezcan todos los elementos trigger, lo hemos tageado con la etiqueta pickUp, y
        //hacemos la comprobacion con in if, si el elemento esta tageado como pickUp.
        //Solo desaparece el elemento si esta tageado como pickUp
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            this.count++;
            Debug.Log($"Objetos recogidos: {this.count}");
        }
        SetCountText();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            this.winTextObject.SetActive(true);
            this.winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        Debug.Log($"Colision detectada con: {collision.gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
