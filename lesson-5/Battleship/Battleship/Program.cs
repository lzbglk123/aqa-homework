namespace Battleship;

class Program
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("Enter your player type (H for Human, C for Computer): ");
            
            var playerTypeInput = Console.ReadLine();

            PlayerType playerType;

            switch (playerTypeInput)
            {
                case "H":
                    playerType = PlayerType.Human;
                    break;

                case "C":
                    playerType = PlayerType.Computer;
                    break;

                default:
                    throw new ArgumentException("Invalid player type!");
            }


            var settings = new GameSettings(
                BoardRows: 5,
                BoardColumns: 5,
                PlayerType: playerType);


            var playerBoard = new Board(
                settings.BoardRows,
                settings.BoardColumns,
                new Ship[]
                {
                    new HorizontalShip(new Position(1, 1), 2),
                    new VerticalShip(new Position(2, 2), 2),
                    new VerticalShip(new Position(4, 0), 3)
                });


            IPlayer player = new HumanPlayer();
            IPlayer computer = new ComputerPlayer();


            var game = new Game(settings);

            game.Play(
                player,
                computer,
                playerBoard);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

}

record GameSettings(
    int BoardRows,
    int BoardColumns,
    PlayerType PlayerType);

enum PlayerType
{
    Human,
    Computer
}