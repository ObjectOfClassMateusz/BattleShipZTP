using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Ship.Turrets
{
    /**
     * @brief interface to create turrets.
     * @details  Turrets are weapons used by ships in 40k gamemode. It have own actions cost - how much action points do you need to launch fire from turret.
     * Deals area-body damage between mix-max range values.
     */
    public interface ITurret
    {
        /**
         * @return set of string that look like an area of turret attack
         */
        List<(string text, int offset)> GetAimBody();
        /**
         * @brief minimum damage will turret deal per point
         */
        int MinDmg();
        /**
         * @brief maximum damage will turret deal per point
         */
        int MaxDmg();
        /**
         * @brief name of sound that will be played
         */
        string AudioFileName();
        /**
         * @brief action delay
         */
        void AfterAudioDelay();
        /**
         * @brief name of turret
         */
        string GetName();
        /**
         * @brief how much action points cost launch a single fire
         */
        int ActionCost();
        /**
         * @brief use an turret and then is unready
         */
        void Use();
        /**
         * @brief renew an unready turret on the next round
         */
        void Renew();
        /**
         * @brief checks is turret ready to fire
         */
        bool IsReady();
    }
}
