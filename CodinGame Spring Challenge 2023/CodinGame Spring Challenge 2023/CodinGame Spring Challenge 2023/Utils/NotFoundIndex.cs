namespace CodinGame_Spring_Challenge_2023.Utils;

public record NotFoundIndex
{
    public int Index { get; }
    
    public NotFoundIndex(int index)
    {
        Index = index;
    }
}