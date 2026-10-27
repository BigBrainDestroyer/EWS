
namespace Program;

public class InformationViewer
{
    public CenterTextInBox TitleBox { get; set; }

    public CenterTextInBox InformationBox { get; set; }

    public float TitleBoxVerticalPercentage { get; set; }

    public InformationViewer(CenterTextInBox titleBox, CenterTextInBox informationBox, float titleBoxVerticalPercentage)
    {
        TitleBox = titleBox;
        InformationBox = informationBox;
        TitleBoxVerticalPercentage = titleBoxVerticalPercentage;
    }
}