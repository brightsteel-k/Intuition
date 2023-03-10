using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtBoard : MonoBehaviour
{
    [SerializeField] WordBank wordBank;
    [SerializeField] Button sb;
    [SerializeField] GameObject sanityOverlay;
    [SerializeField] GameObject thoughtBoardOverlay;
    [SerializeField] GameObject nebula;
    static AudioSource audioSource;
    Emotion chosen;

    private void Awake()
    {
        EventManager.GENERATE_ROOM += disableBoard;
        EventManager.START_ROOM += onTransitionCompleted;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!EventManager.COMMISERATING && EventManager.TRANSITION_COMPLETED)
        {
            chosen = readInitialChordInput();

            if (chosen != Emotion.None)
                wordBank.setWord(chosen);
        }
    }

    Emotion readInitialChordInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.S))
                return Emotion.Envy;
            else if (Input.GetKey(KeyCode.D))
                return Emotion.Aggression;
            else if (Input.GetKey(KeyCode.F))
                return Emotion.Powerless;
            else
                return Emotion.Anger;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
                return Emotion.Envy;
            else if (Input.GetKey(KeyCode.D))
                return Emotion.Pessimism;
            else if (Input.GetKey(KeyCode.F))
                return Emotion.Despair;
            else
                return Emotion.Sadness;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                return Emotion.Aggression;
            else if (Input.GetKey(KeyCode.S))
                return Emotion.Pessimism;
            else if (Input.GetKey(KeyCode.F))
                return Emotion.Anxiety;
            else
                return Emotion.Anticipation;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKey(KeyCode.A))
                return Emotion.Powerless;
            else if (Input.GetKey(KeyCode.S))
                return Emotion.Despair;
            else if (Input.GetKey(KeyCode.D))
                return Emotion.Anxiety;
            else
                return Emotion.Fear;
        }

        return Emotion.None;
    }

    public static void playHurtSound()
    {
        audioSource.Play();
    }

    void disableBoard()
    {
        sb.enabled = false;
        sanityOverlay.SetActive(true);
        thoughtBoardOverlay.gameObject.SetActive(true);
    }

    void onTransitionCompleted()
    {
        sb.enabled = true;
        sanityOverlay.SetActive(false);
        thoughtBoardOverlay.SetActive(false);
    }
}