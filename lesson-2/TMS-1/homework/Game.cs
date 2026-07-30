namespace TMS_1;

class Game
{
    
    public Game(int roundsToPlay)
    {
        RoundsToPlay = roundsToPlay;
    }
    
    private int _roundsPlayed = 0;
    private int _playerScore = 0;
    private int _computerScore = 0;

    public int RoundsToPlay { get; private set; }

    public bool UserWon { get; private set; }

    public string DisplayRoundsPlayed(bool someParam, int roundsPlayed = 2, int someAdditionalParam = 3) =>
        $"Game has {roundsPlayed} rounds played";

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

        Console.WriteLine("Enter your step");

        UserWon = false;

        do
        {
            Console.WriteLine();
            Console.WriteLine($"Round {_roundsPlayed + 1}/{RoundsToPlay}");

            Console.WriteLine("1 - Rock");
            Console.WriteLine("2 - Paper");
            Console.WriteLine("3 - Scissors");
            Console.WriteLine("4 - Well");
            Console.WriteLine("0 - Exit");

            var userInput = Console.ReadLine();

            int userChoice;

            if (!int.TryParse(userInput, out userChoice) || !(userChoice >= 0 && userChoice <= 4))
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            if (userChoice == 0)
            {
                return;
            }

            _roundsPlayed++;

            string userChoiceString = CalculateMoveName(userChoice);
            Console.WriteLine($"You chose {userChoiceString}");

            var random = new Random();
            var computerChoice = random.Next(1, 5);

            string computerChoiceString = CalculateMoveName(computerChoice);

            Console.WriteLine($"Computer chose {computerChoiceString}");

            var winnerExists = TryCalculateWinner(userChoice, computerChoice, out var winner);

            if (winnerExists)
            {
                Console.WriteLine($"{winner} won the round");

                if (winner == "Player 1")
                {
                    _playerScore++;
                }
                else
                {
                    _computerScore++;
                }
            }
            else
            {
                Console.WriteLine("It's a draw");
            }

            Console.WriteLine($"Score: Player {_playerScore} : {_computerScore} Computer");


            // Проверка досрочной победы
            int remainingRounds = RoundsToPlay - _roundsPlayed;

            if (_playerScore > _computerScore + remainingRounds)
            {
                Console.WriteLine("Player wins the game!");
                return;
            }

            if (_computerScore > _playerScore + remainingRounds)
            {
                Console.WriteLine("Computer wins the game!");
                return;
            }

        } while (_roundsPlayed < RoundsToPlay);


        Console.WriteLine();

        if (_playerScore > _computerScore)
        {
            Console.WriteLine("Player wins the game!");
        }
        else if (_computerScore > _playerScore)
        {
            Console.WriteLine("Computer wins the game!");
        }
        else
        {
            Console.WriteLine("Draw");
        }
    }


    private string CalculateMoveName(int moveNumber)
    {
        switch (moveNumber)
        {
            case 1:
                return "Rock";
            case 2:
                return "Paper";
            case 3:
                return "Scissors";
            case 4:
                return "Well";
            default:
                return "";
        }
    }


    private bool TryCalculateWinner(int player1Move, int player2Move, out string winner)
    {
        if (player1Move == player2Move)
        {
            winner = "";
            return false;
        }


        if (
            (player1Move == 1 && player2Move == 3) || // Rock beats Scissors
            (player1Move == 2 && player2Move == 1) || // Paper beats Rock
            (player1Move == 3 && player2Move == 2) || // Scissors beats Paper
            (player1Move == 2 && player2Move == 4) || // Paper beats Well
            (player1Move == 4 && player2Move == 1) || // Well beats Rock
            (player1Move == 4 && player2Move == 3)    // Well beats Scissors
        )
        {
            winner = "Player 1";
        }
        else
        {
            winner = "Player 2";
        }

        return true;
    }
}