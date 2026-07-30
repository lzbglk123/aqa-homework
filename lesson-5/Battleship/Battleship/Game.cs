namespace Battleship;

class Game
{
    public List<Shot> Shots { get; } = new();

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


            try
            {
                ShootResult playerResult;

                try
                {
                    playerResult = player.Shoot(computerBoard);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                if (playerResult == ShootResult.InvalidShot)
                {
                    Console.WriteLine("Turn skipped!");
                    continue;
                }


                if (playerResult != ShootResult.InvalidShot)
                {
                    var playerShot = MakeShot(
                        computerBoard,
                        player.LastShot);


                    if (playerShot.Ship != null)
                    {
                        PlayerHits++;
                        Console.WriteLine("Player hit!");
                    }
                    else
                    {
                        Console.WriteLine("Player miss!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid shot!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine();


            Console.WriteLine("Computer turn");


            try
            {
                var computerResult = computer.Shoot(board);


                var computerShot = MakeShot(
                    board,
                    computer.LastShot);


                Console.WriteLine(
                    $"Computer shot X:{computer.LastShot.X} Y:{computer.LastShot.Y}");


                if (computerShot.Ship != null)
                {
                    ComputerHits++;
                    Console.WriteLine("Computer hit!");
                }
                else
                {
                    Console.WriteLine("Computer miss!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine();

            Console.WriteLine(
                $"Score: Player {PlayerHits} : {ComputerHits} Computer");
            if (PlayerHits >= 3)
            {
                Console.WriteLine("Player wins!");
                break;
            }

            if (ComputerHits >= 3)
            {
                Console.WriteLine("Computer wins!");
                break;
            }


            PrintStatistics(computerBoard);
            PrintStatistics(board);
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


    private Shot MakeShot(Board board, Position position)
    {
        if (Shots.Any(shot =>
                shot.Board == board &&
                shot.Position.X == position.X &&
                shot.Position.Y == position.Y))
        {
            throw new Exception("You already shot at this position!");
        }


        var ship = board.FindShip(position);


        var shot = new Shot(
            board,
            position,
            ship);


        Shots.Add(shot);


        return shot;
    }


    private void PrintStatistics(Board board)
    {
        var boardShots = Shots
            .Where(shot => shot.Board == board)
            .ToList();


        Console.WriteLine();

        Console.WriteLine("Statistics:");

        Console.WriteLine(
            $"Shots: {boardShots.Count}");

        Console.WriteLine(
            $"Hits: {boardShots.Count(shot => shot.Ship != null)}");

        Console.WriteLine(
            $"Misses: {boardShots.Count(shot => shot.Ship == null)}");


        Console.WriteLine(
            $"Has miss: {boardShots.Any(shot => shot.Ship == null)}");


        var firstHit = boardShots
            .FirstOrDefault(shot => shot.Ship != null);


        if (firstHit != null)
        {
            Console.WriteLine(
                $"First hit: X:{firstHit.Position.X} Y:{firstHit.Position.Y}");
        }


        var hitsCoordinates = boardShots
            .Where(shot => shot.Ship != null)
            .Select(shot =>
                $"({shot.Position.X},{shot.Position.Y})");


        Console.WriteLine(
            "Hit positions: " + string.Join(", ", hitsCoordinates));
    }
}