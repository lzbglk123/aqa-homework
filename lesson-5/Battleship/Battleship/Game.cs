namespace Battleship;

class Game
{
    public int PlayerHits { get; private set; }

    public int ComputerHits { get; private set; }


    public void Play(PlayerType playerType, Board board)
    {
        var player = new HumanPlayer();
        var computer = new ComputerPlayer();


        Board computerBoard = GenerateOpponentBoard(
            board.Rows,
            board.Columns);


        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Player turn");


            var playerResult = player.Shoot(computerBoard);


            switch (playerResult)
            {
                case ShootResult.Hit:
                    PlayerHits++;
                    Console.WriteLine("Player hit!");
                    break;

                case ShootResult.Miss:
                    Console.WriteLine("Player miss!");
                    break;

                case ShootResult.InvalidShot:
                    Console.WriteLine("Invalid shot!");
                    break;
            }


            Console.WriteLine();


            Console.WriteLine("Computer turn");


            var computerResult = computer.Shoot(board);


            Console.WriteLine(
                $"Computer shot X:{computer.LastShot.X} Y:{computer.LastShot.Y}");


            if (computerResult == ShootResult.Hit)
            {
                ComputerHits++;
                Console.WriteLine("Computer hit!");
            }
            else
            {
                Console.WriteLine("Computer miss!");
            }


            Console.WriteLine();

            Console.WriteLine(
                $"Score: Player {PlayerHits} : {ComputerHits} Computer");
        }
    }


    private Board GenerateOpponentBoard(int rows, int columns)
    {
        var random = new Random();


        int length = random.Next(1, 4);


        bool horizontal = random.Next(0, 2) == 0;


        Position position;


        if (horizontal)
        {
            position = new Position(
                random.Next(0, columns - length + 1),
                random.Next(0, rows));
        }
        else
        {
            position = new Position(
                random.Next(0, columns),
                random.Next(0, rows - length + 1));
        }


        Ship ship = horizontal
            ? new HorizontalShip(position, length)
            : new VerticalShip(position, length);


        return new Board(
            rows,
            columns,
            new[] { ship });
    }
}