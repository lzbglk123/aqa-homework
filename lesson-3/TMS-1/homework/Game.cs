namespace TMS_1;

class Game
{
    private int _roundsPlayed = 0;

    private readonly Player _player;
    private readonly Player _computer;

    public int RoundsToPlay { get; }

    public Game(Player player, Player computer, int roundsToPlay)
    {
        _player = player;
        _computer = computer;
        RoundsToPlay = roundsToPlay;
    }

    public void Play()
    {
        Console.WriteLine("Hello this is Rock Paper Scissors");

        while (_roundsPlayed < RoundsToPlay)
        {
            Move playerMove = Move.ReadFromConsole();

            if (!Move.IsValid(playerMove.Number))
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            Move computerMove = Move.GenerateRandom();

            _roundsPlayed++;

            GameResult result = CalculateResult(playerMove, computerMove);

            result.Print(_roundsPlayed);

            Console.WriteLine($"Score: {_player.Score} : {_computer.Score}");
            Console.WriteLine();
        }

        Console.WriteLine("Game over!");
        Console.WriteLine($"Final score: {_player.Score} : {_computer.Score}");

        if (_player.Score > _computer.Score)
        {
            Console.WriteLine($"{_player.Name} wins!");
        }
        else if (_computer.Score > _player.Score)
        {
            Console.WriteLine($"{_computer.Name} wins!");
        }
        else
        {
            Console.WriteLine("Draw");
        }
    }

    private GameResult CalculateResult(Move playerMove, Move computerMove)
    {
        if (playerMove.Number == computerMove.Number)
        {
            return new GameResult(
                playerMove,
                computerMove,
                "It's a draw");
        }

        if (
            (playerMove.Number == 1 && computerMove.Number == 3) ||
            (playerMove.Number == 2 && computerMove.Number == 1) ||
            (playerMove.Number == 3 && computerMove.Number == 2) ||
            (playerMove.Number == 2 && computerMove.Number == 4) ||
            (playerMove.Number == 4 && computerMove.Number == 1) ||
            (playerMove.Number == 4 && computerMove.Number == 3)
        )
        {
            _player.AddPoint();

            return new GameResult(
                playerMove,
                computerMove,
                $"{_player.Name} won the round");
        }

        _computer.AddPoint();

        return new GameResult(
            playerMove,
            computerMove,
            $"{_computer.Name} won the round");
    }
}