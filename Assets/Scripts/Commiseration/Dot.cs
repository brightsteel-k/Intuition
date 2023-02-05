using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public static Vector2[][] ALL_PATHS = new Vector2[][] { null, null, null, null, null, null, null, null };
    private Vector2[] path;
    private int pathIndex = 0;
    private int chordIndex;
    private RawImage image;
    [SerializeField] private float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y <= 288)
        {
            Debug.Log("Clickable!");
        }
    }

    void nextNode()
    {
        float distance = Vector2.Distance(path[pathIndex], path[pathIndex + 1]);
        LeanTween.moveLocal(gameObject, path[pathIndex + 1], distance / speed)
            .setOnComplete(reachNode);
    }

    void reachNode()
    {
        pathIndex++;
        if (pathIndex == path.Length - 1)
            finishPath();
        else
            nextNode();
    }

    void finishPath()
    {
        ChordManager.chordFailure(chordIndex);
    }


    public void initialize(int pathIndex, float speedIn, int chordIn, Color32 colourIn)
    {
        image = GetComponent<RawImage>();
        path = ALL_PATHS[pathIndex];
        chordIndex = chordIn;
        speed = speedIn;
        image.color = colourIn;
        nextNode();
    }
}
