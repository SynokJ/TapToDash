using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody player_rb;
    private Player player;
    private int score_num = 0;

    public TextMeshProUGUI score_text;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;

    void Awake()
    {
        player_rb = GetComponent<Rigidbody>();
        player = new Player();
    }

    void Update()
    {
        // move 
        transform.Translate(transform.forward * player.speed() * Time.deltaTime);

        if(player.isEnded())
        {
            gameWonPanel.SetActive(true);
            gameObject.SetActive(false);
        }


        // touch system controller 
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                move();
        }
    }

    private void move()
    {

        if (player.getCmds().Count == 0)
            return;

        switch (player.getCurCmd())
        {
            case Player.MoveState.run:
                Debug.Log("Run Wtf?!");
                break;
            case Player.MoveState.left:
                transform.Rotate(new Vector3(0, -player.rotateAngle(), 0));
                break;
            case Player.MoveState.right:
                transform.Rotate(new Vector3(0, player.rotateAngle(), 0));
                break;
            case Player.MoveState.up:
                player_rb.velocity = Vector3.up * player.jump();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            gameOverPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (other.tag == "Collectible")
        {
            score_num++;
            score_text.text = score_num.ToString();
            other.gameObject.SetActive(false);
        }
    }

    public Player getPlayer()
    {
        return player;
    }

    public Vector3 getPlayerPos()
    {
        return this.gameObject.transform.position;
    }
}
