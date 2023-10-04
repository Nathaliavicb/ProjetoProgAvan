using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplicationPA.Pages.Albums
{
    public class CreateModel : PageModel
    {
        public AlbumInfo Album = new AlbumInfo();
        public string errorMessage = "";
        public string sucessMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Album.AlbumName = Request.Form["Album Name"];
            Album.ArtitsName = Request.Form["Artist Name"];
            Album.Year = Request.Form["Year"];

            if (Album.AlbumName.Length == 0 || Album.ArtitsName.Length == 0
                || Album.Year.Length == 0)
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
                    String sql = "INSERT INTO `albums`(`ALBUM_TITLE`, `ARTIST`, `YEAR`) VALUES (@albumtitle, @artist, @year)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@albumtitle", Album.AlbumName);
                        command.Parameters.AddWithValue("@artist", Album.ArtitsName);
                        command.Parameters.AddWithValue("@year", Album.Year);

                        command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Album.AlbumName = ""; Album.ArtitsName = ""; Album.Year = "";
            sucessMessage = "New Client Added Correctly";

            Response.Redirect("/Albums/Index");

        }
    }
}
