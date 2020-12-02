using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [Header ("Size")]
    [SerializeField]
    public int xSize = 5;
    [SerializeField]
    public int ySize = 7;

    [Header ("Tiles")]
    [SerializeField]
    public List<TileType> tileTypes = new List<TileType> ();
    [SerializeField]
    private GameObject matchPieceObject = null;

    [Header ("Other")]
    [SerializeField]
    private Transform FirstTilePos = null;

    public GameObject TypeMenu;
    private TileType selectedtype;
    private bool done = false;
    private Vector2 offset;
    float xOffset, yOffset;
    float startX, startY;
    private bool initpos = false, goalpos = false;
    private Tile[, ] board, temp;
    private Tile Ourtile, Goaltile, OurTempTile;
    private GameManager gameMgr;
    private int moves;
    private List<Tile> Path = new List<Tile> ();
    private List<Tile> isvisited = new List<Tile> ();
    private bool pathfound;
    public static Board instance;
    public static System.Random rnd = new System.Random ();

    #region     
    public Board () {
        instance = this;
    }
    #endregion
    public void Init (GameManager gameMgr) {
        this.gameMgr = gameMgr;
        moves = gameMgr.hearts;
        DestroyAllTiles ();
        startX = FirstTilePos.transform.position.x;
        startY = FirstTilePos.transform.position.y;

        offset = matchPieceObject.GetComponent<SpriteRenderer> ().bounds.size;
        xOffset = offset.x - 0.2f;
        yOffset = offset.y - 0.2f;
        StartCoroutine (CreateBoard ());

    }

    private IEnumerator CreateBoard () {
        if (FirstTilePos == null)
            Debug.LogError ("[Board] Start position is empty!");

        board = new Tile[xSize, ySize];
        temp = new Tile[xSize, ySize];

        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                //Debug.Log ("board [" + x + "]" + "[" + y + "]" + ", X : " + startX + (xOffset * x));
                //Debug.Log ("board [" + x + "]" + "[" + y + "]" + ", Y : " + startY + (yOffset * y));

                var tile = Instantiate (
                    matchPieceObject,
                    new Vector3 (startX + (xOffset * x), startY + (yOffset * y), 2),
                    matchPieceObject.transform.rotation,
                    FirstTilePos).AddComponent<Tile> ();

                yield return StartCoroutine (Show_Type (true));

                // List<TileType> possibleTypes = new List<TileType>();
                // possibleTypes.AddRange(tileTypes);
                if (selectedtype.name == "+1") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else if (selectedtype.name == "+2") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else if (selectedtype.name == "+3") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else if (selectedtype.name == "+4") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else if (selectedtype.name == "+5") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else if (selectedtype.name == "-1") {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                } else {
                    tile.Init (this, x, y, selectedtype, false, null, null);
                }

                tile.GetComponent<SpriteRenderer> ().color = selectedtype.color;
                tile.GetComponent<SpriteRenderer> ().sprite = selectedtype.sprite;

                board[x, y] = tile;
            }
        }
        UiManager.instance.GenerateTxt (2);
    }

    public void GenerateBoard () {
        TypeMenu.SetActive (false);
        DestroyAllTiles ();
        Tile[, ] randlevel = Levels.instance.GenerateLevels ();

        if (FirstTilePos == null)
            Debug.LogError ("[Board] Start position is empty!");

        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                var tile = Instantiate (
                    matchPieceObject,
                    new Vector3 (startX + ((xOffset) * x), startY + ((yOffset) * y), 2),
                    matchPieceObject.transform.rotation,
                    FirstTilePos).AddComponent<Tile> ();

                tile.Init (this, randlevel[x, y].PosX, randlevel[x, y].PosY, randlevel[x, y].type, randlevel[x, y].visited, randlevel[x, y].neighbors, randlevel[x, y].history);
                tile.GetComponent<SpriteRenderer> ().color = randlevel[x, y].type.color;
                tile.GetComponent<SpriteRenderer> ().sprite = randlevel[x, y].type.sprite;

                board[x, y] = tile;
            }
        }

        UiManager.instance.GenerateTxt (2);
    }

    private void ReplaceBoard (Tile[, ] newBoard) {
        DestroyAllTiles ();
        if (FirstTilePos == null)
            Debug.LogError ("[Board] Start position is empty!");

        float startX = FirstTilePos.transform.position.x;
        float startY = FirstTilePos.transform.position.y;

        for (int y = 0; y < ySize; y++) {
            for (int x = 0; x < xSize; x++) {
                var tile = Instantiate (
                    matchPieceObject,
                    new Vector3 (FirstTilePos.transform.position.x + (xOffset * x), FirstTilePos.transform.position.y + (yOffset * y), 2),
                    matchPieceObject.transform.rotation,
                    FirstTilePos) as GameObject;
                tile.AddComponent<Tile> ();

                if (newBoard[x, y].type.name == "Wall") {
                    selectedtype = tileTypes[1];
                } else if (newBoard[x, y].type.name == "+1") {
                    selectedtype = tileTypes[2];
                } else if (newBoard[x, y].type.name == "+2") {
                    selectedtype = tileTypes[5];
                } else if (newBoard[x, y].type.name == "+3") {
                    selectedtype = tileTypes[6];
                } else if (newBoard[x, y].type.name == "+4") {
                    selectedtype = tileTypes[7];
                } else if (newBoard[x, y].type.name == "+5") {
                    selectedtype = tileTypes[8];
                } else if (newBoard[x, y].type.name == "-1") {
                    selectedtype = tileTypes[9];
                } else if (newBoard[x, y].type.name == "Start") {
                    selectedtype = tileTypes[3];
                } else if (newBoard[x, y].type.name == "Goal") {
                    selectedtype = tileTypes[4];
                } else {
                    selectedtype = tileTypes[0];
                }

                tile.GetComponent<Tile> ().Init (this, x, y, selectedtype, newBoard[x, y].visited, newBoard[x, y].neighbors, newBoard[x, y].history);

                tile.GetComponent<SpriteRenderer> ().color = selectedtype.color;
                tile.GetComponent<SpriteRenderer> ().sprite = selectedtype.sprite;
            }
        }
    }

    public IEnumerator Show_Type (bool Y) {
        if (Y) {
            done = false;
            UiManager.instance.GenerateTxt (1);
            TypeMenu.SetActive (true);
            yield return waitForUser ();
        }
    }

    public void SelectType (int T) {
        if (T == 0) {
            selectedtype = tileTypes[0];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 1) {
            selectedtype = tileTypes[1];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 2) {
            selectedtype = tileTypes[2];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 5) {
            selectedtype = tileTypes[5];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 6) {
            selectedtype = tileTypes[6];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 7) {
            selectedtype = tileTypes[7];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 8) {
            selectedtype = tileTypes[8];
            TypeMenu.SetActive (false);
            done = true;
        }
        if (T == 9) {
            selectedtype = tileTypes[9];
            TypeMenu.SetActive (false);
            done = true;
        }

    }
    private void DestroyAllTiles () {
        Tile[] listOldTiles = FindObjectsOfType<Tile> ();
        for (int i = 0; i < listOldTiles.Length; i++) {
            Destroy (listOldTiles[i].gameObject);
        }
    }

    private IEnumerator waitForUser () {
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
        // now this function returns
    }

    public void StartPos (int Pos) {
        int X = Pos / 10;
        int Y = Pos % 10;

        if (!initpos) {
            if (board[X, Y].type.name == "Wall" || board[X, Y].type.PlusVal > 0) {
                UiManager.instance.GenerateTxt (5);
                return;
            }
            TileType t = tileTypes[3];
            var tile = Instantiate (
                matchPieceObject,
                new Vector3 (FirstTilePos.transform.position.x + (xOffset * X), FirstTilePos.transform.position.y + (yOffset * Y), 2),
                matchPieceObject.transform.rotation,
                FirstTilePos) as GameObject;
            tile.AddComponent<Tile> ();

            tile.GetComponent<Tile> ().Init (this, board[X, Y].PosX, board[X, Y].PosY, t, false, board[X, Y].neighbors, board[X, Y].history);

            tile.GetComponent<SpriteRenderer> ().color = tileTypes[3].color;
            tile.GetComponent<SpriteRenderer> ().sprite = tileTypes[3].sprite;

            board[X, Y] = tile.GetComponent<Tile> ();
            Ourtile = tile.GetComponent<Tile> ();
            initpos = true;
            UiManager.instance.GenerateTxt (3);
        } else if (!goalpos) {
            if (board[X, Y].type.name == "Wall" || board[X, Y].type.PlusVal > 0 || board[X, Y].type.PlusVal < 0 || Ourtile.PosX == X && Ourtile.PosY == Y) {
                UiManager.instance.GenerateTxt (5);
                return;
            }
            TileType t = tileTypes[4];
            var tile = Instantiate (
                matchPieceObject,
                new Vector3 (FirstTilePos.transform.position.x + (xOffset * X), FirstTilePos.transform.position.y + (yOffset * Y), 2),
                matchPieceObject.transform.rotation,
                FirstTilePos) as GameObject;
            tile.AddComponent<Tile> ();
            tile.GetComponent<Tile> ().Init (this, board[X, Y].PosX, board[X, Y].PosY, t, false, board[X, Y].neighbors, board[X, Y].history);

            tile.GetComponent<SpriteRenderer> ().color = tileTypes[4].color;
            tile.GetComponent<SpriteRenderer> ().sprite = tileTypes[4].sprite;

            board[X, Y] = tile.GetComponent<Tile> ();
            Goaltile = tile.GetComponent<Tile> ();
            goalpos = true;
            UiManager.instance.GenerateTxt (4);

            temp = Copy (board, temp);
            OurTempTile = new Tile (Ourtile);
            OurTempTile.Init (this, Ourtile.PosX, Ourtile.PosY, Ourtile.type, Ourtile.visited, Ourtile.neighbors, Ourtile.history);

            for (int x = 0; x < xSize; x++)
                for (int y = 0; y < ySize; y++) {
                    board[x, y].neighbors = GetAllNextStates (board, board[x, y]);
                }
        }
    }

    public bool ValidMove (Tile[, ] state, int X, int Y) {
        try {
            if (state[X, Y].type.name != "Wall" && (X <= xSize || X > 0) && (Y <= ySize || Y > 0)) {
                UiManager.instance.GenerateTxt (4);
                return true;
            }
            if (state[X, Y].type.name == "wall") {

                Debug.LogError ("Wall !");
                UiManager.instance.GenerateTxt (5);
                return false;
            }
            if (state[X, Y].visited) {

                Debug.LogError ("Visited !");
                UiManager.instance.GenerateTxt (6);
                return false;
            }
            if (moves == 0) {
                Debug.LogError ("insufficient Moves !");
                return false;
            }
            if ((X <= xSize || X > 0) || (Y <= ySize || Y > 0)) {
                Debug.LogError ("InValid Move !");
                return false;
            }
        } catch (Exception e) {
            Debug.Log (e);
        }

        return false;
    }
    public Tile[, ] Copy (Tile[, ] Copy, Tile[, ] Paste) {
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                Paste[x, y] = new Tile (Copy[x, y]);
            }
        }
        return Paste;
    }
    public void CopyPath (List<Tile> Copy, List<Tile> Paste) {
        foreach (Tile t in Copy) {
            Tile t2 = new Tile (t);
            t2.Init (this, t.PosX, t.PosY, t.type, t.visited, t.neighbors, t.history);
            Paste.Add (t2);
        }
    }
    public bool CheckGoal (Tile[, ] state, Tile active) {

        if (state[active.PosX, active.PosY].type.name == "Goal" && moves == 0) {
            UiManager.instance.ShowGameOverPanel (false);
            UiManager.instance.ShowGoalPanel (true);
            //Time.timeScale = 0;
            return true;
        } else if (state[active.PosX, active.PosY].type.name != "Goal" && moves == 0 || state[active.PosX, active.PosY].type.name == "Goal" && moves != 0) {
            UiManager.instance.ShowGameOverPanel (true);
            UiManager.instance.ShowGoalPanel (false);
            //Time.timeScale = 0;
            return false;
        }
        return false;
    }
    public void MoveDirection (string dir) {
        if (dir == "Left") {
            temp = GetNextState (temp, OurTempTile, "Left");
            Debug.LogError (Ourtile.PosX + "(ourtile) , " + Ourtile.PosY);
            ReplaceBoard (temp);
        } else if (dir == "Right") {
            temp = GetNextState (temp, OurTempTile, "Right");
            Debug.LogError (Ourtile.PosX + "(ourtile) , " + Ourtile.PosY);
            ReplaceBoard (temp);
        } else if (dir == "Up") {
            temp = GetNextState (temp, OurTempTile, "Up");
            Debug.LogError (Ourtile.PosX + "(ourtile) , " + Ourtile.PosY);
            ReplaceBoard (temp);
        } else if (dir == "Down") {
            temp = GetNextState (temp, OurTempTile, "Down");
            Debug.LogError (Ourtile.PosX + "(ourtile) , " + Ourtile.PosY);
            ReplaceBoard (temp);
        } else if (dir == "DFS" || dir == "BFS" || dir == "UCS") {
            StartCoroutine (ApplyAlgo (dir));
            return;
        }
        UiManager.instance.UpdateMoves (moves);
        CheckGoal (temp, OurTempTile);

    }
    private Tile[, ] GetNextState (Tile[, ] state, Tile active, string dir) {
        Tile[, ] nextstate = new Tile[xSize, ySize];
        Copy (state, nextstate);
        int newX = -1;
        int newY = -1;
        if (dir == "Left") {
            newX = active.PosX - 1;
            newY = active.PosY;
        } else if (dir == "Right") {
            newX = active.PosX + 1;
            newY = active.PosY;
        } else if (dir == "Up") {
            newX = active.PosX;
            newY = active.PosY + 1;
        } else if (dir == "Down") {
            newX = active.PosX;
            newY = active.PosY - 1;
        }
        if (newX != -1 && newY != -1) {

            MoveTile (nextstate, active, newX, newY);
            return nextstate;
        } else
            return null;
    }

    private void MoveTile (Tile[, ] state, Tile active, int newX, int newY) {
        Debug.LogError (active.PosX + " , " + active.PosY);
        Debug.LogError (newX + " , " + newY);
        if (ValidMove (state, newX, newY)) {
            //edit moves
            moves += state[newX, newY].type.PlusVal - 1;

            //return old tile after moving
            state[active.PosX, active.PosY].Init (this, active.PosX, active.PosY, board[active.PosX, active.PosY].type, board[active.PosX, active.PosY].visited, active.neighbors, active.history);
            state[Ourtile.PosX, Ourtile.PosY].Init (this, Ourtile.PosX, Ourtile.PosY, tileTypes[0], board[active.PosX, active.PosY].visited, Ourtile.neighbors, Ourtile.history);

            Tile tile = new Tile ();
            tile.Init (this, newX, newY, tileTypes[3], true, state[newX, newY].neighbors, state[newX, newY].history);

            //if reached goal
            if (state[newX, newY].type.name == "Goal") {
                active.Init (this, tile.PosX, tile.PosY, tileTypes[3], true, tile.neighbors, tile.history);
            } else {
                state[newX, newY] = tile;
                active.Init (this, tile.PosX, tile.PosY, tileTypes[3], true, tile.neighbors, tile.history);
            }

        }
    }

    public List<Tile> GetAllNextStates (Tile[, ] state, Tile active) {
        List<Tile> AllNextStates = new List<Tile> ();
        try {
            //checkdown
            if (active.PosY - 1 >= 0) {
                if (state[active.PosX, active.PosY - 1].type.name != "Wall") {
                    AllNextStates.Add (state[active.PosX, active.PosY - 1]);
                }
            }
            //checkleft
            if (active.PosX - 1 >= 0) {
                if (state[active.PosX - 1, active.PosY].type.name != "Wall") {
                    AllNextStates.Add (state[active.PosX - 1, active.PosY]);
                }
            }
            //checkup
            if (active.PosY + 1 < ySize) {
                if (state[active.PosX, active.PosY + 1].type.name != "Wall") {
                    AllNextStates.Add (state[active.PosX, active.PosY + 1]);
                }
            }
            //checkright
            if (active.PosX + 1 < xSize) {
                if (state[active.PosX + 1, active.PosY].type.name != "Wall") {
                    AllNextStates.Add (state[active.PosX + 1, active.PosY]);
                }
            }
        } catch (Exception e) {
            Debug.Log (e);
        }

        return AllNextStates;
    }
    public IEnumerator ApplyAlgo (string dir) {

        if (dir == "DFS") {
            List<Tile> pathList = new List<Tile> ();

            // add source to path[] 
            pathList.Add (board[Ourtile.PosX, Ourtile.PosY]);
            isvisited.Add (board[Ourtile.PosX, Ourtile.PosY]);

            // Call recursive utility 
            DFS (board[Ourtile.PosX, Ourtile.PosY], pathList, moves + 1);

        }

        if (dir == "BFS") {
            List<Tile> pathList = new List<Tile> ();
            BFS (board[Ourtile.PosX, Ourtile.PosY], pathList);
        }
        if (dir == "UCS") {
            List<Tile> pathList = new List<Tile> ();
            UCS_BFS (board[Ourtile.PosX, Ourtile.PosY], pathList);
        }

        if (Path.Count == 0) {
            Debug.LogError ("Did not Find a Path !  " + Path.Count + moves);
            UiManager.instance.ShowGameOverPanel (true);
            UiManager.instance.ShowGoalPanel (false);
        } else {
            Debug.LogError ("Number Of Tiles in Path are  : " + Path.Count);

            Tile[, ] tempp = new Tile[xSize, ySize];
            Copy (board, tempp);
            Tile t = new Tile (Ourtile);
            t.Init (this, Ourtile.PosX, Ourtile.PosY, Ourtile.type, Ourtile.visited, Ourtile.neighbors, Ourtile.history);
            Path.RemoveAt (0);
            foreach (Tile j in Path) {
                MoveTile (tempp, t, j.PosX, j.PosY);
                UiManager.instance.UpdateMoves (moves);
                ReplaceBoard (tempp);
                CheckGoal (tempp, t);
                yield return new WaitForSeconds (1f);

            }

        }
    }

    private void DFS (Tile t, List<Tile> LocalPath, int tmoves) {

        if (pathfound) {
            return;
        }

        if (t.type.name == "Goal") {
            if (LocalPath.Count == tmoves && !pathfound) {
                Debug.LogError ("Found Paaaaaaath : " + LocalPath.Count);
                CopyPath (LocalPath, Path);
                pathfound = true;
            }
            return;
        }

        if (t.neighbors.Count != 0) {
            for (int i = 0; i < t.neighbors.Count; i++) {
                if (!isvisited.Contains (t.neighbors[i])) {
                    isvisited.Add (t.neighbors[i]);
                    LocalPath.Add (t.neighbors[i]);
                    DFS (t.neighbors[i], LocalPath, tmoves + t.neighbors[i].type.PlusVal);
                    LocalPath.Remove (t.neighbors[i]);
                    isvisited.Remove (t.neighbors[i]);
                }
            }
        }

    }

    public void BFS (Tile t, List<Tile> LocalPath) {
        LinkedList<List<Tile>> queue = new LinkedList<List<Tile>> ();

        LocalPath.Add (t);
        queue.AddLast (LocalPath);

        int moves = 8;
        bool neg = false;

        while (queue.Count != 0) {

            LocalPath = queue.First.Value;

            queue.RemoveFirst ();
            Tile j = LocalPath[LocalPath.Count - 1];

            if (j.type.name == "Goal") {
                /* Debbug start */
                Debug.LogError ("Found Paaaaaaath : " + LocalPath.Count);
                /*Debug End  */

                foreach (Tile f in LocalPath) {
                    moves += f.type.PlusVal;
                    if (moves < 0)
                        neg = true;
                }

                if (LocalPath.Count == moves && !neg) {
                    Debug.LogError ("LocalPath.Count == moves, count(moves) : " + LocalPath.Count);
                    CopyPath (LocalPath, Path);
                    return;
                } else {
                    neg = false;
                    moves = 8;
                }
            }

            if (j.neighbors.Count != 0) {
                for (int i = 0; i < j.neighbors.Count; i++) {
                    if (!LocalPath.Contains (j.neighbors[i])) {
                        List<Tile> newPath = new List<Tile> (LocalPath);
                        newPath.Add (j.neighbors[i]);
                        queue.AddLast (newPath);
                    }
                }
            }
        }
    }

    public void UCS_BFS (Tile t, List<Tile> LocalPath) {
        LinkedList<List<Tile>> queue = new LinkedList<List<Tile>> ();

        LocalPath.Add (t);
        queue.AddLast (LocalPath);

        int moves = 8;
        bool neg = false;

        while (queue.Count != 0) {

            LocalPath = queue.First.Value;

            queue.RemoveFirst ();
            Tile j = LocalPath[LocalPath.Count - 1];

            if (j.type.name == "Goal") {
                /* Debbug start */
                Debug.LogError ("Found Paaaaaaath : " + LocalPath.Count);
                /*Debug End  */

                foreach (Tile f in LocalPath) {
                    moves += f.type.PlusVal;
                    if (moves < 0)
                        neg = true;
                }

                if (LocalPath.Count == moves && !neg) {
                    Debug.LogError ("LocalPath.Count == moves, count(moves) : " + LocalPath.Count);
                    CopyPath (LocalPath, Path);
                    return;
                } else {
                    neg = false;
                    moves = 8;
                }
            }

            if (j.neighbors.Count != 0) {
                j.neighbors.Sort ((x, y) => x.type.PlusVal.CompareTo (y.type.PlusVal));
                for (int i = 0; i < j.neighbors.Count; i++) {
                    if (!LocalPath.Contains (j.neighbors[i])) {
                        List<Tile> newPath = new List<Tile> (LocalPath);
                        newPath.Add (j.neighbors[i]);
                        queue.AddLast (newPath);
                    }
                }
            }
        }
    }
    // public List<List<Tile>> DFS_Stack (Tile active) {

    //     //List<List<Tile>> Paths = new List<List<Tile>> ();
    //     List<Tile> Path = new List<Tile> ();
    //     bool[, ] visited = new bool[xSize, ySize];
    //     Stack<Tile> stack = new Stack<Tile> ();
    //     stack.Push (temp[active.PosX, active.PosY]);

    //     while (stack.Count != 0) {
    //         Tile t = stack.Pop ();

    //         if (visited[t.PosX, t.PosY]) {
    //             Debug.LogError ("Skipped : X= " + t.PosX + "Y= " + t.PosY + t.visited);
    //             if (Tile.ReferenceEquals (t, null)) {
    //                 Debug.LogError ("t = null  + " + t.type.name);
    //             }
    //             continue;
    //         }
    //         visited[t.PosX, t.PosY] = true;
    //         Path.Add (t);
    //         if (t.type.name == "Goal") {
    //             Debug.LogError (" (Found Path!");
    //             //visited[t.PosX, t.PosY] = false;
    //             Paths.Add (Path);
    //             //Path.Clear ();
    //         }

    //         List<Tile> tempo = GetAllNextStates (temp, t);
    //         if (tempo.Count != 0) {
    //             for (int i = 0; i < tempo.Count; i++) {
    //                 if (visited[tempo[i].PosX, tempo[i].PosY])
    //                     continue;
    //                 else {
    //                     //visited[tempo[i].PosX, tempo[i].PosY] = true;
    //                     Path.Add (tempo[i]);
    //                     stack.Push (tempo[i]);
    //                     visited[tempo[i].PosX, tempo[i].PosY] = false;
    //                     Path.RemoveAt (Path.Count - 1);
    //                 }

    //             }
    //         }
    //     }
    //     return Paths;
    // }

}