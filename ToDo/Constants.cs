namespace ToDo
{
    public static class Constants
    {
        public const string DatabaseFilename = "TodoDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            //Öppnar databasen i läs och skriv läge
            SQLite.SQLiteOpenFlags.ReadWrite |
            //Skapa databasen om den inte finns
            SQLite.SQLiteOpenFlags.Create |
            //Möjliggör tillgång till databasen från flera processer
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

}