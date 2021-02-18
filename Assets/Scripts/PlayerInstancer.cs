using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstancer : MonoBehaviour
{
    public int numPlayers;
    public GameObject player;
    public static Neuralnetwork bestNetwork;
    public static int num_players;
    public static PlayerInstancer playerInstancer_instance;
    public Text generation_text;
    public static int generation;

    private void Awake()
    {
        playerInstancer_instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        for(int i=0;i<numPlayers;i++)
        {
           GameObject currentPlayer =  Instantiate(player, transform.position, Quaternion.identity);
            currentPlayer.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            num_players += 1;
        }
        generation += 1;
        generation_text.text ="Generation: "+ generation.ToString();
    }

   public void createChildrens()
    {
        int a =(int) (numPlayers * (3.0f / 4.0f));
        Debug.Log(a);
        for (int i=0;i<a;i++)
        {
            GameObject currentPlayer = Instantiate(player, transform.position, Quaternion.identity);
            PlayerClass player_class = currentPlayer.GetComponent<PlayerClass>();
            player_class.myNeuralNetwork = bestNetwork;
            player_class.isChild = true;
            currentPlayer.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            num_players += 1;
        }
        int b = (int)(numPlayers * (1.0f / 4.0f));
        Debug.Log(b);
        for (int i = 0; i < b; i++)
        {
            GameObject currentPlayer = Instantiate(player, transform.position, Quaternion.identity);
            currentPlayer.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            num_players += 1;
        }
        generation += 1;
        generation_text.text = "Generation: " + generation.ToString();
    }

   



}
