using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridSystem {

    public int width;
    public int height;
    public int cellSize = 10;

    protected RawImage display;

    protected Texture2D gridTexture;

    protected Cell[,] cells;

    protected bool isGameMode;

    // A list of changed cells prepared for update.
    // We use this to avoid looping through all the cells.
    protected List<Cell> changedCells;

    // A stack of 2D array of bools for undo purpose;
    protected Stack<UnitState[,]> recordings;


    // Constructor
    public GridSystem(int w, int h, RawImage dis)
    {
        width = w;
        height = h;
        display = dis;
        cells = new Cell[width, height];
        changedCells = new List<Cell>();
    }


    // Initialize the grid.
    public virtual void InitGrid(bool gameMode)
    {
        isGameMode = gameMode;
        recordings = new Stack<UnitState[,]>();
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (gameMode)
                {
                    //cells[i, j] = new Cell(i, j, new Unit(Identifier.w, Random.Range(1, 8), false));
                    cells[i, j] = new Cell(i, j, new Unit((Identifier)Random.Range(0, 4), Random.Range(0, 2) == 1 ? true : false));
                }
                else
                {
                    cells[i, j] = new Cell(i, j, new Unit(Identifier.w, Random.Range(0, 3) == 1 ? true : false));
                }
                changedCells.Add(cells[i, j]);
            }
        }
        GetCellNeighbors();
    }


    // Get the neighbors for each cell.
    protected void GetCellNeighbors()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                for (int p = i - 1; p <= i + 1; ++p)
                {
                    for (int q = j - 1; q <= j + 1; ++q)
                    {
                        if (p >= 0 && p < width && // Boundary check.
                            q >= 0 && q < height && // Boundary check.
                            !(p == i && q == j)) // Do not include self.
                        {
                            cells[i, j].neighbors.Add(cells[p, q]);
                        }
                    }
                }
            }
        }
    }


    // Draw/Update the grid by changing the color of pixels for
    // the texture of the display image.
    public void DrawGrid()
    {
        bool drawBorder = false;
        if (gridTexture == null)
        {
            drawBorder = true;
            gridTexture = new Texture2D(width * cellSize, height * cellSize, TextureFormat.RGB24, false);
            gridTexture.filterMode = FilterMode.Point;
            display.texture = gridTexture;
        }

        foreach (Cell cell in changedCells)
        {
            for (int p = cell.xIndex * cellSize; p < (cell.xIndex + 1) * cellSize; ++p)
            {
                for (int q = cell.yIndex * cellSize; q < (cell.yIndex + 1)* cellSize; ++q)
                {
                    if (p == cell.xIndex * cellSize || q == cell.yIndex * cellSize || p == (cell.xIndex + 1) * cellSize - 1 || q == (cell.yIndex + 1) * cellSize - 1) // Condition for borders.
                    {
                        if (drawBorder) gridTexture.SetPixel(p, q, cell.borderColor); // Only draw the border the first time.
                    }
                    else
                    {
                        gridTexture.SetPixel(p, q, cell.stateColor);
                    }
                }
            }
        }
        gridTexture.Apply();
    }


    // Generate the next state;
    public void GenNext()
    {
        changedCells.Clear();
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (cells[i, j].PrepareUpdate(isGameMode))
                {
                    changedCells.Add(cells[i, j]);
                }
            }
        }

        foreach(Cell cell in changedCells)
        {
            cell.Update();
        }
    }


    // Generate the previous state;
    public void GenPrev()
    {
        changedCells.Clear();
        if (recordings.Count > 0)
        {
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    bool recordingTopAlive = recordings.Peek()[i, j].IsAlive;
                    Identifier recordingTopId = recordings.Peek()[i, j].Id;
                    if (cells[i, j].SetUnitState(true, recordingTopAlive, recordingTopId))
                    {
                        changedCells.Add(cells[i, j]);
                    }
                }
            }
            recordings.Pop();
        }
    }

    // Highlight/unhighlight selected cell.
    public void HighlightSelected(int i, int j, bool highlight)
    {
        for (int p = i * cellSize; p < (i + 1) * cellSize; ++p)
        {
            for (int q = j * cellSize; q < (j + 1) * cellSize; ++q)
            {
                if (p == i * cellSize || q == j * cellSize || p == (i + 1) * cellSize - 1 || q == (j + 1) * cellSize - 1)
                {
                    gridTexture.SetPixel(p, q, highlight ? cells[i, j].highlightColor : cells[i, j].borderColor);
                }
            }
        }
        gridTexture.Apply();
    }


    // Set the state of the selected cell.
    // if there is no need to set, return false.
    public bool SetSelected(int i, int j, bool alive)
    {
        changedCells.Clear();
        if (cells[i, j].SetUnitState(true, alive))
        {
            changedCells.Add(cells[i, j]);
            return true;
        }
        return false;
    }


    // Record the current state of all cells for undo.
    public void Record()
    {
        if (recordings.Count == int.MaxValue)
        {
            Debug.Log("I don't think you can reach here");
            return;
        }

        UnitState[,] recording = new UnitState[width, height];
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                recording[i, j] = new UnitState(cells[i, j].GetUnitState(true));
            }
        }
        recordings.Push(recording);
    }


    // Clear all live cells.
    public void ClearAll()
    {
        changedCells.Clear();
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (cells[i, j].SetUnitState(true, false))
                {
                    changedCells.Add(cells[i, j]);
                }
            }
        }
    }
};
