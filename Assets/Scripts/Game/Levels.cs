using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {
    public List<Tile[, ]> levels = new List<Tile[, ]> ();
    public static Levels instance;
    public static System.Random rnd = new System.Random ();

    #region     
    public Levels () {
        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    public Tile[, ] GenerateLevels () {

        List<TileType> PlusTypes = new List<TileType> ();
        PlusTypes.Add (Board.instance.tileTypes[2]);
        PlusTypes.Add (Board.instance.tileTypes[5]);
        PlusTypes.Add (Board.instance.tileTypes[6]);
        PlusTypes.Add (Board.instance.tileTypes[7]);
        PlusTypes.Add (Board.instance.tileTypes[8]);
        PlusTypes.Add (Board.instance.tileTypes[9]);

        levels.Add (level0 ());
        levels.Add (level1 ());
        levels.Add (level2 ());
        levels.Add (level3 ());
        levels.Add (level4 ());
        levels.Add (level5 ());
        levels.Add (level6 ());
        levels.Add (level7 ());
        levels.Add (veryrandom ());
        levels.Add (veryrandom ());
        levels.Add (veryrandom ());

        int r = rnd.Next (levels.Count);
        if (r == 8) {
            return levels[r];
        } else {
            List<Tile> NoneList = new List<Tile> ();
            foreach (Tile t in levels[r]) {
                if (t.type.name == "None")
                    NoneList.Add (t);
            }

            for (int rndtime = 0; rndtime < 3; rndtime++) {
                int r2 = rnd.Next (PlusTypes.Count);
                int r3 = rnd.Next (NoneList.Count);
                Tile plustile = new Tile ();
                plustile.Init (Board.instance, NoneList[r3].PosX, NoneList[r3].PosY, PlusTypes[r2], NoneList[r3].visited, NoneList[r3].neighbors, NoneList[r3].history);
                levels[r][plustile.PosX, plustile.PosY] = plustile;
            }
            return levels[r];

        }
    }
    public Tile[, ] level0 () {

        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];

        TileType selectedtype;
        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 2, 0, 8);
        FillFrom (level, selectedtype, 2, 3, 0, 5);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 2, 3, 4, 8);
        FillFrom (level, selectedtype, 3, 6, 0, 8);

        return level;
    }
    public Tile[, ] level1 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 1, 6, 0, 1);
        FillFrom (level, selectedtype, 0, 6, 1, 2);
        FillFrom (level, selectedtype, 2, 6, 2, 3);
        FillFrom (level, selectedtype, 0, 6, 3, 4);
        FillFrom (level, selectedtype, 4, 6, 4, 5);
        FillFrom (level, selectedtype, 0, 6, 5, 6);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 0, 1, 0, 1);
        FillFrom (level, selectedtype, 0, 2, 2, 3);
        FillFrom (level, selectedtype, 0, 4, 4, 5);
        FillFrom (level, selectedtype, 0, 6, 6, 8);

        return level;
    }
    public Tile[, ] level2 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 1, 0, 8);
        FillFrom (level, selectedtype, 3, 4, 0, 8);
        FillFrom (level, selectedtype, 0, 6, 6, 8);
        FillFrom (level, selectedtype, 4, 6, 0, 1);
        FillFrom (level, selectedtype, 4, 6, 2, 3);
        FillFrom (level, selectedtype, 4, 6, 4, 5);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 1, 3, 0, 6);
        FillFrom (level, selectedtype, 4, 6, 1, 2);
        FillFrom (level, selectedtype, 4, 6, 3, 4);
        FillFrom (level, selectedtype, 4, 6, 5, 6);

        return level;
    }
    public Tile[, ] level3 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 1, 0, 8);
        FillFrom (level, selectedtype, 2, 3, 0, 8);
        FillFrom (level, selectedtype, 4, 6, 0, 8);
        FillFrom (level, selectedtype, 0, 6, 5, 8);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 1, 3, 0, 5);
        FillFrom (level, selectedtype, 3, 4, 0, 5);

        return level;
    }
    public Tile[, ] level4 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];

        FillFrom (level, selectedtype, 0, 6, 0, 8);

        FillFrom (level, selectedtype, 0, 6, 2, 4);
        FillFrom (level, selectedtype, 0, 6, 6, 8);
        FillFrom (level, selectedtype, 1, 2, 1, 8);
        FillFrom (level, selectedtype, 3, 4, 1, 8);
        FillFrom (level, selectedtype, 1, 2, 1, 2);
        FillFrom (level, selectedtype, 3, 4, 1, 2);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 0, 6, 0, 1);
        FillFrom (level, selectedtype, 0, 1, 1, 2);
        FillFrom (level, selectedtype, 2, 3, 1, 2);
        FillFrom (level, selectedtype, 4, 6, 1, 2);
        FillFrom (level, selectedtype, 0, 1, 4, 8);
        FillFrom (level, selectedtype, 2, 3, 4, 8);
        FillFrom (level, selectedtype, 4, 6, 4, 8);

        return level;
    }
    public Tile[, ] level5 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 6, 0, 1);
        FillFrom (level, selectedtype, 0, 6, 2, 3);
        FillFrom (level, selectedtype, 0, 6, 4, 5);
        FillFrom (level, selectedtype, 0, 6, 6, 8);
        FillFrom (level, selectedtype, 2, 3, 0, 8);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 0, 2, 1, 2);
        FillFrom (level, selectedtype, 0, 2, 3, 4);
        FillFrom (level, selectedtype, 0, 2, 5, 6);
        FillFrom (level, selectedtype, 3, 6, 1, 2);
        FillFrom (level, selectedtype, 3, 6, 3, 4);
        FillFrom (level, selectedtype, 3, 6, 5, 6);

        return level;
    }
    public Tile[, ] level6 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 6, 1, 2);
        FillFrom (level, selectedtype, 0, 6, 4, 5);
        FillFrom (level, selectedtype, 0, 6, 6, 8);
        FillFrom (level, selectedtype, 1, 2, 0, 5);
        FillFrom (level, selectedtype, 3, 4, 0, 5);
        FillFrom (level, selectedtype, 0, 1, 4, 8);
        FillFrom (level, selectedtype, 2, 3, 4, 8);
        FillFrom (level, selectedtype, 4, 6, 4, 8);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 0, 1, 0, 1);
        FillFrom (level, selectedtype, 2, 3, 0, 1);
        FillFrom (level, selectedtype, 4, 6, 0, 1);
        FillFrom (level, selectedtype, 0, 1, 2, 4);
        FillFrom (level, selectedtype, 2, 3, 2, 4);
        FillFrom (level, selectedtype, 4, 6, 2, 4);

        FillFrom (level, selectedtype, 1, 2, 5, 6);
        FillFrom (level, selectedtype, 3, 4, 5, 6);

        return level;
    }
    public Tile[, ] level7 () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        TileType selectedtype;

        selectedtype = Board.instance.tileTypes[0];
        FillFrom (level, selectedtype, 0, 4, 0, 1);
        FillFrom (level, selectedtype, 1, 4, 1, 2);
        FillFrom (level, selectedtype, 2, 4, 2, 3);
        FillFrom (level, selectedtype, 0, 1, 2, 3);
        FillFrom (level, selectedtype, 0, 4, 3, 4);
        FillFrom (level, selectedtype, 0, 3, 4, 5);
        FillFrom (level, selectedtype, 0, 4, 5, 6);

        selectedtype = Board.instance.tileTypes[1];
        FillFrom (level, selectedtype, 0, 1, 1, 2);
        FillFrom (level, selectedtype, 1, 2, 2, 3);
        FillFrom (level, selectedtype, 3, 4, 4, 5);
        FillFrom (level, selectedtype, 0, 6, 6, 8);
        FillFrom (level, selectedtype, 4, 6, 0, 8);

        return level;
    }
    public Tile[, ] veryrandom () {
        Tile[, ] level = new Tile[Board.instance.xSize, Board.instance.ySize];
        List<TileType> tilesTypes = new List<TileType> ();
        tilesTypes.Add (Board.instance.tileTypes[0]);
        tilesTypes.Add (Board.instance.tileTypes[0]);
        tilesTypes.Add (Board.instance.tileTypes[0]);
        tilesTypes.Add (Board.instance.tileTypes[0]);
        tilesTypes.Add (Board.instance.tileTypes[1]);
        tilesTypes.Add (Board.instance.tileTypes[1]);
        tilesTypes.Add (Board.instance.tileTypes[2]);
        tilesTypes.Add (Board.instance.tileTypes[2]);
        tilesTypes.Add (Board.instance.tileTypes[5]);
        tilesTypes.Add (Board.instance.tileTypes[5]);
        tilesTypes.Add (Board.instance.tileTypes[6]);
        tilesTypes.Add (Board.instance.tileTypes[6]);
        tilesTypes.Add (Board.instance.tileTypes[7]);
        tilesTypes.Add (Board.instance.tileTypes[8]);
        tilesTypes.Add (Board.instance.tileTypes[9]);
        tilesTypes.Add (Board.instance.tileTypes[9]);
        tilesTypes.Add (Board.instance.tileTypes[9]);
        tilesTypes.Add (Board.instance.tileTypes[9]);
        tilesTypes.Add (Board.instance.tileTypes[9]);
        TileType selectedtype;

        for (int x = 0; x < Board.instance.xSize; x++)
            for (int y = 0; y < Board.instance.ySize; y++) {
                int r = rnd.Next(tilesTypes.Count);
                selectedtype = tilesTypes[r];
                Tile tile = new Tile ();
                tile.Init (Board.instance, x, y, selectedtype, false, null, null);
                level[x, y] = tile;
            }
        return level;
    }

    public void FillFrom (Tile[, ] l, TileType selectedtype, int A, int B, int Y, int Z) {
        for (int x = A; x < B; x++)
            for (int y = Y; y < Z; y++) {
                Tile tile = new Tile ();
                tile.Init (Board.instance, x, y, selectedtype, false, null, null);
                l[x, y] = tile;
            }
    }
}