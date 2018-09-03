using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {

    public int width;
    public int height;

    public bool initialized;

    // Dimensions of the grid image and canvas.
    public RawImage gridDisplay;
    public float imageWidth;
    public float imageHeight;

    public float originalImageWidth;
    public float originalImageHeight;

    public Canvas canvas;
    public float canvasWidth;
    public float canvasHeight;

    public AudioClip SetSound;
    public AudioClip UnsetSound;

    // Current and previous pos of selected cell.
    private int curSelectedX;
    private int curSelectedY;
    private int preSelectedX;
    private int preSelectedY;

    private GridSystem grid;

    private AudioSource audioSource;

    private bool isGameMode;

    private void Awake()
    {
        originalImageWidth = gridDisplay.GetComponent<RectTransform>().rect.width;
        originalImageHeight = gridDisplay.GetComponent<RectTransform>().rect.height;
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }


    // Use this for initialization.
    void Start () {
        initialized = false;
	}

    // Activate/deactivate the grid.
    public void SetGridState(bool active)
    {
        gridDisplay.gameObject.SetActive(active);
    }


    // Initialization.
    public void Initialize(int w, int h, bool gameMode)
    {
        isGameMode = gameMode;
        initialized = false;
        grid = null;
        width = w;
        height = h;
        AdjustScreenSize();
        grid = new GridSystem(width, height, gridDisplay);
        grid.InitGrid(gameMode);
        grid.DrawGrid();
        audioSource = GetComponent<AudioSource>();
        initialized = true;
    }


    // Adjust the screen size based on the
    // given width and height so that the 
    // cells do not appear distorted.
    private void AdjustScreenSize()
    {
        imageWidth = originalImageWidth;
        imageHeight = originalImageHeight;

        Vector2 newDisplaySize;
        if (width > height)
        {
            newDisplaySize = new Vector2(imageWidth, imageHeight * height / width);
        }
        else
        {
            newDisplaySize = new Vector2(imageWidth * width / height, imageHeight);
        }
        gridDisplay.rectTransform.sizeDelta = newDisplaySize;
        imageWidth = gridDisplay.GetComponent<RectTransform>().rect.width;
        imageHeight = gridDisplay.GetComponent<RectTransform>().rect.height;

    }


    // Update is called once per frame
    void Update () {
        if (initialized)
        {
            if (Input.GetKey("right"))
            {
                Invoke("GenNext", 0.1f);
            }
            else if (Input.GetKey("left"))
            {
                Invoke("GenPrev", 0.1f);
            }
            if (!isGameMode)
            {
                HighlightAndSetSelected();
            }
        }
        
    }


    // Generate next stage.
    public void GenNext()
    {
        if (initialized)
        {
            grid.Record(); // Record for undo.
            grid.GenNext();
            grid.DrawGrid();
        }
    }


    // Reverse to previous stage if possible.
    public void GenPrev()
    {   if (initialized)
        {
            grid.GenPrev();
            grid.DrawGrid();
        }
    }

    // Clear All cells.
    public void ClearAll()
    {
        if (initialized)
        {
            grid.Record(); // Record for undo.
            grid.ClearAll();
            grid.DrawGrid();
        }
    }


    // Highlight the cell where the mouse if hovering over
    // and set cell's state based on which mouse button is clicked.
    public void HighlightAndSetSelected()
    {
        Vector3 mousePos = Input.mousePosition;
        float newX = mousePos.x - (canvasWidth - imageWidth) / 2;
        float newY = mousePos.y - (canvasHeight - imageHeight) / 2;
        curSelectedX = (int)(newX / (imageWidth / width));
        curSelectedY = (int)(newY / (imageHeight / height));

        // Reset the border color of the previously selected cell.
        if (curSelectedX != preSelectedX || curSelectedY != preSelectedY)
        {
            if (preSelectedX >= 0 && preSelectedY >= 0 && preSelectedX < width && preSelectedY < height)
            {
                // Unhighlight the previously selected cell.
                grid.HighlightSelected(preSelectedX, preSelectedY, false);
            }
        }

        if (curSelectedX >= 0 && curSelectedY >= 0 && curSelectedX < width && curSelectedY < height)
        {
            preSelectedX = curSelectedX;
            preSelectedY = curSelectedY;

            grid.HighlightSelected(curSelectedX, curSelectedY, true);

            if (Input.GetMouseButton(0))
            {
                grid.Record(); // Record for undo.

                if (grid.SetSelected(curSelectedX, curSelectedY, true))
                {
                    // Only play sound if set.
                    audioSource.clip = SetSound;
                    audioSource.Play();
                }


            }
            else if (Input.GetMouseButton(1))
            {
                grid.Record(); // Record for undo.

                if (grid.SetSelected(curSelectedX, curSelectedY, false))
                {
                    // Only play sound if set.
                    audioSource.clip = UnsetSound;
                    audioSource.Play();
                }
            }
            grid.DrawGrid();
        }
    }
}
