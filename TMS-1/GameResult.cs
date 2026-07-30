namespace TMS_1;

public class GameResult
{
    public Move PlayerMove { get; }

    public Move ComputerMove { get; }

    public string Text { get; }


    public GameResult(Move playerMove, Move computerMove, string text)
    {
        PlayerMove = playerMove;
        ComputerMove = computerMove;
        Text = text;
    }


    public void Print()
    {
        Console.WriteLine($"You chose {PlayerMove.Name}");
        Console.WriteLine($"Computer chose {ComputerMove.Name}");
        Console.WriteLine(Text);
    }
}