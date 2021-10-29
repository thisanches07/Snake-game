using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGame : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    bool startGame = false;

    #region Unity Methods

    private void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }


    private void Update()
    {

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


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


        head();
        end();

        GL.Begin(GL.QUADS);
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
        GL.Vertex3(-2, 0, 0);

        GL.End();

        GL.PopMatrix();
    }

    public void head()
    {
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 0.5f, 0);
        GL.Vertex3(0.5f, 0.5f, 0);
        GL.Vertex3(0.5f, 0, 0);

        GL.Vertex3(0.5f, 0, 0);
        GL.Vertex3(1, 0, 0);
        GL.Vertex3(0.5f, 0.5f, 0);
        GL.Vertex3(1, 0.5f, 0);

        GL.End();
        Eyes();
        Tongue();

        
    }
    void Eyes() {
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(0.2f, 0.4f, 0);
        GL.Vertex3(0.2f, 0.3f, 0);
        GL.Vertex3(0.5f, 0.3f, 0);
        GL.Vertex3(0.5f, 0.4f, 0);

        GL.Vertex3(0.2f, 0.1f, 0);
        GL.Vertex3(0.2f, 0.2f, 0);
        GL.Vertex3(0.5f, 0.2f, 0);
        GL.Vertex3(0.5f, 0.1f, 0);

        GL.Color(Color.red);
        GL.Vertex3(0.3f, 0.4f, 0);
        GL.Vertex3(0.3f, 0.3f, 0);
        GL.Vertex3(0.35f, 0.3f, 0);
        GL.Vertex3(0.35f, 0.4f, 0);

        GL.Vertex3(0.3f, 0.1f, 0);
        GL.Vertex3(0.3f, 0.2f, 0);
        GL.Vertex3(0.35f, 0.2f, 0);
        GL.Vertex3(0.35f, 0.1f, 0);


        GL.End();
    }
    void Tongue() {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(new Vector3(0.8f, 0.25f, 0));
        GL.Vertex(new Vector3(1.1f, 0.25f, 0));

        GL.Vertex(new Vector3(1.1f, 0.25f, 0));
        GL.Vertex(new Vector3(1.5f, 0.5f, 0));

        GL.Vertex(new Vector3(1.1f, 0.25f, 0));
        GL.Vertex(new Vector3(1.5f, 0, 0));
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

    #endregion
}
