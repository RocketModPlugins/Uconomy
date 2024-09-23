using Rocket.API;

namespace fr34kyn01535.Uconomy
{
    public class UconomyConfiguration : IRocketPluginConfiguration
    {
        public string Image { get; set; }
        public decimal RewardToKillPlayer { get; set; }
        public decimal RewardToKillZombie { get; set; }
        public decimal RewardToKillMegaZombie { get; set; }
        public decimal RewardToKillAnimal { get; set; }
        public decimal RewardToFish { get; set; }
        public decimal InitialBalance;
        public string MoneySymbol;
        public string MoneyName;

        public string MessageColor;

        public void LoadDefaults()
        {
            Image = "Url Here";
            RewardToKillPlayer = 60;
            RewardToKillZombie = 15;
            RewardToKillAnimal = 100;
            RewardToKillMegaZombie = 500;
            RewardToFish = 30;
            InitialBalance = 30;
            MoneySymbol = "$";
            MoneyName = "Credits";
            MessageColor = "blue";
        }
    }
}
