using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "level1.txt";
            int lvl = 1;
            Console.CursorVisible = false;
            char[,] map = ReadMap(path);
            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;
            ConsoleKeyInfo presedKey = new ConsoleKeyInfo();
            Task.Run(() =>
            {
                while (true)
                {
                    presedKey = Console.ReadKey();
                }
            });
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                DrawMap(map);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write('@');
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(44, 0);
                Console.Write($"Score:{score}");
                Thread.Sleep(60);
                //ConsoleKeyInfo presedKey = Console.ReadKey();
                HandleInput(presedKey, ref pacmanX, ref pacmanY,ref map, ref score, ref path, ref lvl);


            }
        }
        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY,ref char [,] map, ref int score, ref string path , ref int lvl)
        {
            #region old
            //if(pressedKey.Key == ConsoleKey.UpArrow)
            //{
            //    if(map[pacmanX,pacmanY -1] != '#')
            //        pacmanY -= 1;
            //}
            //else if (pressedKey.Key == ConsoleKey.DownArrow)
            //{
            //    if (map[pacmanX, pacmanY + 1] != '#')
            //        pacmanY += 1;
            //}
            //else if (pressedKey.Key == ConsoleKey.RightArrow)
            //{
            //    if (map[pacmanX + 1, pacmanY] != '#')
            //        pacmanX += 1;
            //}
            //else if (pressedKey.Key == ConsoleKey.LeftArrow)
            //{
            //    if (map[pacmanX - 1, pacmanY] != '#')
            //        pacmanX -= 1;
            //}
            #endregion
            int[] direction = GetDirection(pressedKey);
            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];
            if (nextPacmanPositionX > 41 || nextPacmanPositionY > 12)
            {
                path = $"level{lvl + 1}.txt";
                lvl++;
                pacmanX = 1;
                pacmanY = 1;
                map = ReadMap(path);
            }
            else
            {
                char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];
                if (nextCell != '#')
                {
                    pacmanX = nextPacmanPositionX;
                    pacmanY = nextPacmanPositionY;
                    if (nextCell == '.')
                    {
                        score++;
                        map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                    }

                }
            }

        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };
            if (pressedKey.Key == ConsoleKey.UpArrow)
                    direction[1] = -1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                direction[0] = 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            return direction;
        }
        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];
            return map;
        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;
            foreach(var line in lines)
                if(line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;
        }
        private static void NextLevel(ref string path)
        {

        }
    }
}
