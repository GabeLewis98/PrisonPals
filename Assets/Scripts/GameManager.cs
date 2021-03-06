using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> arms = new List<GameObject>();
    private GameObject currentArm;
    public Draw drawScript;
    public float minimumScore = 2f;
    public float mediumScore = 4f;
    public Transform armStart;
    public Text scoreDisplay;
    public float endDelay = 2f;
    public Sprite goodSprite;
    public Sprite medSprite;
    public Sprite badSprite;

    private WaitForSeconds endDelayWait;

    // Start is called before the first frame update
    void Start()
    {
        endDelayWait = new WaitForSeconds(endDelay);
        StartCoroutine(addArm());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator addArm()
    {
        if (currentArm != null)
        {
            Destroy(currentArm);
        }

        int rnd = Random.Range(0, arms.Count);
        currentArm = Instantiate(arms[rnd], armStart.position, armStart.rotation);

        yield return new WaitForEndOfFrame();

        GameObject stencil = GameObject.FindGameObjectWithTag("Stencil");
        EdgeCollider2D col = stencil.GetComponent<EdgeCollider2D>();

        if (col != null)
            drawScript.setUpTattoo(col);

    }

    public IEnumerator EndTatto()
    {
        float totalScore = drawScript.getFinalScore();
        //SpriteRenderer sr = currentArm.transform.GetChild(1).GetComponent<SpriteRenderer>();

        if (totalScore <= minimumScore)
        {
            //Display good tattoo
            scoreDisplay.text = ("Perfect ");
            currentArm.transform.GetChild(1).gameObject.SetActive(true);
            //sr.sprite = goodSprite;


        }
        else if (totalScore > minimumScore && totalScore <= mediumScore)
        {
            //Display medium tattoo
            scoreDisplay.text = ("Ok ");
            currentArm.transform.GetChild(2).gameObject.SetActive(true);
            //sr.sprite = medSprite;
        }
        else
        {
            //Display worst tattoo
            scoreDisplay.text = ("Terrible ");
            currentArm.transform.GetChild(3).gameObject.SetActive(true);
            //sr.sprite = badSprite;
        }
        //sr.color = Color.white;
        yield return endDelayWait;
        StartCoroutine(addArm());
        scoreDisplay.text = "";
    }
}
