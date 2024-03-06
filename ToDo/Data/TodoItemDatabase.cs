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

        /*Använder asynkron "lazy"-initialisering för att fördröja 
         * initialiseringen av databasen tills den först används, med en enkel
         * Init-metod som anropas av varje metod i klassen*/

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TodoItem>();
        }

        /*metoder för de fyra typerna av datamanipulation: skapa, läsa, redigera och radera.
         * SQLite.NET - biblioteket tillhandahåller en enkel object Relational Map (ORM)
         * som gör det möjligt att lagra och hämta objekt utan att skriva SQL-satser. Det är 
         * också möjligt att använda SQL-frågor
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

            //Med SQL-fråga:
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

