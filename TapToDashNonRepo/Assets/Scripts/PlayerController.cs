using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody player_rb;
    private Player player;
    private int score_num = 0;
    private float cur_player_speed = 0;
    private Character character;
    private float max_player_speed = 12f;

    public LevelManagerJson levelManager;
    public AudioSource loseAudio;
    public TextMeshProUGUI score_text;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public List<Character> character_box;
    public Animator anim;

    void Awake()
    {
        player_rb = GetComponent<Rigidbody>();

        setChar();
        player = new Player(character.speed);
        cur_player_speed = player.GetSpeed();
        gameObject.GetComponent<MeshRenderer>().material = character.material;

        if(character.test_mesh != null)
        {
            gameObject.GetComponent<MeshRenderer>().materials = character.test_material;
            gameObject.GetComponent<MeshFilter>().mesh = character.test_mesh;
        }
    }

    void Update()
    {
        // move 
        transform.Translate(transform.forward * cur_player_speed * Time.deltaTime);

        if (player.IsEnded())
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

    public void IncreaseSpeed()
    {
        if (cur_player_speed < max_player_speed)
            cur_player_speed += 0.5f;
    }

    private void move()
    {

        if (player.GetCommandsContainer().Count == 0)
            return;

        switch (player.GetCurCommand())
        {
            case Player.MoveState.run:
                Debug.Log("Run Wtf?!");
                break;
            case Player.MoveState.left:
                transform.Rotate(new Vector3(0, -player.GetRotationAngle(), 0));
                break;
            case Player.MoveState.right:
                transform.Rotate(new Vector3(0, player.GetRotationAngle(), 0));
                break;
            case Player.MoveState.up:
                player_rb.velocity = Vector3.up * player.GetJumpScale();
                anim.SetTrigger("doJump");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            gameOverPanel.SetActive(true);
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("CurLevel", levelManager.GetCurLevel());
            loseAudio.Play();
        }
        else if (other.tag == "Collectible")
        {
            score_num++;
            score_text.text = score_num.ToString();
            other.gameObject.SetActive(false);

            setMoney();
        }
    }

    public void setMoney()
    {
        PlayerPrefs.SetInt("CoinNumTemp", score_num);
    }

    public Player getPlayer()
    {
        return player;
    }

    public Vector3 getPlayerPos()
    {
        return this.gameObject.transform.position;
    }

    public void setChar()
    {
        string skin_name = PlayerPrefs.GetString("SkinName", "def");

        foreach (Character ch in character_box)
            if (ch.name == skin_name)
                character = ch;

        if (character == null)
            character = character_box[0];
    }
}
