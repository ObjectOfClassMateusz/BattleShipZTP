using BattleshipZTP.Utilities;

namespace BattleshipZTP.Scenarios
{
    /**
     * Scenario 
     * @brief design pattern for difference events
     * */
    public abstract class Scenario : IScenario
    {
        protected readonly Dictionary<string, IScenario> _scenarios;
        public Scenario() 
        { 
            _scenarios = new Dictionary<string, IScenario>();
        }
        /**
        * @brief Act - there and programmer write his method
        */
        public virtual void Act()
        {
            Console.Clear();
            Env.Wait(200);
            Env.SetColor();
        }
        /**
        * @brief Act - there and programmer write his method (Async version)
        */
        public virtual async Task AsyncAct()
        {
            Console.Clear();
            Env.Wait(200);
            Env.SetColor();
        }
        /**
         * @brief Connects a different scenario to create a transition beetween scenarios
         * @param key for autorize scenario, scenario object
         * @details adds an scenario to dictonary
         */
        public virtual void ConnectScenario(string key , IScenario scenario)
        {
            _scenarios[key] = scenario;
        }
    }
}
