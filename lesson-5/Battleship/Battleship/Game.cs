namespace Battleship;

class Game
{
    public List<Shot> Shots { get; } = new();

    private readonly GameSettings settings;

    public Game(GameSettings settings)
    {
        this.settings = settings;
    }


    public void Play(IPlayer player, IPlayer computer, Board playerBoard)
    {
        Board computerBoard = GenerateOpponentBoard(
            playerBoard.Rows,
            playerBoard.Columns);


        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Player turn");


            Shot playerShot;

            try
            {
                playerShot = player.Shoot(computerBoard);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }


            Shots.Add(playerShot);


            if (playerShot.Ship != null)
            {
                Console.WriteLine("Player hit!");
            }
            else
            {
                Console.WriteLine("Player miss!");
            }


            Console.WriteLine();


            Console.WriteLine("Computer turn");


            Shot computerShot;

            try
            {
                computerShot = computer.Shoot(playerBoard);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }


            Shots.Add(computerShot);


            Console.WriteLine(
                $"Computer shot X:{computerShot.Position.X} Y:{computerShot.Position.Y}");


            if (computerShot.Ship != null)
            {
                Console.WriteLine("Computer hit!");
            }
            else
            {
                Console.WriteLine("Computer miss!");
            }


            Console.WriteLine();

            PrintBoard(playerBoard, true);
            PrintBoard(computerBoard, false);


            Console.WriteLine(
                $"Player sunk ships: {CountSunkShips(playerBoard)}");

            Console.WriteLine(
                $"Computer sunk ships: {CountSunkShips(computerBoard)}");


            if (CountSunkShips(computerBoard) == computerBoard.Ships.Length)
            {
                Console.WriteLine("Player wins!");
                break;
            }


            if (CountSunkShips(playerBoard) == playerBoard.Ships.Length)
            {
                Console.WriteLine("Computer wins!");
                break;
            }
        }
    }



    private Board GenerateOpponentBoard(int rows, int columns)
    {
        var random = new Random();

        var ships = new List<Ship>();


        while (ships.Count < settings.BoardRows)
        {
            int length = random.Next(1, 4);

            bool horizontal = random.Next(2) == 0;


            Position position;


            if (horizontal)
            {
                position = new Position(
                    random.Next(columns - length + 1),
                    random.Next(rows));
            }
            else
            {
                position = new Position(
                    random.Next(columns),
                    random.Next(rows - length + 1));
            }


            Ship ship = horizontal
                ? new HorizontalShip(position, length)
                : new VerticalShip(position, length);



            if (ships.Any(existing =>
                    existing.IsIntersecting(ship)))
            {
                continue;
            }


            ships.Add(ship);
        }


        return new Board(
            rows,
            columns,
            ships.ToArray());
    }



    private void PrintBoard(Board board, bool showShips)
    {
        Console.WriteLine();

        for (int y = 0; y < board.Rows; y++)
        {
            for (int x = 0; x < board.Columns; x++)
            {
                var position = new Position(x, y);


                var shot = Shots.FirstOrDefault(
                    s =>
                    s.Board == board &&
                    s.Position.X == x &&
                    s.Position.Y == y);



                if (shot != null)
                {
                    Console.Write(
                        shot.Ship != null ? "X " : "O ");

                    continue;
                }


                if (showShips && board.HasShip(position))
                {
                    Console.Write("S ");
                }
                else
                {
                    Console.Write(". ");
                }
            }

            Console.WriteLine();
        }
    }



    private int CountSunkShips(Board board)
    {
        return board.Ships.Count(
            ship => ship.IsSunk);
    }
}
