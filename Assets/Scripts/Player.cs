using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject saida;

    public float lineVelocity = 1f;

    public TMP_Text vidas;


    
    public enum states { idle, going, shoot};

    public states currentState = states.idle;

    private GameObject proj;

    public Vector3 projColisionPoint;

    private GameObject anteriorP;

    private IEnumerator currentCoroutine;

    private int vida = 3;

    void Start()
    {
        Debug.Log(new Vector2(1, 0) * 0.5f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized * 100;
        //Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints((Vector2)mainCamera.ScreenToWorldPoint(transform.position) + dir, (Vector2)mainCamera.ScreenToWorldPoint(transform.position));
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        FSM(dir);
        
    }

    private void FSM(Vector2 dir)
    {
        if (currentState == states.idle)
        {
            GetComponent<TrailRenderer>().emitting = false;
            _lineRenderer.SetPosition(0, transform.position + (Vector3)dir.normalized * 10);
            _lineRenderer.SetPosition(1, saida.transform.position);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                currentState = states.shoot;
            }
        }
        else if(currentState == states.going)
        {
            GetComponent<TrailRenderer>().emitting = true;
            _lineRenderer.SetPosition(0, projColisionPoint);
            _lineRenderer.SetPosition(1, transform.position);
        }
        else if(currentState == states.shoot)
        {
            _lineRenderer.SetPosition(0, proj.transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }

    }

    public IEnumerator FreezeFrame(int frames)
    {
        //yield return null;
        while (frames > 0)
        {
            Camera.main.clearFlags = CameraClearFlags.Nothing;
            yield return null;
            Camera.main.cullingMask = 0;
            frames--;
        }

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.cullingMask = ~0;
    }

    private void Shoot()
    {
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        proj = Instantiate(projectile);
        Projectile script = proj.GetComponent<PlayerHook>();
        script.dir = dir;
        script.velocity = lineVelocity;
        script.player = gameObject;
        if(anteriorP != null)
        {
            script.anterior = anteriorP;
        }
        anteriorP = proj;
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(proj.transform.position);
        float angle = AngleBetweenTwoPoints(mousePos, positionOnScreen);
        proj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - (float)1.5708));

        proj.transform.position = saida.transform.position;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    IEnumerator ToProj(Vector3 pos, float velocity, GameObject projectil, Vector2 normal)
    {
        currentState = states.going;
        while(transform.position != pos)
        {
            Vector3 adjustedPos = new Vector3(pos.x, pos.y, pos.z);
            transform.position = Vector3.MoveTowards(transform.position, adjustedPos, velocity * Time.deltaTime);
            if(Vector3.Distance(transform.position, adjustedPos) <= GetComponent<CircleCollider2D>().radius)
            {
                break;
            }
            yield return null;
        }
        //Destroy(projectil);
        currentState = states.idle;
    }

    public void StartTrajectory(Vector2 pos, float velocity, GameObject projectil, GameObject anteriorProjectil, Vector2 normal)
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            Destroy(anteriorProjectil);
        }
        currentCoroutine = ToProj(pos, velocity, projectil, normal);
        StartCoroutine(currentCoroutine);
    }

    public void Passed()
    {
        vida--;
        vidas.text = vida.ToString();
        if ( vida == 0)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}