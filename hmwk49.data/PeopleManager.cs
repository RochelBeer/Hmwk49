using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmwk49.data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleManager
    {
        public string _connectionString;
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetPeople()
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM PeopleTable";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Person> _people = new();
            while (reader.Read())
            {
                _people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return _people;
        }
        public void Add(Person person)
        {

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = $@"INSERT INTO PeopleTable(FirstName, LastName, Age)
VALUES(@firstName,@lastName,@age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();


        }
        public void AddMany(List<Person>people)
        {
            foreach(Person p in people)
            {
                Add(p);
            }
        }
    }
}
