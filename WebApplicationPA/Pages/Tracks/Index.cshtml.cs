using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using WebApplicationPA.Pages.Albums;

namespace WebApplicationPA.Pages.Tracks
{
    public class IndexModel : PageModel
    {
        public List<TrackInfo> Tracks = new List<TrackInfo>();

        string connectionString = "Server=localhost;Port=3306;Database=music;Uid=root;Pwd=root;";
        
        
        public void OnGet()
        {
            String albumId = Request.Query["album-id"];

            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=music;Uid=root;Pwd=root;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM tracks WHERE albums_ID = @id", connection);
                    command.Parameters.AddWithValue("@id", albumId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TrackInfo t = new TrackInfo()
                            {
                                ID = "" + reader.GetInt32(0),
                                trackName = reader.GetString(1),
                                number = reader.GetInt32(2).ToString(),
                            };
                            Tracks.Add(t);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
            }
        }
    }

    public class TrackInfo
    {
        public string ID { get; set; }
        public string trackName { get; set; }
        public string number { get; set; }

    }
}
