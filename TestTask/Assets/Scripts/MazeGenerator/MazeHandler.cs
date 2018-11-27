using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
     public class MazeHandler
    {
        private const int DefaultWidth = 60;
        private const int DefaultHeight = 40;

        public Maze maze { get; set; }
        private List<Cell> Processed = new List<Cell>();//Содержит клетки, которые ещё не были обработаны

        public MazeHandler() : this(DefaultHeight, DefaultWidth) { }
        public MazeHandler(int height, int width)
        {
            Random rnd = new Random();

            this.maze = new Maze
            {
                map = new int[height, width],
                width = width,
                height = height
            };

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        maze.map[i, j] = (int)CellType.Wall;
                    }
                    else
                    {
                        maze.map[i, j] = (int)CellType.Unknown;
                    }
                }
            }
        }

        public void StartGenerate()
        {
            Random rnd = new Random();
            maze.map[10, 10] = (int)CellType.Pass;
            Cell entryCell = new Cell(10, 11);

            Processed.Add(entryCell);

            while (Processed.Any())
            {
                int rand = rnd.Next(Processed.Count());
                Cell cell = Processed[rand];

                if (IsAvailable(cell))
                {
                    DoPass(cell);
                }
                else
                {
                    maze.map[cell.X, cell.Y] = (int)CellType.Wall;
                }
                Processed.RemoveAt(rand);
            }

            for (int i = 0; i < maze.height; i++)
            {
                for (int j = 0; j < maze.width; j++)
                {
                    if (maze.map[i, j] == (int)CellType.Unknown) maze.map[i, j] = (int)CellType.Wall;
                }
            }

            StartCorrectingDeadEnds();
            //ClearDeadEnds();
        }

        private int GetNumderNeighbors(int i, int j)
        {
            int counter = 0;

            if (maze.map[i + 1, j] == (int)CellType.Pass) counter++;
            if (maze.map[i, j - 1] == (int)CellType.Pass) counter++;
            if (maze.map[i, j + 1] == (int)CellType.Pass) counter++;
            if (maze.map[i - 1, j] == (int)CellType.Pass) counter++;
            return counter;

        }

        private bool IsAvailable(Cell cell)
        {
            int counter = 0;

            if (cell.X > 0)
            {
                if (maze.map[cell.X - 1, cell.Y] == (int)CellType.Pass)
                {
                    counter++;
                    if (cell.Y > 0)
                    {
                        if (maze.map[cell.X + 1, cell.Y - 1] == (int)CellType.Pass) counter++;
                    }

                    if (cell.Y < maze.width - 1)
                    {
                        if (maze.map[cell.X + 1, cell.Y + 1] == (int)CellType.Pass) counter++;
                    }
                }
            }

            if (cell.Y > 0)
            {
                if (maze.map[cell.X, cell.Y - 1] == (int)CellType.Pass)
                {
                    counter++;

                    if (cell.X > 0 &&
                        maze.map[cell.X - 1, cell.Y + 1] == (int)CellType.Pass)
                    {
                        counter++;
                    }
                    if (cell.X < maze.height - 1 &&
                        maze.map[cell.X + 1, cell.Y + 1] == (int)CellType.Pass)
                    {
                        counter++;
                    }

                }
            }

            if (cell.Y < maze.width - 1)
            {
                if (maze.map[cell.X, cell.Y + 1] == (int)CellType.Pass)
                {
                    counter++;

                    if (cell.X > 0 &&
                        maze.map[cell.X - 1, cell.Y - 1] == (int)CellType.Pass)
                    {
                        counter++;
                    }
                    if (cell.X < maze.height - 1 &&
                        maze.map[cell.X + 1, cell.Y - 1] == (int)CellType.Pass)
                    {
                        counter++;
                    }
                }
            }

            if (cell.X < maze.height - 1)
            {
                if (maze.map[cell.X + 1, cell.Y] == (int)CellType.Pass)
                {
                    counter++;

                    if (cell.X > 0 &&
                        maze.map[cell.X - 1, cell.Y - 1] == (int)CellType.Pass)
                    {
                        counter++;
                    }
                    if (cell.Y < maze.width - 1 &&
                        maze.map[cell.X - 1, cell.Y + 1] == ((int)CellType.Pass))
                    {
                        counter++;
                    }
                }
            }

            return counter == 1;

        }

        private void DoPass(Cell cell)//добавляет соседей клетке
        {
            maze.map[cell.X, cell.Y] = (int)CellType.Pass;
            Cell neighborCell;
            if (cell.X > 1)
            {
                if (maze.map[cell.X - 1, cell.Y] == (int)CellType.Unknown)
                {
                    neighborCell = new Cell(cell.X - 1, cell.Y);
                    Processed.Add(neighborCell);
                }
            }

            if (cell.X < maze.height - 2)
            {
                if (maze.map[cell.X + 1, cell.Y] == (int)CellType.Unknown)
                {
                    neighborCell = new Cell(cell.X + 1, cell.Y);
                    Processed.Add(neighborCell);
                }
            }

            if (cell.Y > 1)
            {
                if (maze.map[cell.X, cell.Y - 1] == (int)CellType.Unknown)
                {
                    neighborCell = new Cell(cell.X, cell.Y - 1);
                    Processed.Add(neighborCell);
                }
            }

            if (cell.Y < maze.width - 2)
            {
                if (maze.map[cell.X, cell.Y + 1] == (int)CellType.Unknown)
                {
                    neighborCell = new Cell(cell.X, cell.Y + 1);
                    Processed.Add(neighborCell);
                }
            }
        }

        private void StartCorrectingDeadEnds()
        {
            Random rnd = new Random();

            for (int i = 1; i < maze.height - 2; i++)
            {
                for (int j = 1; j < maze.width - 2; j++)
                {
                    if (maze.map[i, j] == (int)CellType.Pass)
                    {
                        TryCorrectDeadEnds(i, j);
                    }
                }
            }
        }

        private void ClearDeadEnds()//заделывает тупики
        {
            for (int i = 1; i < maze.height - 1; i++)
            {
                for (int j = 1; j < maze.width - 1; j++)
                {
                    if (maze.map[i, j] != (int)CellType.Pass) continue;
                    if (GetNumderNeighbors(i, j) < 2)
                    {
                        maze.map[i, j] = (int)CellType.Wall;
                        ClearDeadEnds();
                    }
                }
            }
        }

        private void TryCorrectDeadEnds(int i, int j)//функция уменьшает количество тупиков, освобождая клетки
        {
            if (GetNumderNeighbors(i,j) < 2)
            {
                if (i > 1 && i < maze.height - 2 && j < maze.width - 2 &&
                    maze.map[i + 1, j + 1] != (int)CellType.Pass &&
                    maze.map[i - 1, j + 1] != (int)CellType.Pass)
                    {
                        maze.map[i, j + 1] = (int)CellType.Pass; TryCorrectDeadEnds(i, j + 1); 
                    }

                else if (i > 1 && i < maze.height - 2 && j > 1 &&
                    maze.map[i + 1, j - 1] != (int)CellType.Pass &&
                    maze.map[i - 1, j - 1] != (int)CellType.Pass)
                    {
                        maze.map[i, j - 1] = (int)CellType.Pass; TryCorrectDeadEnds(i, j - 1);
                    }

                else if (j > 1 && j < maze.height - 2 && i < maze.width - 2 &&
                    maze.map[i + 1, j - 1] != (int)CellType.Pass &&
                    maze.map[i + 1, j + 1] != (int)CellType.Pass)
                    {
                        maze.map[i + 1, j] = (int)CellType.Pass; TryCorrectDeadEnds(i + 1, j);
                    }

                else if (j > 1 && j < maze.height - 2 && i > 1 &&
                    maze.map[i - 1, j + 1] != (int)CellType.Pass &&
                    maze.map[i - 1, j - 1] != (int)CellType.Pass)
                    {
                        maze.map[i - 1, j] = (int)CellType.Pass; TryCorrectDeadEnds(i - 1, j);
                    }
            }
        }
    }
}
