
namespace Program;

public struct RaylibText
{
    public string Text;
    public float TextSize;
    public float TextSpacing;

    public RaylibText(string text, float textSize, float textSpacing)
    {
        Text = text;
        TextSize = textSize;
        TextSpacing = textSpacing;
    }
}