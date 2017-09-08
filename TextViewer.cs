using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextViewer : MonoBehaviour {

    public List<Text> texts;

    public float pagingTime = 1f;

    protected int position = 0;

    public int Position
    {
        get
        {
            return position;
        }
    }

    protected int nextPosition = 0;

    protected bool pagingActive = false;

    public bool IsPagingActive {
        get
        {
            return pagingActive;
        }
    }

    public UnityEvent onAllViewed;

    // Use this for initialization
    void Start () {
        Color c;

        for (int i = 1; i < texts.Count; i++)
        {
            texts[i].enabled = false;
            c = texts[i].color; c.a = 0;
            texts[i].color = c;
        }
	}

	// Update is called once per frame
	void Update () {
		
        if (pagingActive)
        {
            OnPagingActive();
        }

	}

    public virtual void Next()
    {
        if (pagingActive) return;

        if (position + 1 < texts.Count)
        {
            nextPosition = position + 1;
            pagingActive = true;
        }
        else
        {
            onAllViewed.Invoke();
        }
    }

    public virtual void Prev()
    {
        if (pagingActive) return;

        if (position > 0)
        {
            nextPosition = position - 1;
            pagingActive = true;
        }
    }

    protected virtual void OnPagingActive()
    {
        if (texts[position].color.a > 0)
        {
            // Currently Page Visible
            Color c = texts[position].color;
            c.a -= Time.deltaTime / (pagingTime / 2f);

            if (c.a < 0)
            {
                c.a = 0;
            }

            texts[position].color = c;
        }
        else
        {
            // Currently Page InVisible
            texts[position].enabled = false;
            texts[nextPosition].enabled = true;

            Color c = texts[nextPosition].color;
            c.a += Time.deltaTime / (pagingTime / 2f);

            if (c.a > 1)
            {
                c.a = 1;
            }

            texts[nextPosition].color = c;

            if (c.a == 1)
            {
                position = nextPosition;
                pagingActive = false;
            }
        }
    }
}
