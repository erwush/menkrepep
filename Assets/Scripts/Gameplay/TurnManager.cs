using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public UiManager menu;
    public Player[] players;
    public Player activePlayer;
    public int currentIndex;
    public BoardManager board;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = UiManager.Instance;
        board = BoardManager.Instance;
        foreach(var player in players) player.star = 6;
        
    }

    void Awake()
    {

        Instance = this;
    }
    
    public void ConfirmAttack()
    {
        activePlayer.ConfirmAttack();
    }



    public void NextTurn()
    {
        //*NOTE: Nah aku cuman kepikiran tpi kekyna bakal ada bug dmn suatu efek ketrigger 2x karen ada di anyturnend sama selfturnend dsb
        currentIndex++;
        if (currentIndex >= players.Length) currentIndex = 0;
        foreach(var unit in activePlayer.activeUnits) unit.GetComponent<BoardObject>().OnSelfTurnEnd();
        foreach(var player in players)
        {

            player.ChangeStar(1);
            player.ChangeUltStar(1);
            player.EndAction();
            foreach(var unit in player.activeUnits) unit.GetComponent<BoardObject>().OnAnyTurnEnd();
            player.selectedObj = null;
            player.selectedTile = null;  
        };

        activePlayer = players[currentIndex];
        foreach (var unit in activePlayer.activeUnits) unit.GetComponent<BoardObject>().OnSelfTurnStart();
        foreach(var unit in activePlayer.activeUnits) unit.GetComponent<BoardObject>().OnAnyTurnStart();
        foreach (var tile in board.tiles) tile.player = activePlayer;
        if(menu.selectedDisplay.Count > 0) foreach(var disp in menu.selectedDisplay)
            {
                Debug.Log("aweweo");
                Destroy(disp.gameObject);
            }

        menu.selectedDisplay.Clear();
    }
    
    public void ChangeState(string stateName) 
    {
        activePlayer.ChangeState(stateName);
    }
}
