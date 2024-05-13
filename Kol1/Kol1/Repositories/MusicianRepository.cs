using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Kol1.Interfaces;
using Kol1.Models;
using Microsoft.Extensions.Configuration;

namespace Kol1.Repositories;

public class MusicianRepository : IMusicianRepository
{
    private IConfiguration _configuration;

    public MusicianRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<MusicianSongs> GetMusicianSongsById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await using var command = new SqlCommand();
            command.Connection = connection;
            await connection.OpenAsync();
            command.CommandText = "select Imie, Nazwisko, Pseudonim, u.NazwaUtworu, u.CzasTrwania, u.IdAlbum from Muzyk " +
                                  "INNER JOIN WykonawcaUtworu WU on Muzyk.IdMuzyk = WU.IdMuzyk " +
                                  "Inner Join Utwor U on WU.IdUtwor = U.IdUtwor where Muzyk.IdMuzyk = @id";
            command.Parameters.AddWithValue("@id", id);
            var name = "";
            var surname = "";
            var sceneName = "";
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
            name = reader["Imie"].ToString();
            surname = reader["Nazwisko"].ToString();
            sceneName = reader["Pseudonim"].ToString();

            
                // localTitle = reader.GetValue(title).ToString();
                // localGeners.Add(reader.GetValue(name).ToString());
                
                Song song = new Song()
                {
                    
                };
            }

            return new MusicianSongs()
            {
                IdMusician = id,
                Name = name,
                Surname = surname,
                SceneName = sceneName,
                Songs = null
            };
        }

        return null;
    }
}