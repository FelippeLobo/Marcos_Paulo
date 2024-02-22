using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private float jumpForce = 5f; // Força do pulo
    private float moveSpeed = 5f;
    private Rigidbody rb; // Referência ao Rigidbody do personagem
    private Vector3 movement; // Vetor de movimento

    public Transform groundCheck; // Ponto para verificar se está no chão
    private float groundDistance = 0.4f; // Distância para considerar que está no chão
    public LayerMask groundMask; // Layer do chão
    private bool isGrounded; // Está no chão?

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checa se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Captura o input do teclado
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Cria um vetor de movimento baseado nos inputs
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Detecta o input de pulo (tecla Espaço) e se está no chão
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Move o personagem
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        // Aplica o movimento ao Rigidbody para mover o personagem
        rb.MovePosition(transform.position + (moveSpeed * Time.fixedDeltaTime * direction));
    }
}
