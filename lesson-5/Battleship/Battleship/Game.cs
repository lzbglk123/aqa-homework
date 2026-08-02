namespace Battleship;

class Game
{
    public List<Shot> Shots { get; } = new();

    public int PlayerHits { get; private set; }

    public int ComputerHits { get; private set; }


    public void Play(PlayerType playerType, Board board)
    {
        IPlayer player = new HumanPlayer();
        IPlayer computer = new ComputerPlayer();


        Board computerBoard = GenerateOpponentBoard(
            board.Rows,
            board.Columns);


        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Player turn");


            try
            {
                Shot playerShot = player.Shoot(computerBoard);

                if (IsRepeatedShot(playerShot))
                {
                    throw new Exception("You already shot at this position!");
                }

                Shots.Add(playerShot);


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
                Shot computerShot = computer.Shoot(board);

                if (IsRepeatedShot(computerShot))
                {
                    throw new Exception("You already shot at this position!");
                }

                Shots.Add(computerShot);


                Console.WriteLine(
                    $"Computer shot X:{computerShot.Position.X} Y:{computerShot.Position.Y}");


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


            PrintStatistics(computerBoard);
            PrintStatistics(board);


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
        }
    }


    private bool IsRepeatedShot(Shot shot)
    {
        return Shots.Any(oldShot =>
            oldShot.Board == shot.Board &&
            oldShot.Position.X == shot.Position.X &&
            oldShot.Position.Y == shot.Position.Y);
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


        var hits = boardShots
            .Where(shot => shot.Ship != null)
            .Select(shot =>
                $"({shot.Position.X},{shot.Position.Y})");


        Console.WriteLine(
            "Hit positions: " + string.Join(", ", hits));
    }
}