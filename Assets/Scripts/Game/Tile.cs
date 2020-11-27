using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField]
    public TileType type;
    public TileType GetTileType => type;

    public int PosX { get; set; }
    public int PosY { get; set; }

    public bool visited { get; set; }
    public List<Tile> neighbors = new List<Tile> ();
    public List<Tile> history = new List<Tile> ();

    private SpriteRenderer ballColorRender;

    //private Board board;

    private void Awake () {
        ballColorRender = GetComponent<SpriteRenderer> ();
    }

    public Tile () {

    }

    public Tile (Tile OldObj) {
        this.type = OldObj.type;
        this.PosX = OldObj.PosX;
        this.PosY = OldObj.PosY;
        //this.board = OldObj.board;
        this.visited = OldObj.visited;
        try {
            if (OldObj.neighbors.Count > 0)
                for (int i = 0; i < OldObj.neighbors.Count; i++) {
                    this.neighbors.Add (OldObj.neighbors[i]);
                }
            if (OldObj.history.Count > 0)
                for (int i = 0; i < OldObj.history.Count; i++) {
                    this.history.Add (OldObj.history[i]);
                }

        } catch (Exception e) {
            //Debug.Log (e);
        }
        // try {
        //     this.ballColorRender = OldObj.ballColorRender;
        //     //this.ballColorRender.color = OldObj.ballColorRender.color;
        // } catch (Exception e) {
        //     Debug.Log (e);
        // }
    }

    public void Init (Board board, int posX, int posY, TileType type, bool visited, List<Tile> neighbors, List<Tile> history) {
        //this.board = board;

        this.PosX = posX;
        this.PosY = posY;
        this.type = type;
        this.visited = visited;
        this.neighbors = neighbors;
        this.history = history;

        // try {
        //     ballColorRender.sprite = type.sprite;
        //     ballColorRender.color = type.color;
        // } catch (Exception e) {
        //     Debug.Log (e);
        // }

    }

}