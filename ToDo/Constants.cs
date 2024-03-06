namespace ToDo
{
    public static class Constants
    {
        public const string DatabaseFilename = "TodoDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            //�ppnar databasen i l�s och skriv l�ge
            SQLite.SQLiteOpenFlags.ReadWrite |
            //Skapa databasen om den inte finns
            SQLite.SQLiteOpenFlags.Create |
            //M�jligg�r tillg�ng till databasen fr�n flera processer
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

}