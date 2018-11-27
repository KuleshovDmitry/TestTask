namespace MazeGenerator
{
    public enum CellType
    {
        Wall,
        Pass,
        Unknown
    }
    public class Maze
    {
        public int width { get; set; }
        public int height { get; set; }
        public int[,] map { get; set; }
    }
}
