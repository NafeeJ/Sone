using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1Controller : MonoBehaviour
{
    private Animator myAnimator;
    private SpriteRenderer theSpriteRenderer;

    public float timeTilFlashing;
    public float flashingTime;
    private bool flashing;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        dead = false;
    }

    void Update()
    {
        if (flashing)
        {
            flashingTime -= Time.deltaTime;
        }
    }

    public IEnumerator DieCoroutine()
    {
        myAnimator.SetBool("death", true);
        dead = true;
        yield return new WaitForSeconds(timeTilFlashing);
        StartCoroutine(Flashing());
        yield return new WaitForSeconds(flashingTime);
        gameObject.SetActive(false);

    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    public IEnumerator Flashing()
    {
        flashing = true;
        while (flashingTime > 0)
        {
            theSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            theSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
