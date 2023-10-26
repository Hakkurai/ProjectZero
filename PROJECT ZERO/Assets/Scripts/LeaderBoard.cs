using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public InputField foodInput;
    public InputField mealInput;
    public Text foodList;

    private string dbName = "URI=file:Leaderboard.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();

        DisplayLeaderboard();

    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Leaderboard (Name VARCHAR(30), Score INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddPlayer()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Leaderboard (Name, Score) VALUES ('" + foodInput.text + "', '" + mealInput.text + "');";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        foodList.text = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Leaderboard ORDER BY Score DESC"; 

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string score = reader["Score"].ToString();
                        int nameWidth = 30; 
                        int paddingWidth = nameWidth - name.Length;
                        string padding = new string(' ', Math.Max(0, paddingWidth));
                        string formattedLine = $"{name}{padding}\t{score}";
                        foodList.text += formattedLine + "\n";
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }
    }

}
