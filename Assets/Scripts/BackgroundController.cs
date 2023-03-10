using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject leftPane;
    private GameObject rightPane;

    private GameObject leftCharacter;
    private GameObject rightCharacter;

    private GameObject newCharacter;
    private SpriteRenderer cSpriteRenderer;

    public Sprite[] sprites;

    private int count = 0;

    private void Awake()
    {
        EventManager.GENERATE_ROOM += executeTransition;
        EventManager.COMMISERATE_LOSE += shakeCharacter;
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.transform.position = new Vector3(0, 0, 0);

        leftPane = transform.GetChild(0).gameObject;
        rightPane = transform.GetChild(1).gameObject;
        
        leftCharacter = generateCharacter(leftPane, 0f, 0);

    }


    /**
    * Spawns a new character and shifts the panes over.
    */
    public void executeTransition() {

        rightCharacter = generateCharacter(rightPane, 20.498f, Random.Range(1, sprites.Length));
        audioSource.Play();
        translatePanes();

    }


    private GameObject generateCharacter(GameObject appendTo, float xPos, int sIndex) {

        newCharacter = new GameObject();
        newCharacter.name = "Character" + count;
        count++;

        newCharacter.transform.localScale = new Vector3(7.4f, 7.4f, 1f);
        newCharacter.transform.position = new Vector3(xPos, 0f, -0.1f);

        cSpriteRenderer = newCharacter.AddComponent<SpriteRenderer>();
        cSpriteRenderer.sprite = sprites[sIndex];

        newCharacter.transform.parent = appendTo.transform;

        return newCharacter;

    }


    private void translatePanes() {

        gameObject.transform.position = new Vector3(0, 0, 0);
        
        LeanTween.moveX(gameObject, -20.498f, 6.0f)
                 .setEase(LeanTweenType.easeInOutQuart)
                 .setOnComplete(() => { 

                    Destroy(leftCharacter);

                    gameObject.transform.position = new Vector3(0, 0, 0);

                    rightCharacter.transform.parent = leftPane.transform;
                    rightCharacter.transform.position = new Vector3(0f, 0f, -0.1f);

                    leftCharacter = rightCharacter;

                    EventManager.StartRoom();

                 });

    }


    public void shakeCharacter() {

        Vector3 initialLoc = leftCharacter.transform.position;
        
        LeanTween.moveX(leftCharacter, 0.3f, 0.2f)
                 .setEase(LeanTweenType.easeShake)
                 .setOnComplete(() => {

                    LeanTween.moveX(leftCharacter, 0.2f, 0.2f)
                        .setEase(LeanTweenType.easeShake);

                 });

    }


}
