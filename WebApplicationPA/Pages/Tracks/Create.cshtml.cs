using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplicationPA.Pages.Tracks
{
    public class CreateModel : PageModel
    {
        public TrackInfo Track = new TrackInfo();
        public string errorMessage = "";
        public string sucessMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            String albumId = Request.Query["album-id"];
            Track.trackName = Request.Form["Track Name"];
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
                    String sql = "INSERT INTO `tracks`(`track_title`, `number`, `albums_ID`) VALUES (@tracktitle, @number, @albumsID)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@albumsID", albumId);
                        command.Parameters.AddWithValue("@tracktitle", Track.trackName);
                        command.Parameters.AddWithValue("@number", Track.number);

                        command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Track.trackName = ""; Track.number = "";
            sucessMessage = "New Client Added Correctly";

            Response.Redirect("/Tracks/Index?album-id=" + albumId);

        }
    }
}
