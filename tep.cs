namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var counts = new double[30, 12];

            foreach (var name in names)
            {
                int day = name.BirthDate.Day;
                int month = name.BirthDate.Month - 1;

                if (day >= 2)
                {
                    day = day - 2;

                    if (day >= 0 && day < 30) 
                    {
                        counts[day, month]++;
                    }
                }
            }

            var x = new string[30];
            for (int i = 0; i <= 29; i++)
            {
                x[i] = (i + 2).ToString(); 
            }

            var y = new string[12];
            for (int j = 0; j <= 11; j++)
            {
                y[j] = (j + 1).ToString(); 
            }

            return new HeatmapData(
                "Распределение рождений",
                counts,
                x,
                y);
        }
	} 
}
