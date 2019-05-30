using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private string _phoneNumber;
    private string _email;
    private string _notes;
    private int _id;
    private int _stylistId;

    public Client (string name, string phoneNumber, string email, string notes, int stylistId, int id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _email = email;
      _notes = notes;
      _stylistId = stylistId;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetPhoneNumber()
    {
      return _phoneNumber;
    }

    public void SetPhoneNumber(string newPhoneNumber)
    {
      _phoneNumber = newPhoneNumber;
    }

    public string GetEmail()
    {
      return _email;
    }

    public void SetEmail(string newEmail)
    {
      _email = newEmail;
    }

    public string GetNotes()
    {
      return _notes;
    }

    public void SetNotes(string newNotes)
    {
      _notes = newNotes;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string phoneNumber = rdr.GetString(2);
        string email = rdr.GetString(3);
        string notes = rdr.GetString(4);
        int stylistId = rdr.GetInt32(5);
        Client newClient = new Client(name, phoneNumber, email, notes, stylistId, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
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

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE client_id = @clientId;";
      MySqlParameter thisId = new MySqlParameter("@clientId", this._id);
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
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
        bool nameEquality = this.GetName() == newClient.GetName();
        bool phoneNumberEquality = this.GetPhoneNumber() == newClient.GetPhoneNumber();
        bool emailEquality = this.GetEmail() == newClient.GetEmail();
        bool notesEquality = this.GetNotes() == newClient.GetNotes();
        bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
        return (nameEquality && phoneNumberEquality && emailEquality && notesEquality && stylistIdEquality && idEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (client_name, client_phone_number, client_email, client_notes, stylist_id) VALUES (@clientName, @clientPhoneNumber, @clientEmail, @clientNotes, @stylistId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName ="@clientName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter phoneNumber = new MySqlParameter();
      phoneNumber.ParameterName ="@clientPhoneNumber";
      phoneNumber.Value = this._phoneNumber;
      cmd.Parameters.Add(phoneNumber);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName ="@clientEmail";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      MySqlParameter notes = new MySqlParameter();
      notes.ParameterName ="@clientNotes";
      notes.Value = this._notes;
      cmd.Parameters.Add(notes);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName ="@stylistId";
      stylistId.Value = this._stylistId;
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
      cmd.CommandText = @"SELECT * FROM clients WHERE client_id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      string clientPhoneNumber = "";
      string clientEmail = "";
      string clientNotes = "";
      int clientStylistId = 0;
      while (rdr.Read())
      {
         clientId = rdr.GetInt32(0);
         clientName = rdr.GetString(1);
         clientPhoneNumber = rdr.GetString(2);
         clientEmail = rdr.GetString(3);
         clientNotes = rdr.GetString(4);
         clientStylistId = rdr.GetInt32(5);
      }
      Client foundClient= new Client(clientName, clientPhoneNumber, clientEmail, clientNotes, clientStylistId, clientId);
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
      return foundClient;
    }

    public void Edit(string newName, string newPhoneNumber, string newEmail, string newNotes)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET client_name = @newName WHERE client_id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter phoneNumber = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@newEmail";
      email.Value = newName;
      cmd.Parameters.Add(email);

      MySqlParameter notes = new MySqlParameter();
      notes.ParameterName = "@newNotes";
      notes.Value = newNotes;
      cmd.Parameters.Add(notes);

      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
