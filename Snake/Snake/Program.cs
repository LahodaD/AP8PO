using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Program
    {
        enum Direction { UP, DOWN, LEFT, RIGHT };

        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            Random randomNumber = new Random();
            int score = 5;
            int gameOver = 0;
            Pixel head = new Pixel();
            head.XPos = screenWidth / 2;
            head.YPos = screenHeight / 2;
            head.ScreenColor = ConsoleColor.Red;
            Direction movement = Direction.RIGHT;
            List<int> xPosBody = new List<int>();
            List<int> yPosBody = new List<int>();
            int berryX = randomNumber.Next(0, screenWidth);
            int berryY = randomNumber.Next(0, screenHeight);
            DateTime time = DateTime.Now;
            DateTime time2 = DateTime.Now;
            bool buttonPressed = false;
            while (true)
            {
                Console.Clear();
                if (head.XPos == screenWidth - 1 || head.XPos == 0 || head.YPos == screenHeight - 1 || head.YPos == 0)
                {
                    gameOver = 1;
                }
                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, screenHeight - 1);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(screenWidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryX == head.XPos && berryY == head.YPos)
                {
                    score++;
                    berryX = randomNumber.Next(1, screenWidth - 2);
                    berryY = randomNumber.Next(1, screenHeight - 2);
                }
                for (int i = 0; i < xPosBody.Count; i++)
                {
                    Console.SetCursorPosition(xPosBody[i], yPosBody[i]);
                    Console.Write("■");
                    if (xPosBody[i] == head.XPos && yPosBody[i] == head.YPos)
                    {
                        gameOver = 1;
                    }
                }
                if (gameOver == 1)
                {
                    break;
                }
                Console.SetCursorPosition(head.XPos, head.YPos);
                Console.ForegroundColor = head.ScreenColor;
                Console.Write("■");
                Console.SetCursorPosition(berryX, berryY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                time = DateTime.Now;
                buttonPressed = false;
                while (true)
                {
                    time2 = DateTime.Now;
                    if (time2.Subtract(time).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        //Console.WriteLine(key.Key.ToString());
                        if (key.Key.Equals(ConsoleKey.UpArrow) && movement != Direction.DOWN && !buttonPressed)
                        {
                            movement = Direction.UP;
                            buttonPressed = true;
                        }
                        if (key.Key.Equals(ConsoleKey.DownArrow) && movement != Direction.UP && !buttonPressed)
                        {
                            movement = Direction.DOWN;
                            buttonPressed = true;
                        }
                        if (key.Key.Equals(ConsoleKey.LeftArrow) && movement != Direction.RIGHT && !buttonPressed)
                        {
                            movement = Direction.LEFT;
                            buttonPressed = true;
                        }
                        if (key.Key.Equals(ConsoleKey.RightArrow) && movement != Direction.LEFT && !buttonPressed)
                        {
                            movement = Direction.RIGHT;
                            buttonPressed = true;
                        }
                    }
                }
                xPosBody.Add(head.XPos);
                yPosBody.Add(head.YPos);
                switch (movement)
                {
                    case Direction.UP:
                        head.YPos--;
                        break;
                    case Direction.DOWN:
                        head.YPos++;
                        break;
                    case Direction.LEFT:
                        head.XPos--;
                        break;
                    case Direction.RIGHT:
                        head.XPos++;
                        break;
                }
                if (xPosBody.Count > score)
                {
                    xPosBody.RemoveAt(0);
                    yPosBody.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
        class Pixel
        {
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }
        }
    }
}