using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeGame : MonoBehaviour
{
    [SerializeField] private GameObject pnl_GameOver;
    [SerializeField] private GameObject pnl_Score;

    public Material material;
    public Vector2 sb;
    public int numBlock = 2;
    public float velocidade = 0.1f;
    private float xAux = 0;
    private float yAux = 0;
    private char direcao = 'D';
    private bool free = false;
    private bool play = true;
    protected Snake snake = new Snake();
    private bool timer = false;
    public Text ts;
    public Text ts2;


    // Start is called before the first frame update
    public void Start()
    {
        
        snake = new Snake();
        ts.text = snake.score.ToString();
        ts2.text = snake.score.ToString();
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    }

    public void Reset(){
        numBlock = 2;
        velocidade = 0.1f;
        xAux = 0;
        yAux = 0;
        direcao = 'D';
        free = false;
        play = true;
        snake = null;
    }

    // Update is called once per frame
    void Update()
    {

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if(!timer)
        {
            
            StartCoroutine(CountDown(0.05f));


            // Logica para setar a posi��o
            if (Input.GetKey(KeyCode.LeftArrow) && direcao != 'D' && free)
            {
                direcao = 'E';
                free = false;
            }
            if (Input.GetKey(KeyCode.RightArrow) && direcao != 'E' && free)
            {
                direcao = 'D';
                free = false;
            }
            if (Input.GetKey(KeyCode.DownArrow) && direcao != 'C' && free)
            {
                direcao = 'B';
                free = false;
            }
            if (Input.GetKey(KeyCode.UpArrow) && direcao != 'B' && free)
            {
                direcao = 'C';
                free = false;
            }

            // Logica dos passos
            if (direcao == 'E' && play)
            {
                xAux = xAux - velocidade;
            }

            if (direcao == 'D' && play)
            {
                xAux = xAux + velocidade;
            }

            if (direcao == 'B' && play)
            {
                yAux = yAux - velocidade;
            }

            if (direcao == 'C' && play)
            {
                yAux = yAux + velocidade;
            }

            // Logica Atualiza a dire��o dos blocos
            if (xAux >= 1)
            {
                xAux = 0;
                snake.update('D');
                free = true;
                play = snake.checkCrash();
            }

            if (xAux <= -1)
            {
                xAux = 0;
                snake.update('E');
                free = true;
                play = snake.checkCrash();
            }

            if (yAux >= 1)
            {
                yAux = 0;
                snake.update('C');
                free = true;
                play = snake.checkCrash();
            }

            if (yAux <= -1)
            {
                yAux = 0;
                snake.update('B');
                free = true;
                play = snake.checkCrash();
            }

            if(!play){
                StartCoroutine(GameOverSequence());
            }

            // recome�ar
            if (Input.GetKey(KeyCode.Escape))
            {
                direcao = 'D';
                snake = new Snake(-10.0f, 0.0f);
                play = true;
            }

        }

        

        ts.text = snake.score.ToString();
        ts2.text = snake.score.ToString();

    }

    private IEnumerator GameOverSequence()
    {
        pnl_GameOver.SetActive(true);

        pnl_Score.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        
    }

    IEnumerator CountDown(float delay)
    {
        timer = true;
        yield return new WaitForSeconds(delay);
        timer= false;
    }

    private void OnPostRender()
    {
        BarTop();
        BarBottom();
        BarLeft();
        BarRight();
        Body();
    }

    void Body()
    {
        GL.PushMatrix();
        material.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);

        snake.drawBox();


        GL.End();
        GL.PopMatrix();
    }

    void BarTop()
    {

        GL.PushMatrix();
        material.SetPass(0);
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
        material.SetPass(0);
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
        material.SetPass(0);
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
        material.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);

        GL.Vertex3(sb.x, sb.y * (-1), 0);
        GL.Vertex3(sb.x, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y * (-1), 0);

        GL.End();
        GL.PopMatrix();
    }



    public class Snake
    {
        private List<Block> blocks = new List<Block>();
        private Block randomBlock = new Block(0.0f, 0.0f, 'C');
        public int score = 0;

        public Snake()
        {
            blocks.Add(new Block(0, 0, 'D'));
            addBlock();
            addBlock();
            addBlock();
            createRandomBlock();
        }

        public Snake(float x, float y)
        {
            blocks.Add(new Block(x, y, 'D'));
            addBlock();
            addBlock();
            addBlock();
            createRandomBlock();
        }

        public void update(char direction)
        {

            float nextX = 0;
            float nextY = 0;

            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                if (i == 0)
                {
                    nextX = blocks[i].posicaoX;
                    nextY = blocks[i].posicaoY;

                    if (direction == 'D') blocks[i].posicaoX = blocks[i].posicaoX + 1.0f;
                    else if (direction == 'E') blocks[i].posicaoX = blocks[i].posicaoX - 1.0f;
                    else if (direction == 'C') blocks[i].posicaoY = blocks[i].posicaoY + 1.0f;
                    else blocks[i].posicaoY = blocks[i].posicaoY - 1.0f;
                }
                else
                {
                    float x = blocks[i].posicaoX;
                    float y = blocks[i].posicaoY;

                    blocks[i].posicaoX = nextX;
                    blocks[i].posicaoY = nextY;

                    nextX = x;
                    nextY = y;
                }
            }

            this.checkBlock();
        }

        public void checkBlock()
        {
            if (blocks[0].posicaoX == randomBlock.posicaoX && blocks[0].posicaoY == randomBlock.posicaoY)
            {
                this.addBlock();
                this.createRandomBlock();
                this.score++;
            }
        }

        public bool checkCrash()
        {
            for (int i = 1; i <= blocks.Count - 1; i++)
            {
                if (blocks[0].posicaoX == blocks[i].posicaoX && blocks[0].posicaoY == blocks[i].posicaoY) return false;
            }

            return true;
        }

        public void drawBox()
        {
            bool first = true;
            foreach (Block block in blocks)
            {
                float x = block.posicaoX;
                float y = block.posicaoY;

                if (first) GL.Color(Color.red);
                else GL.Color(Color.green);

                GL.Vertex3(x, y, 0);
                GL.Vertex3(x, y + 1, 0);
                GL.Vertex3(x + 1, y + 1, 0);
                GL.Vertex3(x + 1, y, 0);
                first = false;
            }

            GL.Color(Color.blue);
            GL.Vertex3(randomBlock.posicaoX, randomBlock.posicaoY, 0);
            GL.Vertex3(randomBlock.posicaoX, randomBlock.posicaoY + 1, 0);
            GL.Vertex3(randomBlock.posicaoX + 1, randomBlock.posicaoY + 1, 0);
            GL.Vertex3(randomBlock.posicaoX + 1, randomBlock.posicaoY, 0);
        }

        public void createRandomBlock()
        {
            System.Random rnd = new System.Random();

            bool valid = true;

            do
            {
                valid = true;

                randomBlock.posicaoX = rnd.Next(-12, 12);
                randomBlock.posicaoY = rnd.Next(-5, 5);

                for (int i = 0; i <= blocks.Count - 1; i++)
                {
                    if (blocks[i].posicaoX == randomBlock.posicaoX && blocks[i].posicaoY == randomBlock.posicaoY) valid = false;
                }

            } while (valid == false);

        }

        public void addBlock()
        {
            Block block = blocks[blocks.Count - 1];

            if (block.direcao == 'D') blocks.Add(new Block(block.posicaoX - 1.0f, block.posicaoY, 'D'));
            else if (block.direcao == 'E') blocks.Add(new Block(block.posicaoX + 1.0f, block.posicaoY, 'E'));
            else if (block.direcao == 'C') blocks.Add(new Block(block.posicaoX, block.posicaoY - 1.0f, 'C'));
            else blocks.Add(new Block(block.posicaoX, block.posicaoY + 1.0f, 'B'));

        }

    }

    class Block
    {
        public float posicaoX { get; set; }
        public float posicaoY { get; set; }
        public char direcao { get; set; }

        public Block(float posicaoX, float posicaoY, char direcao)
        {
            this.posicaoX = posicaoX;
            this.posicaoY = posicaoY;
            this.direcao = direcao;
        }
    }


}
