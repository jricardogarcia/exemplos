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
    /// Controller para manipulação dos diretores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DiretorController : ControllerBase
    {
        private readonly GvFlixContext _context;

        public DiretorController(GvFlixContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obter a lista de todos os diretores.
        /// </summary>
        /// <returns>Lista de todos os diretores da aplicação.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diretor>>> GetDiretor()
        {
            return await _context.Diretor.ToListAsync();
        }

        /// <summary>
        /// Método para obter um diretor específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do diretor.</param>
        /// <returns>Diretor do identificador informado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Diretor>> GetDiretor(int id)
        {
            var diretor = await _context.Diretor.FindAsync(id);
            if (diretor == null)
            {
                return NotFound();
            }

            return diretor;
        }

        /// <summary>
        /// Método para alterar os dados de um diretor.
        /// </summary>
        /// <param name="id">Identificador do diretor.</param>
        /// <param name="ator">Entidade com os novos valores.</param>
        /// <returns>Sem retorno caso obtenha sucesso na alteração.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiretor(int id, [FromBody]Diretor diretor)
        {
            if (id != diretor.Id)
            {
                return BadRequest();
            }

            _context.Entry(diretor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiretorExists(id))
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
        /// Método para adicionar um novo diretor.
        /// </summary>
        /// <param name="ator">Entidade do diretor para ser adicionado.</param>
        /// <returns>Entidade do diretor adicionado.</returns>
        [HttpPost]
        public async Task<ActionResult<Diretor>> PostDiretor([FromBody]Diretor diretor)
        {
            _context.Diretor.Add(diretor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiretor", new { id = diretor.Id }, diretor);
        }

        /// <summary>
        /// Método para excluir um diretor pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do diretor.</param>
        /// <returns>Sem retorno caso obtenha sucesso na exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiretor(int id)
        {
            var diretor = await _context.Diretor.FindAsync(id);
            if (diretor == null)
            {
                return NotFound();
            }

            _context.Diretor.Remove(diretor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método responsável por verificar se o diretor já existe na aplicação.
        /// </summary>
        /// <param name="id">Identificador do diretor.</param>
        /// <returns>Verdadeiro caso o diretor já exista.</returns>
        private bool DiretorExists(int id)
        {
            return _context.Diretor.Any(e => e.Id == id);
        }
    }
}
