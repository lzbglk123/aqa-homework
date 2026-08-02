namespace Battleship;

class Game
{
    private readonly GameSettings settings;

    public List<Shot> Shots { get; } = new();


    public Game(GameSettings settings)
    {
        this.settings = settings;
    }


    public void Play(IPlayer player, IPlayer computer, Board playerBoard)
    {
        Board computerBoard = GenerateOpponentBoard(
            settings.BoardRows,
            settings.BoardColumns,
            playerBoard.Ships.Length);


        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Player turn");


            try
            {
                var playerShot = player.Shoot(computerBoard);

                if (IsAlreadyShot(playerShot))
                {
                    Console.WriteLine("You already shot at this position!");
                }
                else
                {
                    Shots.Add(playerShot);

                    if (playerShot.Ship != null)
                    {
                        playerShot.Ship.Hits.Add(playerShot);
                        Console.WriteLine("Player hit!");
                    }
                    else
                    {
                        Console.WriteLine("Player miss!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Turn skipped!");
                continue;
            }


            Console.WriteLine();
            Console.WriteLine("Computer turn");


            try
            {
                var computerShot = computer.Shoot(playerBoard);


                if (!IsAlreadyShot(computerShot))
                {
                    Shots.Add(computerShot);


                    Console.WriteLine(
                        $"Computer shot X:{computerShot.Position.X} Y:{computerShot.Position.Y}");


                    if (computerShot.Ship != null)
                    {
                        computerShot.Ship.Hits.Add(computerShot);
                        Console.WriteLine("Computer hit!");
                    }
                    else
                    {
                        Console.WriteLine("Computer miss!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine();

            PrintBoard(playerBoard, true);
            PrintBoard(computerBoard, false);


            var playerSunk = playerBoard.Ships
                .Count(ship => ship.IsSunk);

            var computerSunk = computerBoard.Ships
                .Count(ship => ship.IsSunk);


            Console.WriteLine(
                $"Player sunk ships: {playerSunk}");

            Console.WriteLine(
                $"Computer sunk ships: {computerSunk}");


            if (playerSunk == playerBoard.Ships.Length)
            {
                Console.WriteLine("Computer wins!");
                break;
            }


            if (computerSunk == computerBoard.Ships.Length)
            {
                Console.WriteLine("Player wins!");
                break;
            }
        }
    }



    private bool IsAlreadyShot(Shot shot)
    {
        return Shots.Any(existing =>
            existing.Board == shot.Board &&
            existing.Position.X == shot.Position.X &&
            existing.Position.Y == shot.Position.Y);
    }



    private Board GenerateOpponentBoard(
        int rows,
        int columns,
        int shipCount)
    {
        var random = new Random();

        var ships = new List<Ship>();


        while (ships.Count < shipCount)
        {
            int length = random.Next(1, 4);

            bool horizontal =
                random.Next(0, 2) == 0;


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


            Ship newShip = horizontal
                ? new HorizontalShip(position, length)
                : new VerticalShip(position, length);



            bool intersects = ships.Any(ship =>
                ship.IsIntersecting(newShip));


            if (!intersects)
            {
                ships.Add(newShip);
            }
        }


        return new Board(
            rows,
            columns,
            ships.ToArray());
    }



    private void PrintBoard(Board board, bool showShips)
    {
        Console.WriteLine();
        Console.WriteLine("Board:");

        for (int y = 0; y < board.Rows; y++)
        {
            for (int x = 0; x < board.Columns; x++)
            {
                var position = new Position(x, y);


                var shot = Shots.FirstOrDefault(s =>
                    s.Board == board &&
                    s.Position.X == x &&
                    s.Position.Y == y);



                if (shot != null)
                {
                    Console.Write(
                        shot.Ship != null ? "X " : "O ");
                }
                else if (showShips &&
                         board.FindShip(position) != null)
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
}