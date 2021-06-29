using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoJogos.Controllers.V1 {
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase{
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService) {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, 
            Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5) {

            var games = await _gameService.Get(page, quantity);
            if (games.Count == 0) {
                return NoContent();
            }
            return Ok(games);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid id) {
            var game = await _gameService.Get(id);
            if (game == null) {
                return NoContent();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel) {
            try {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            } 
            catch (RegisteredGameException ex) {
                return UnprocessableEntity("Já existe um jogo cadastrado com este nome para esta produtora");
            }
        }

        //Update the whole resource
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel gameInputModel) {
            try {
                await _gameService.Update(id, gameInputModel);
                return Ok();
            }
            catch (NotRegisteredGameException ex) {
                return NotFound("O jogo especificado não foi encontrado");
            }   
        }

        [HttpPatch("{id:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromRoute] double price) {
            try {
                await _gameService.Update(id, price);
                return Ok();
            }
            catch (NotRegisteredGameException ex) {
                return NotFound("O jogo especificado não foi encontrado");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid id) {
            try {
                await _gameService.Remove(id);
            }
            catch (NotRegisteredGameException ex) {
                return NotFound("O jogo especificado não foi encontrado");
            }
            return Ok();
        }

    }
}