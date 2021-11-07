using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Slingshooter SlingShooter;

    public List<bird> Birds;
    public List<Enemy> Enemies;

    private bool _isGameEnded = false;

    public TrailController TrailController;

    private bird _shotBird;
    public BoxCollider2D TapCollider;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;   
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }   
    public void AssignTrail(bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            SceneManager.LoadScene("Main");
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
