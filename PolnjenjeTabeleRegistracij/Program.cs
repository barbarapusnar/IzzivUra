// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
Console.WriteLine("Hello, World!");
var p=CreateConnection();
static SqliteConnection CreateConnection()
{
    SqliteConnection sqlite_conn;
    // Create a new database connection:
    sqlite_conn = new SqliteConnection("DataSource=D:\\Challenger\\IzzivUra\\OUporabnikih\\Podatki.db;");
    // Open the connection:
    try
    {
        sqlite_conn.Open();
        Console.WriteLine("Connection worked!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Connection didn't work!");
        Console.WriteLine(ex.Message);
    }
    return sqlite_conn;
}