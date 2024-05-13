using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Kol1.DTO;
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
            command.CommandText =
                "select Imie, Nazwisko, Pseudonim,u.IdUtwor, u.NazwaUtworu, u.CzasTrwania, u.IdAlbum from Muzyk " +
                "INNER JOIN WykonawcaUtworu WU on Muzyk.IdMuzyk = WU.IdMuzyk " +
                "Inner Join Utwor U on WU.IdUtwor = U.IdUtwor where Muzyk.IdMuzyk = @id";
            command.Parameters.AddWithValue("@id", id);
            var name = "";
            var surname = "";
            var sceneName = "";
            IList<Song> songs = new List<Song>();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                name = reader["Imie"].ToString();
                surname = reader["Nazwisko"].ToString();
                sceneName = reader["Pseudonim"].ToString();
                float val = Convert.ToSingle(reader["CzasTrwania"].ToString());
                songs.Add(new Song()
                {
                    IdSong = (int)reader["IdUtwor"],
                    Duration = val,
                    SongName = (string)reader["NazwaUtworu"],
                    Album = (int)reader["IdAlbum"]
                });
            }

            return new MusicianSongs()
            {
                IdMusician = id,
                Name = name,
                Surname = surname,
                SceneName = sceneName,
                Songs = songs
            };
        }
    }

    public async Task<MusicianSongs> AddMusicianSongs(MusicianSongsDTO musicianSongsDto)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            await using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            DbTransaction dbTransaction = await connection.BeginTransactionAsync();
            command.Transaction = dbTransaction as SqlTransaction;
            try
            {
                command.CommandText =
                    "insert into Muzyk(imie, nazwisko, pseudonim) values (@Imie, @Nazwisko, @Pseudonim)";
                command.Parameters.AddWithValue("@Imie", musicianSongsDto.Name);
                command.Parameters.AddWithValue("@Nazwisko", musicianSongsDto.Surname);
                command.Parameters.AddWithValue("@Pseudonim", musicianSongsDto.SceneName);
                MusicianSongs musicianSongs = new MusicianSongs()
                {
                    Name = musicianSongsDto.Name,
                    Surname = musicianSongsDto.Surname,
                    SceneName = musicianSongsDto.SceneName
                };
                int musicianId = GetId(musicianSongs);
                int songId = musicianSongsDto.Songs;

                command.CommandText = "INSERT INTO WykonawcaUtworu (idmuzyk, idutwor) " +
                                      "VALUES (@idmuzyk,@idutwor);";
                command.Parameters.AddWithValue("@idmuzyk", musicianId);
                command.Parameters.AddWithValue("@idutwor", songId);
                await command.ExecuteNonQueryAsync();


                dbTransaction.Commit();
                return new MusicianSongs()
                {
                    IdMusician = musicianId,
                    Name = musicianSongsDto.Name,
                    SceneName = musicianSongsDto.SceneName,
                    Songs = null
                };
            }
            catch (ArgumentException e)
            {
                dbTransaction.Rollback();
                Console.WriteLine(e);
            }
        }

        return null;
    }

    public int GetId(MusicianSongs musicianSongs)
    {
        return 0;
    }
}