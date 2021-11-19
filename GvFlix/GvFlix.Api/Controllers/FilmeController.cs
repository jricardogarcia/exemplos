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
    /// Controller para manipulação dos filmes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly GvFlixContext _context;

        public FilmeController(GvFlixContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obter a lista de todos os filmes.
        /// </summary>
        /// <returns>Lista de todos os filmes da aplicação.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilme()
        {
            return await _context.Filme.ToListAsync();
        }

        /// <summary>
        /// Método para obter um filme específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do filme.</param>
        /// <returns>Filme do identificador informado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        /// <summary>
        /// Método para alterar os dados de um filme.
        /// </summary>
        /// <param name="id">Identificador do filme.</param>
        /// <param name="ator">Entidade com os novos valores.</param>
        /// <returns>Sem retorno caso obtenha sucesso na alteração.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, Filme filme)
        {
            if (id != filme.Id)
            {
                return BadRequest();
            }

            _context.Entry(filme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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
        /// Método para adicionar um novo filme.
        /// </summary>
        /// <param name="ator">Entidade do filme para ser adicionado.</param>
        /// <returns>Entidade do filme adicionado.</returns>
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(Filme filme)
        {
            _context.Filme.Add(filme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilme", new { id = filme.Id }, filme);
        }

        /// <summary>
        /// Método para excluir um filme pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do filme.</param>
        /// <returns>Sem retorno caso obtenha sucesso na exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método responsável por verificar se o filme já existe na aplicação.
        /// </summary>
        /// <param name="id">Identificador do filme.</param>
        /// <returns>Verdadeiro caso o filme já exista.</returns>
        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.Id == id);
        }
    }
}
