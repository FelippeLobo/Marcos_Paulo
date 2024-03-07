using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O objeto que a câmera seguirá (o personagem, por exemplo)
    public Vector3 offset = new Vector3(0f, 2f, -5f); // A distância entre a câmera e o personagem

    public float mouseSensitivity = 100f; // Sensibilidade do mouse para controle de rotação da câmera

    public float minHeight = 1f; // Altura mínima da câmera em relação ao chão
    public float maxHeight = 5f; // Altura máxima da câmera em relação ao personagem
    public float verticalAngleLimit = 45f; // Limite vertical de rotação da câmera

    private float mouseX;
    private float mouseY;

    public float smoothSpeed = 0.125f; // A suavidade de movimento da câmera

    void Start()
    {
        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target not assigned!");
            return;
        }

        // Captura a entrada do mouse
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -verticalAngleLimit, verticalAngleLimit); // Limita o ângulo de rotação da câmera

        // Aplica a rotação à câmera
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.rotation = rotation;

        // Move a câmera para a posição desejada
        Vector3 desiredPosition = target.position - (rotation * offset);
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, offset.magnitude))
        {
            if (hit.distance < minHeight)
            {
                desiredPosition = target.position + (rotation * Vector3.up * minHeight);
            }
            else if (hit.distance > maxHeight)
            {
                desiredPosition = target.position + (rotation * Vector3.up * maxHeight);
            }
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Mantém a rotação do personagem igual à rotação da câmera, exceto para a rotação em y
        Vector3 characterRotation = target.rotation.eulerAngles;
        characterRotation.y = mouseX;
        target.rotation = Quaternion.Euler(characterRotation);
    }
}