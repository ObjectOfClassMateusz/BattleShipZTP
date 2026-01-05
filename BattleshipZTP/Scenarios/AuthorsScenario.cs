using BattleshipZTP.GameAssets;
using BattleshipZTP.UI;
using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios;

public class AuthorsScenario : Scenario
{
    private List<string> _authors = new List<string> {"Mateusz Tręda", "Oliwia Sieradzka"};
    
    public AuthorsScenario() : base() 
    { 
    }

    public override void Act()
    {
        base.Act();

        IWindowBuilder builder = new WindowBuilder();
        UIDirector director = new UIDirector(builder);

        director.AuthorsInit();
        
        builder.AddComponent(new TextOutput("Authors of the game: ©️"));
        builder.AddComponent(new TextOutput($"- {_authors[0]}"));
        builder.AddComponent(new TextOutput($"- {_authors[1]}"));
        builder.AddComponent(new Button("Return to Main Menu"));
        
        Window authorsWindow = builder.Build();
        builder.ResetBuilder();
        UIController controller = new UIController();
        controller.AddWindow(authorsWindow);

        List<string> option = controller.DrawAndStart();

        option[0] = "Main";
        _scenarios[option.LastOrDefault()].Act();
    }
}

