namespace CommuniLend.Data.Configurations.StaticSeedData
{
    internal static class SeedUserIds
    {

        private static readonly string[] Ids =
        [
            "fec24483-730b-58d9-9383-2c44fcf490e8", // 1 John Smith
            "5085596c-7975-580a-aa8a-68ad83d435bb", // 2 Sarah Johnson
            "b9af376a-21a5-540b-89d2-8c131633400d", // 3 Michael Chen
            "93295a13-2d4b-5732-9cba-916402a8edf3", // 4 Emily Rodriguez
            "f04c78b2-6180-5b82-bdb8-0eedb97675a7", // 5 David Williams
            "234a29ff-3432-5780-a6e8-3acd25056d4c", // 6 Jessica Martinez
            "5babe2ff-dff9-5359-8b04-f77b0028ca98", // 7 Robert Taylor
        ];

        public static string Get(int memberNumber) => Ids[memberNumber - 1];
    }



}
