namespace Battleship;

class ComputerPlayer : IPlayer
{
    public Position LastShot { get; private set; }

    private readonly List<Position> shots = new();


    public ShootResult Shoot(Board targetBoard)
    {
        var random = new Random();

        Position shotPosition;


        do
        {
            shotPosition = targetBoard.GeneratePosition(random);

        } while (shots.Any(p =>
                     p.X == shotPosition.X &&
                     p.Y == shotPosition.Y));


        LastShot = shotPosition;

        shots.Add(LastShot);


        return targetBoard.HasShip(LastShot)
            ? ShootResult.Hit
            : ShootResult.Miss;
    }


    public void WriteName()
    {
        Console.WriteLine("Robot");
    }
}