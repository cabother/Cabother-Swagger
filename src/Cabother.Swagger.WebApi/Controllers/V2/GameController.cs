using System;
using System.Collections.Generic;
using System.Net.Mime;
using Cabother.Swagger.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cabother.Swagger.WebApi.Controllers.V2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v{version:apiVersion}/games")]
    [ApiController, Produces(MediaTypeNames.Application.Json)]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Game> Get()
        {
            _logger.LogInformation("Get game names {DateTime}", DateTimeOffset.Now);
            var rng = new Random();
            return new List<Game> {
                new Game { Id = 1, Name = "Resident Evil Village" },
                new Game { Id = 2, Name = "God of War" },
                new Game { Id = 3, Name = "Halo Infinite" },
                new Game { Id = 4, Name = "Hitman 3" },
                new Game { Id = 5, Name = "Deathloop" },
             };
        }
    }
}
