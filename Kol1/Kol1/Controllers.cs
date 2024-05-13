using System;
using System.Threading.Tasks;
using Kol1.Interfaces;
using Kol1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kol1;

[ApiController]
[Route("api/musicians")]
public class Controllers : ControllerBase
{
    private readonly IMusicianService _musicianService;

    public Controllers(IMusicianService musicianService)
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
    
}