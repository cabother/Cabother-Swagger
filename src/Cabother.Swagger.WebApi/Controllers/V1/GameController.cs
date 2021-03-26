using System;
using System.Collections.Generic;
using System.Net.Mime;
using Cabother.Swagger.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cabother.Swagger.WebApi.Controllers.V1
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
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
                new Game { Id = 1, Name = "Halo Infinite" },
                new Game { Id = 2, Name = "Resident Evil Village" },
                new Game { Id = 3, Name = "God of War" },
                new Game { Id = 4, Name = "Deathloop" },
                new Game { Id = 5, Name = "Hitman 3" },
             };
        }
    }
}
