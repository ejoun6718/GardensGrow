﻿using UnityEngine;
using System.Collections;

public class Spikes : TerrainObject {
	public int damage = 2;
    //public int framesPerHit = 50;
    public float spikeCD;

	public bool toggleable = false;
	public bool spikesUp = false;

	private int currentFrame = 0;
    private Animator anim;
    private bool striked = false;

	public bool activeCollider;

	// Use this for initialization
	protected override void Start () {
		base.Start();
        anim = this.gameObject.GetComponent<Animator>();
		if (!activeCollider) {
			BoxCollider2D thisCollider = this.gameObject.GetComponent<BoxCollider2D>();
			thisCollider.enabled = false;
			Destroy(transform.GetComponent<Rigidbody>());
		}

		if (toggleable) {
			anim.SetBool("Hit", false);
			anim.SetBool("SpikesUp", spikesUp);
		}
	}
    //called when re enabled
    void OnEnable()
    {
        anim.SetBool("SpikesUp", spikesUp);
    }
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
            StartCoroutine(spikeUpWait(other));
		}
        if (striked)
        {
            other.gameObject.GetComponent<PlayerGridObject>().TakeDamage(damage);
        }
	}
    IEnumerator spikeUpWait(Collider2D other)
    { 
        PlayerGridObject player = other.GetComponent<PlayerGridObject>();

        if (toggleable == false)
        {
           if (player.onPlatform == false && !striked)
           {
                yield return new WaitForSeconds(spikeCD);
                if (other.IsTouching(this.gameObject.GetComponent<Collider2D>()))
                {
                    player.TakeDamage(damage);
                }
                anim.SetBool("SpikesUp", true);
                striked = true;
                StartCoroutine(Wait());
           }
         }
        else
        {
            if (spikesUp)
            {
                player.TakeDamage(damage);
                player.gameObject.transform.position = Globals.spawnLocation;
            }
        }
    }
    //method for animation ease
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("SpikesUp", false);
        yield return new WaitForSeconds(.5f);
        striked = false;
    }
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			currentFrame = 0;
		}
	}

	public void Toggle() {
		if (spikesUp) {
			spikesUp = false;
			anim.SetBool("SpikesUp", false);
		}
		else {
			spikesUp = true;
			anim.SetBool("SpikesUp", true);
		}
	}
}