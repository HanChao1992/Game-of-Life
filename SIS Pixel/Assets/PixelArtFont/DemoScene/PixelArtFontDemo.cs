using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PixelArtFontDemo : MonoBehaviour 
{
	// references to UI Text elements
	public Text pixelArtFontTest;
	public Text pixelArtThreeDTxt;
	public Text pixelArtOutlinedTxt;
	public Text titleText;
	public Text titleThreeDtext;
	public Text titleOutlinedText;
	public Text pixelArtExtTxt;
	public Text pixelArtExtTitleTxt;
    public Text blockyTxt;
    public Text blockyTitleTxt;
	public GameObject pixelArtFont;
	public GameObject pixelArtThreeD;
	public GameObject pixelArtOutlined;
	public GameObject pixelArtTitle;
	public GameObject pixelArtThreeDtitle;
	public GameObject pixelArtOutlinedTitle;
	public GameObject pixelArtExt;
	public GameObject pixelArtExtTitle;
    public GameObject blocky;
    public GameObject blockyTitle;

    public GameObject fontCanvas;
    public GameObject mainCamera;
    public GameObject worldCamera;


	void Start ()
    {
        mainCamera.SetActive(true);
        fontCanvas.SetActive(true);
        worldCamera.SetActive(false);

        // Populate the text UI elements below...
        pixelArtThreeD.SetActive(false);
        pixelArtFont.SetActive(false);
        pixelArtOutlined.SetActive(false);
        pixelArtExt.SetActive(false);
        pixelArtThreeDtitle.SetActive(false);
        pixelArtOutlinedTitle.SetActive(false);
        pixelArtExtTitle.SetActive(false);
        pixelArtTitle.SetActive(false);
        blocky.SetActive(false);
        blockyTitle.SetActive(false);
        pixelArtFontTest.text = "148 Usable Characters \n" + "abcdefghijklmnopqrstuvwxyz \n" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
          "\n 0123456789" + "!£$%^&*()_-=+{}[];:'@#~,<.>/?`¬" + "\n ☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼ \n" 
				+ "¢¥ƒ¿½¼¡«»©®°±µ¶¾÷‹›™†‡–Ø€" + "\nThanks for using fonts from JazzCreates2015©.";
		pixelArtThreeDTxt.text = "110 Usable Characters\n" + "abcdefghijklmnopqrstuvwxyz \n" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
						"\n 0123456789" + "!£$%^&*()_-=+{}[];:'@#, \n <.>/?`¬§¢¥ƒ¿½¼¡«»©®°±µ¶¾÷ ";
		pixelArtOutlinedTxt.text = "110 Usable Characters\n" + "abcdefghijklmnopqrstuvwxyz \n" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"\n 0123456789" + "!£$%^&*()_-=+{}[];:'@#, \n <.>/?`¬§¢¥ƒ¿½¼¡«»©®°±µ¶¾÷ ";
        pixelArtExtTxt.text = "193 Usable Characters\n" + "!#$%&'()*+,-./:;<=>?@ \n 0123456789 \n ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "\n []^ _`abcdefghijklmnopqrstuvwxyz" + 
            "¢£ \n ¤¥©ª«®º»ÀÁÂÃÄÅÆÇÈÉÊË \n ÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæ \n çèéêëìíîïðñòóôõö÷øùúûüý" + "\n þÿŒœŠšŽžƒ•<>€™←↑→↓▲►▼◄☺☻☼♀♂♠♣♥♦♪♫";
        blockyTxt.text = "193 Usable Characters\n" + "!#$%&'()*+,-./:;<=>?@ \n 0123456789 \n ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "\n []^ _`abcdefghijklmnopqrstuvwxyz" +
            "¢£ \n ¤¥©ª«®º»ÀÁÂÃÄÅÆÇÈÉÊË \n ÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæ \n çèéêëìíîïðñòóôõö÷øùúûüý" + "\n þÿŒœŠšŽžƒ•<>€™←↑→↓▲►▼◄☺☻☼♀♂♠♣♥♦♪♫";
        titleText.text = "JazzCreatePixelArt Font";
		titleThreeDtext.text = "JazzCreatePixelArt 3D Font";
		titleOutlinedText.text = "JazzCreatePixelArtOutlined Font";
		pixelArtExtTitleTxt.text = "JazzCreatePixelArtExt Font";
        blockyTitleTxt.text = "JazzCreate Blocky Font";
		StartFontDemo ();
	}
	IEnumerator CycleFonts()
	{
		yield return new WaitForSeconds (5);
        blocky.SetActive(false);
        blockyTitle.SetActive(false);
        pixelArtFont.SetActive(false);
		pixelArtTitle.SetActive (false);
        pixelArtExtTitle.SetActive(false);
        pixelArtExt.SetActive(false);
        pixelArtOutlinedTitle.SetActive(false);
        pixelArtOutlined.SetActive(false);
        pixelArtThreeD.SetActive(true);
		pixelArtThreeDtitle.SetActive (true);
        yield return new WaitForSeconds (5);
        blocky.SetActive(false);
        blockyTitle.SetActive(false);
        pixelArtFont.SetActive(false);
        pixelArtTitle.SetActive(false);
        pixelArtExtTitle.SetActive(false);
        pixelArtExt.SetActive(false);
        pixelArtThreeDtitle.SetActive (false);
		pixelArtThreeD.SetActive(false);
		pixelArtOutlinedTitle.SetActive (true);
		pixelArtOutlined.SetActive (true);
		yield return new WaitForSeconds (5);
        blocky.SetActive(false);
        blockyTitle.SetActive(false);
        pixelArtFont.SetActive(false);
        pixelArtTitle.SetActive(false);
        pixelArtExtTitle.SetActive(false);
        pixelArtExt.SetActive(false);
        pixelArtThreeD.SetActive(false);
        pixelArtThreeDtitle.SetActive(false);
        pixelArtOutlinedTitle.SetActive (false);
		pixelArtOutlined.SetActive (false);
		pixelArtFont.SetActive(true);
		pixelArtTitle.SetActive (true);
        yield return new WaitForSeconds(5);
        blocky.SetActive(false);
        blockyTitle.SetActive(false);
        pixelArtOutlinedTitle.SetActive(false);
        pixelArtOutlined.SetActive(false);
        pixelArtExtTitle.SetActive(true);
        pixelArtExt.SetActive(true);
        pixelArtThreeD.SetActive(false);
        pixelArtThreeDtitle.SetActive(false);
        pixelArtFont.SetActive(false);
        pixelArtTitle.SetActive(false);
        yield return new WaitForSeconds(5);
        pixelArtOutlinedTitle.SetActive(false);
        pixelArtOutlined.SetActive(false);
        pixelArtExtTitle.SetActive(false);
        pixelArtExt.SetActive(false);
        pixelArtThreeD.SetActive(false);
        pixelArtThreeDtitle.SetActive(false);
        pixelArtFont.SetActive(false);
        pixelArtTitle.SetActive(false);
        blocky.SetActive(true);
        blockyTitle.SetActive(true);
        yield return new WaitForSeconds(5);

        mainCamera.SetActive(false);
        fontCanvas.SetActive(false);
        worldCamera.SetActive(true);
        //StartFontDemo ();
	}
	void StartFontDemo ()
	{
        StopCoroutine("CycleFonts");
		StartCoroutine ("CycleFonts");
	}
}