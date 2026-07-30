namespace TMS_1;

class Game
{
    public Game(int roundsToPlay)
    {
        RoundsToPlay = roundsToPlay;
    }

    private int _roundsPlayed = 0;

    private Player _player = new Player("Player");
    private Player _computer = new Player("Computer");


    public int RoundsToPlay { get; private set; }

    public bool UserWon { get; private set; }


    public void Play()
    {
        Console.WriteLine("Hello this is Rock Paper Scissors");


        int rounds;

        do
        {
            Console.WriteLine("Enter number of rounds:");
        }
        while (!int.TryParse(Console.ReadLine(), out rounds) || rounds <= 0);


        RoundsToPlay = rounds;

        UserWon = false;


        do
        {
            Console.WriteLine();
            Console.WriteLine($"Round {_roundsPlayed + 1}/{RoundsToPlay}");


            Move playerMove = Move.ReadFromConsole();

            if (playerMove.Number == 0)
            {
                return;
            }


            _roundsPlayed++;


            Move computerMove = Move.GenerateRandom();


            GameResult result = CalculateResult(playerMove, computerMove);

            result.Print();


            if (result.Text == "Player won the round")
            {
                _player.AddPoint();
            }
            else if (result.Text == "Computer won the round")
            {
                _computer.AddPoint();
            }


            Console.WriteLine($"Score: {_player.Score} : {_computer.Score}");


            int remainingRounds = RoundsToPlay - _roundsPlayed;


            if (_player.Score > _computer.Score + remainingRounds)
            {
                Console.WriteLine("Player wins the game!");
                return;
            }


            if (_computer.Score > _player.Score + remainingRounds)
            {
                Console.WriteLine("Computer wins the game!");
                return;
            }


        } while (_roundsPlayed < RoundsToPlay);


        Console.WriteLine();


        if (_player.Score > _computer.Score)
        {
            Console.WriteLine("Player wins the game!");
        }
        else if (_computer.Score > _player.Score)
        {
            Console.WriteLine("Computer wins the game!");
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
            return new GameResult(
                playerMove,
                computerMove,
                "Player won the round");
        }


        return new GameResult(
            playerMove,
            computerMove,
            "Computer won the round");
    }
}