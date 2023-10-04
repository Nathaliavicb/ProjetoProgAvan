using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using WebApplicationPA.Pages.Albums;

namespace WebApplicationPA.Pages.Tracks
{
    public class EditModel : PageModel
    {
        public TrackInfo Track = new TrackInfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=music;Uid=root;Pwd=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM `tracks` WHERE ID = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Track.ID = "" + reader.GetInt32(0);
                                Track.trackName = reader.GetString(1);
                                Track.number = reader.GetInt32(2).ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            String album_id = Request.Query["album-id"];

            Track.ID = Request.Query["id"];
            Track.trackName = Request.Form["track Name"];
            Track.number = Request.Form["number"];

            if (Track.trackName.Length == 0 || Track.number.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=music;Uid=root;Pwd=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE tracks SET track_title=@tracktitle , number=@number WHERE ID = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tracktitle", Track.trackName);
                        command.Parameters.AddWithValue("@number", Track.number);
                        command.Parameters.AddWithValue("@id", Track.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Tracks/Index?album-id=" + album_id);
        }
    }
}
