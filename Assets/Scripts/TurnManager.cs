using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public Player[] players;
    public Player activePlayer;
    public int currentIndex;
    public BoardManager board;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board = BoardManager.Instance;
        
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTurn()
    {
        currentIndex++;
        if (currentIndex >= players.Length) currentIndex = 0;
        foreach(var player in players)
        {
            player.ChangeState("Idle");
            player.selectedObj = null;
            player.selectedTile = null;  
        };
        
        activePlayer = players[currentIndex];
        foreach (var tile in board.tiles) tile.player = activePlayer;
    }
    
    public void ChangeState(string stateName) 
    {
        activePlayer.ChangeState(stateName);
    }
}
