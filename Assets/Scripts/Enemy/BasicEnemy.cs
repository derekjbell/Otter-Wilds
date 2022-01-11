using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System;

public class BasicEnemy : MonoBehaviour
{
    Tilemap bugmap;
    MoveInterface movement;
    MoveArbiter arbiter;
    int step_index = 0;
    public GameObject enemy;
    public GameObject thought_bubble;
    SpriteRenderer next_move_sprite;

    [SerializeField]
    List<MoveType> move_steps = new List<MoveType>();
    EnemyHitboxDetection enemy_hitbox_script;

    private void Awake()
    {
        arbiter = GameObject.Find("/MoveArbiter").GetComponent<MoveArbiter>();
        bugmap = GameObject.Find("/Grid/Bugmap").GetComponent<Tilemap>();
        next_move_sprite = thought_bubble.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        enemy_hitbox_script = enemy.transform.GetChild(2).gameObject.GetComponent<EnemyHitboxDetection>();
    }

    void Start()
    {
        arbiter.OnTick += Tick;
        movement = GetComponent<MoveInterface>();
    }

    private void Update()
    {
        UpdateNextMoveTooltip();
    }

    public void CollisionDetected(EnemyHitboxDetection childScript)
    {
        Debug.Log("child collided");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnMouseEnter()
    {
        thought_bubble.SetActive(true);
    }

    private void OnMouseExit()
    {
        thought_bubble.SetActive(false);
    }

    void UpdateNextMoveTooltip()
    {
        Sprite[] move_sprites = Resources.LoadAll<Sprite>("MoveTileSprites/icons-Sheet");

        foreach (var s in move_sprites)
        {
            if (move_steps[step_index].ToString().Equals(s.name))
            {
                next_move_sprite.sprite = s;
            }
        }
    }

    void Tick()
    {
        MoveType bug = Tiles.LookupBug(bugmap.GetTile(movement.position));
        if (bug != MoveType.None)
        {
            movement.SetMove(bug);
            bugmap.SetTile(movement.position, null);
        }
        else if (move_steps.Count > 0)
        {
            movement.SetMove(move_steps[step_index]);
            step_index = (step_index + 1) % move_steps.Count;
        }
    }
}
