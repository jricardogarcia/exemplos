using Dal;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GvFlix.Api.Controllers
{
    /// <summary>
    /// Controller para manipulação dos atores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AtorController : ControllerBase
    {
        private readonly GvFlixContext _context;

        public AtorController(GvFlixContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obter a lista de todos os atores.
        /// </summary>
        /// <returns>Lista de todos os atores da aplicação.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ator>>> GetAtores()
        {
            return await _context.Ator.OrderBy(o => o.Nome).ToListAsync();
        }

        /// <summary>
        /// Método para obter um ator específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do ator.</param>
        /// <returns>Ator do identificador informado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ator>> GetAtor(int id)
        {
            var ator = await _context.Ator.FindAsync(id);
            if (ator == null)
            {
                return NotFound();
            }

            return ator;
        }

        /// <summary>
        /// Método para alterar os dados de um ator.
        /// </summary>
        /// <param name="id">Identificador do ator.</param>
        /// <param name="ator">Entidade com os novos valores.</param>
        /// <returns>Sem retorno caso obtenha sucesso na alteração.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtor(int id, [FromBody]Ator ator)
        {
            if (id != ator.Id)
            {
                return BadRequest();
            }

            _context.Entry(ator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Método para adicionar um novo ator.
        /// </summary>
        /// <param name="ator">Entidade do ator para ser adicionado.</param>
        /// <returns>Entidade do ator adicionado.</returns>
        [HttpPost]
        public async Task<ActionResult<Ator>> PostAtor([FromBody]Ator ator)
        {
            _context.Ator.Add(ator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtor", new { id = ator.Id }, ator);
        }

        /// <summary>
        /// Método para excluir um ator pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do ator.</param>
        /// <returns>Sem retorno caso obtenha sucesso na exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtor(int id)
        {
            var ator = await _context.Ator.FindAsync(id);
            if (ator == null)
            {
                return NotFound();
            }

            _context.Ator.Remove(ator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método responsável por verificar se o ator já existe na aplicação.
        /// </summary>
        /// <param name="id">Identificador do ator.</param>
        /// <returns>Verdadeiro caso o ator já exista.</returns>
        private bool AtorExists(int id)
        {
            return _context.Ator.Any(e => e.Id == id);
        }
    }
}
