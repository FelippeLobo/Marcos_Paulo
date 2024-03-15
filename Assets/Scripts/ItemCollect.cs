using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarItem : MonoBehaviour
{
    // Tag do objeto que o personagem pode coletar
    public string itemTag = "Item";

    // Função chamada quando um objeto colide com o jogador
    private void OnTriggerEnter(Collider other)
    {
         Debug.Log("Munição recarregada!");
        // Verifica se o objeto que o jogador colidiu tem a tag do item
        if (other.CompareTag(itemTag))
        {
           
            // Executa a ação correspondente ao item
            ColetarAcao(other.gameObject);

            // Remove o objeto coletado
            Destroy(other.gameObject);
        }
    }

    // Função que executa a ação correspondente ao item
    private void ColetarAcao(GameObject item)
    {
        // Você pode implementar diferentes ações para diferentes tipos de itens
        // Por exemplo, verificar o nome do item e executar uma ação correspondente
        // Exemplo: Se o item for uma "Munição", recarregar a munição
        if (item.name == "Municao")
        {
            RecarregarMunicao();
        }
        // Exemplo: Se o item for um "ItemDeVida", curar o personagem
        else if (item.name == "ItemDeVida")
        {
            CurarPersonagem();
        }
        // Adicione outros casos conforme necessário
    }

    // Função para recarregar a munição
    private void RecarregarMunicao()
    {
        // Implemente a lógica para recarregar a munição aqui
        Debug.Log("Munição recarregada!");
    }

    // Função para curar o personagem
    private void CurarPersonagem()
    {
        // Implemente a lógica para curar o personagem aqui
        Debug.Log("Personagem curado!");
    }
}
