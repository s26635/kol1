using System;
using System.Threading.Tasks;
using Kol1.DTO;
using Kol1.Interfaces;
using Kol1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kol1;

[ApiController]
[Route("api/musicians")]
public class Controller : ControllerBase
{
    private readonly IMusicianService _musicianService;

    public Controller(IMusicianService musicianService)
    {
        _musicianService = musicianService;
    }
    [HttpGet("{id}/songs")]
    public async Task<IActionResult> GetMusicianSongsById(int id)
    {
        try
        {
            MusicianSongs result = await _musicianService.GetMusicianSongsById(id);
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddMusicianSongs(MusicianSongsDTO musicianSongsDto)
    {
        MusicianSongs result = await _musicianService.AddMusicianSongs(musicianSongsDto);
        return Created(Request.Path.Value ?? "api/musicians",result);
    }
}