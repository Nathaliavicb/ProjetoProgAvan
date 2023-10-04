using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebApplicationPA.Pages.Albums
{
    public class IndexModel : PageModel
    {
        public List<AlbumInfo> Albums = new List<AlbumInfo>();

        string connectionString = "Server=localhost;Port=3306;Database=music;Uid=root;Pwd=root;";
        public void OnGet()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM ALBUMS", connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AlbumInfo a = new AlbumInfo()
                        {
                            ID = "" + reader.GetInt32(0),
                            AlbumName = reader.GetString(1),
                            ArtitsName = reader.GetString(2),
                            Year = "" + reader.GetInt32(3),
                        };

                        Albums.Add(a);
                    }
                }
            }
            catch (Exception ex) 
            {
            }
        }
    }

    public class AlbumInfo
    {
        public string ID { get; set; }
        public string AlbumName { get; set; }
        public string ArtitsName { get; set; }
        public string Year { get; set; }

    }
}
