using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;

    public Client(string name, int stylistId, int id = 0)
    {
      _id = id;
      _stylistId = stylistId;
      _name = name;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && stylistEquality);
      }
    }
    public override int GetHashCode()
    {
         return this.GetName().GetHashCode();
    }

    public string GetName()
    {
        return _name;
    }
    public int GetId()
    {
        return _id;
    }
    public int GetStylistId()
    {
        return _stylistId;
    }
    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylistId);";

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = _name;
        cmd.Parameters.Add(name);

        MySqlParameter stylistId = new MySqlParameter();
        stylistId.ParameterName = "@stylistId";
        stylistId.Value = _stylistId;
        cmd.Parameters.Add(stylistId);


        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public static Client Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int clientId = 0;
        string clientName = "";
        int clientStylistId = 0;

        while(rdr.Read())
        {
          clientId = rdr.GetInt32(0);
          clientName = rdr.GetString(1);
          clientStylistId = rdr.GetInt32(2);
        }
        Client newClient = new Client(clientName, clientStylistId, clientId);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return newClient;
    }
    public static List<Client> GetAll()
    {
        List<Client> allClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          int clientStylistId = rdr.GetInt32(2);
          Client newClient = new Client(clientName, clientStylistId, clientId);
          allClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allClients;
    }
    public static List<Client> GetAllStylistsClients(int searchForStylistId)
    {
        List<Client> allClients = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = searchForStylistId;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int clientId = rdr.GetInt32(0);
          string clientName = rdr.GetString(1);
          int clientStylistId = rdr.GetInt32(2);
          Client foundClient = new Client(clientName, clientStylistId, clientId);
          allClients.Add(foundClient);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allClients;
    }
    public static void DeleteClient(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
    public void UpdateClient(string updName, int updStylistId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE clients SET name = @updateName, stylist_id = @updateStylistId WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@updateName";
        name.Value = updName;
        cmd.Parameters.Add(name);

        MySqlParameter updateStylistId = new MySqlParameter();
        updateStylistId.ParameterName = "@updateStylistId";
        updateStylistId.Value = updStylistId;
        cmd.Parameters.Add(updateStylistId);

        cmd.ExecuteNonQuery();
        _name = updName;
        _stylistId = updStylistId;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
  }
}
