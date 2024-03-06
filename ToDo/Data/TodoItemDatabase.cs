using SQLite;
using ToDo.Models;

namespace ToDo.Data
{
    public class TodoItemDatabase
    {
        SQLiteAsyncConnection Database;
        public TodoItemDatabase()
        {

        }

        /*Anv�nder asynkron "lazy"-initialisering f�r att f�rdr�ja 
         * initialiseringen av databasen tills den f�rst anv�nds, med en enkel
         * Init-metod som anropas av varje metod i klassen*/

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TodoItem>();
        }

        /*metoder f�r de fyra typerna av datamanipulation: skapa, l�sa, redigera och radera.
         * SQLite.NET - biblioteket tillhandah�ller en enkel object Relational Map (ORM)
         * som g�r det m�jligt att lagra och h�mta objekt utan att skriva SQL-satser. Det �r 
         * ocks� m�jligt att anv�nda SQL-fr�gor
         */

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

            //Med SQL-fr�ga:
            //Return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<TodoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            await Init();
            if (item.Id != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

    }
}

