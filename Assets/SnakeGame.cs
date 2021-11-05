using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SnakeGame : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    bool startGame = false;
    float by = 0;
    float bx = 0;
    float velo = 0.02f;
    public int life = 3;
    public int score = 0;
    public char move = 'n';
    public bool rotate = false;
    public float mGR = 2;
    public float mGL = -2;


    #region Unity Methods

    private void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        move = 'n';
    }


    private void Update()
    {

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = 'l';
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = 'r';
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotation('u');
            move = 'u';
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move = 'd';
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            move = 'z';
            by = 0;
            bx = 0;
        }

        Move();
        Collision();

    }
    private void OnPostRender()
    {
        if (startGame)
        {
            Arena();
            Snake();
        }
    }
    #endregion


    #region My Methods

    public void StartGame()
    {
        startGame = true;
    }

    void Arena() {
        BarBottom();
        BarLeft();
        BarRight();
        BarTop();
    }

    void BarTop()
    {

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(sb.x * (-1), sb.y, 0);
        GL.Vertex3(sb.x * (-1), sb.y - 1, 0);
        GL.Vertex3(0, sb.y - 1, 0);
        GL.Vertex3(0, sb.y, 0);



        GL.Vertex3(0, sb.y, 0);
        GL.Vertex3(0, sb.y - 1, 0);
        GL.Vertex3(sb.x, sb.y - 1, 0);
        GL.Vertex3(sb.x, sb.y, 0);

        GL.End();
        GL.PopMatrix();
    }

    void BarBottom()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
        GL.Vertex3(sb.x * (-1), 1 + sb.y * (-1), 0);
        GL.Vertex3(sb.x, 1 + sb.y * (-1), 0);
        GL.Vertex3(sb.x, sb.y * (-1), 0);

        GL.End();
        GL.PopMatrix();
    }

    void BarLeft()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
        GL.Vertex3(sb.x * (-1), sb.y, 0);
        GL.Vertex3(sb.x * (-1) + 1, sb.y, 0);
        GL.Vertex3(sb.x * (-1) + 1, sb.y * (-1), 0);

        GL.End();
        GL.PopMatrix();
    }

    void BarRight()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(sb.x, sb.y * (-1), 0);
        GL.Vertex3(sb.x, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y * (-1), 0);

        GL.End();
        GL.PopMatrix();
    }


    void Snake()
    {

        GL.PushMatrix();
        mat.SetPass(0);

        Debug.Log(move);
        if (move == 'n')
            headRight();
        if (move == 'u')
            headUp();            
        if (move == 'd')
            headDown();
        if (move == 'l')
            headLeft();
        if (move == 'r')
            headRight();
            
        end();

        /*GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 0.5f, 0);
        GL.Vertex3(-0.5f, 0.5f, 0);
        GL.Vertex3(-0.5f, 0, 0);


        GL.Vertex3(-0.5f, 0, 0);
        GL.Vertex3(-0.5f, 0.5f, 0);
        GL.Vertex3(-1, 0.5f, 0);
        GL.Vertex3(-1, 0, 0);

        GL.Vertex3(-1, 0, 0);
        GL.Vertex3(-1, 0.5f, 0);
        GL.Vertex3(-1.5f, 0.5f, 0);
        GL.Vertex3(-1.5f, 0, 0);

        GL.Vertex3(-1.5f, 0, 0);
        GL.Vertex3(-1.5f, 0.5f, 0);
        GL.Vertex3(-2, 0.5f, 0);
        GL.Vertex3(-2, 0, 0);*/

        GL.End();

        GL.PopMatrix();
    }

    public void headRight()
    {
        Debug.Log("RIGHT");
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx, by + 0.5f, 0);
        GL.Vertex3(bx + 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx + 0.5f, by, 0);

        GL.Vertex3(bx + 0.5f, by, 0);
        GL.Vertex3(bx + 1, by, 0);
        GL.Vertex3(bx + 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx + 1, by + 0.5f, 0);

        GL.End();
        //Eyes
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(bx + 0.2f, by + 0.4f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.5f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.5f, by + 0.4f, 0);

        GL.Vertex3(bx + 0.2f, by + 0.1f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.5f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.5f, by + 0.1f, 0);

        GL.Color(Color.red);
        GL.Vertex3(bx + 0.3f, by + 0.4f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.35f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.35f, by + 0.4f, 0);

        GL.Vertex3(bx + 0.3f, by + 0.1f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.35f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.35f, by + 0.1f, 0);
        GL.End();

        //Tongue
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(bx + 0.8f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx + 1.1f, by + 0.25f, 0));

        GL.Vertex(new Vector3(bx + 1.1f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx + 1.5f, by + 0.5f, 0));

        GL.Vertex(new Vector3(bx + 1.1f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx + 1.5f, by, 0));
        GL.End();


    }


    public void headLeft()
    {
        Debug.Log("LEFT");
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx, by + 0.5f, 0);
        GL.Vertex3(bx - 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx - 0.5f, by, 0);

        GL.Vertex3(bx - 0.5f, by, 0);
        GL.Vertex3(bx - 1, by, 0);
        GL.Vertex3(bx - 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx - 1, by + 0.5f, 0);

        GL.End();
        //eyes
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(bx - 0.2f, by + 0.4f, 0);
        GL.Vertex3(bx - 0.2f, by + 0.3f, 0);
        GL.Vertex3(bx - 0.5f, by + 0.3f, 0);
        GL.Vertex3(bx - 0.5f, by + 0.4f, 0);

        GL.Vertex3(bx - 0.2f, by + 0.1f, 0);
        GL.Vertex3(bx - 0.2f, by + 0.2f, 0);
        GL.Vertex3(bx - 0.5f, by + 0.2f, 0);
        GL.Vertex3(bx - 0.5f, by + 0.1f, 0);

        GL.Color(Color.red);
        GL.Vertex3(bx - 0.3f, by + 0.4f, 0);
        GL.Vertex3(bx - 0.3f, by + 0.3f, 0);
        GL.Vertex3(bx - 0.35f, by + 0.3f, 0);
        GL.Vertex3(bx - 0.35f, by + 0.4f, 0);

        GL.Vertex3(bx - 0.3f, by + 0.1f, 0);
        GL.Vertex3(bx - 0.3f, by + 0.2f, 0);
        GL.Vertex3(bx - 0.35f, by + 0.2f, 0);
        GL.Vertex3(bx - 0.35f, by + 0.1f, 0);


        GL.End();

        //Tongue
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(bx - 0.8f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx - 1.1f, by + 0.25f, 0));

        GL.Vertex(new Vector3(bx - 1.1f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx - 1.5f, by + 0.5f, 0));

        GL.Vertex(new Vector3(bx - 1.1f, by + 0.25f, 0));
        GL.Vertex(new Vector3(bx - 1.5f, by, 0));
        GL.End();


    }

   public  void headUp()
    {
        Debug.Log("UP");
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx + 0.5f, by , 0);
        GL.Vertex3(bx + 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx , by + 0.5f, 0);

        GL.Vertex3(bx, by + 0.5f, 0);
        GL.Vertex3(bx , by + 1, 0);
        GL.Vertex3(bx + 0.5f, by + 0.5f, 0);
        GL.Vertex3(bx + 0.5f, by + 1, 0);

        GL.End();
        //Eyes
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(bx + 0.4f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.5f, 0);
        GL.Vertex3(bx + 0.4f, by + 0.5f, 0);

        GL.Vertex3(bx + 0.1f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.2f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.5f, 0);
        GL.Vertex3(bx + 0.1f, by + 0.5f, 0);

        GL.Color(Color.red);
        GL.Vertex3(bx + 0.4f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.3f, by + 0.35f, 0);
        GL.Vertex3(bx + 0.4f, by + 0.35f, 0);

        GL.Vertex3(bx + 0.1f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.3f, 0);
        GL.Vertex3(bx + 0.2f, by + 0.35f, 0);
        GL.Vertex3(bx + 0.1f, by + 0.35f, 0);
        GL.End();

        //Tongue
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(bx + 0.25f, by + 0.8f, 0));
        GL.Vertex(new Vector3(bx + 0.25f, by + 1.1f, 0));

        GL.Vertex(new Vector3(bx + 0.25f, by + 1.1f, 0));
        GL.Vertex(new Vector3(bx + 0.5f, by + 1.5f, 0));

        GL.Vertex(new Vector3(bx + 0.25f, by + 1.1f, 0));
        GL.Vertex(new Vector3(bx , by + 1.5f, 0));
        GL.End();
    }

    public void headDown()
    {
        Debug.Log("DOWN");
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx + 0.5f, by, 0);
        GL.Vertex3(bx + 0.5f, by - 0.5f, 0);
        GL.Vertex3(bx, by - 0.5f, 0);

        GL.Vertex3(bx, by - 0.5f, 0);
        GL.Vertex3(bx, by - 1, 0);
        GL.Vertex3(bx + 0.5f, by - 0.5f, 0);
        GL.Vertex3(bx + 0.5f, by - 1, 0);

        GL.End();
        //Eyes
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(bx + 0.4f, by - 0.2f, 0);
        GL.Vertex3(bx + 0.3f, by - 0.2f, 0);
        GL.Vertex3(bx + 0.3f, by - 0.5f, 0);
        GL.Vertex3(bx + 0.4f, by - 0.5f, 0);

        GL.Vertex3(bx + 0.1f, by - 0.2f, 0);
        GL.Vertex3(bx + 0.2f, by - 0.2f, 0);
        GL.Vertex3(bx + 0.2f, by - 0.5f, 0);
        GL.Vertex3(bx + 0.1f, by - 0.5f, 0);

        GL.Color(Color.red);
        GL.Vertex3(bx + 0.4f, by - 0.3f, 0);
        GL.Vertex3(bx + 0.3f, by - 0.3f, 0);
        GL.Vertex3(bx + 0.3f, by - 0.35f, 0);
        GL.Vertex3(bx + 0.4f, by - 0.35f, 0);

        GL.Vertex3(bx + 0.1f, by - 0.3f, 0);
        GL.Vertex3(bx + 0.2f, by - 0.3f, 0);
        GL.Vertex3(bx + 0.2f, by - 0.35f, 0);
        GL.Vertex3(bx + 0.1f, by - 0.35f, 0);
        GL.End();

        //Tongue
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(bx + 0.25f, by - 0.8f, 0));
        GL.Vertex(new Vector3(bx + 0.25f, by - 1.1f, 0));

        GL.Vertex(new Vector3(bx + 0.25f, by - 1.1f, 0));
        GL.Vertex(new Vector3(bx + 0.5f, by - 1.5f, 0));

        GL.Vertex(new Vector3(bx + 0.25f, by - 1.1f, 0));
        GL.Vertex(new Vector3(bx, by - 1.5f, 0));
        GL.End();


    }
    
    public void end()
    {
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex(new Vector3(-2, 0, 0));
        GL.Vertex(new Vector3(-2.5f, 0.25f, 0));
        GL.Vertex(new Vector3(-2.5f, 0.25f, 0));
        GL.Vertex(new Vector3(-2, 0.5f, 0));
        GL.End();
    }

    void Move()
    {

        if(move=='l')
        {
            //rotacionar
            rotate = true;
            rotation('l');
            bx -= velo;
        }
        if (move=='r')
        {
            //rotacionar
            rotate = true;
            rotation('r');
            bx += velo;
        }
        if (move=='u')
        {
            //rotacionar
            rotate = true;
            rotation('u');
            by += velo;
        }
        if (move=='d')
        {
            //rotacionar
            rotate = true;
            rotation('d');
            by -= velo;
        }

        
    }

     void rotation(char direction)
    {
        if (direction == 'u' && rotate)
        {
            rotate = false;
            headUp();            
        }
        if (direction == 'd' && rotate)
        {
            rotate = false;
            headDown();
        }
        if (direction == 'l' && rotate)
        {
            rotate = false;
            headLeft();
        }
        if (direction == 'r' && rotate)
        {
            rotate = false;
            headRight();
        }
    }
    void Collision()
    {
        if ((by + 1) >= (sb.y-1) || (by - 1) <= (-sb.y))
        {
            life--;
            by = 0;
            bx = 0;
        }
        if ((bx + 1) >= (sb.x - 1) || (bx - 1) <= (-sb.x + 1))
        {
            life--;
            bx = 0;
            by = 0;

        }

    }
    #endregion
}
