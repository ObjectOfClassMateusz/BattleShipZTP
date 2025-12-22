using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    public abstract class Scenario : IScenario
    {
        protected readonly Dictionary<string, IScenario> _scenarios;
        public Scenario() 
        { 
            _scenarios = new Dictionary<string, IScenario>();
        }
        public virtual void Act()
        {
            Console.Clear();
            Env.Wait(200);
            Env.SetColor();
        }
    }
}
