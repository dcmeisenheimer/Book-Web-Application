using System;
using System.Collections.Generic;
using System.Data.SQLite;
using API.Models.Interfaces;

namespace API.Models
{
    public class ReadBookData : IGetAllBooks, IGetBook
    {
        public List<Book> GetAllBooks()
        {
            string cs = @"URI=file:C:\Source\FallRepos\WebBasedApplication\book.db";
            using var con = new SQLiteConnection(cs);

            con.Open();

            string stm = "SELECT * FROM books"; //selects all the fields from the table called books

            using var cmd = new SQLiteCommand(stm, con); 

            using SQLiteDataReader rdr = cmd.ExecuteReader(); //executes the reader
            List<Book> allBooks = new List<Book>();

            while(rdr.Read()) //reads all the data to the book list
            {
                allBooks.Add(new Book(){Id =  rdr.GetInt32(0), Title = rdr.GetString(1), Author = rdr.GetString(2)}); 
            }
            return allBooks;
        }

        public Book GetBook(int id)
        {
            string cs = @"URI=file:C:\Source\FallRepos\WebBasedApplication\book.db";
            using var con = new SQLiteConnection(cs);

            con.Open();

            string stm = "SELECT * FROM books WHERE id = @id";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            return new Book(){Id =  rdr.GetInt32(0), Title = rdr.GetString(1), Author = rdr.GetString(2)};
        }
    }
}
