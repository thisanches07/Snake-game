using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GL_Draw : MonoBehaviour
{
    public Material material;
    public int numBlock = 2;
    public float velocidade = 0.1f;
    private float xAux = 0;
    private float yAux = 0;
    private char direcao = 'D';
    private bool free = false;
    private bool play = true;
    protected Snake snake = new Snake();


    // Start is called before the first frame update
    void Start()
    {
        snake = new Snake();
    }

    // Update is called once per frame
    void Update()
    {

        // Logica para setar a posição
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

        // Logica Atualiza a direção dos blocos
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

        // recomeçar
        if (Input.GetKey(KeyCode.Escape))
        {
            direcao = 'D';
            snake = new Snake(-10.0f, 0.0f);
            play = true;
        }

    }

    private void OnPostRender()
    {
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



    public class Snake
    {
        private List<Block> blocks = new List<Block>();
        private Block randomBlock = new Block(0.0f, 0.0f, 'C');

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
