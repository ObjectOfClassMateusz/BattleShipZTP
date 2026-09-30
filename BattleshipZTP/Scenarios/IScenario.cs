using BattleshipZTP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Scenarios
{
    /**Scenario Interface - design pattern for difference events
     * @brief This interface shares methods to define for unique purpose
     */
    public interface IScenario
    {
        void Act();
        Task AsyncAct();
        void ConnectScenario(string key, IScenario scenario);
    }
}
