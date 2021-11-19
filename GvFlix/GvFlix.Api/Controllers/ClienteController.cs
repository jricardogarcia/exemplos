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
    /// Controller para manipulação dos clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly GvFlixContext _context;

        public ClienteController(GvFlixContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obter a lista de todos os clientes.
        /// </summary>
        /// <returns>Lista de todos os clientes da aplicação.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        /// <summary>
        /// Método para obter um cliente específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Cliente do identificador informado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Método para alterar os dados de um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <param name="ator">Entidade com os novos valores.</param>
        /// <returns>Sem retorno caso obtenha sucesso na alteração.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        /// Método para adicionar um novo cliente.
        /// </summary>
        /// <param name="ator">Entidade do cliente para ser adicionado.</param>
        /// <returns>Entidade do cliente adicionado.</returns>
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Método para excluir um cliente pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Sem retorno caso obtenha sucesso na exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Método responsável por verificar se o cliente já existe na aplicação.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Verdadeiro caso o cliente já exista.</returns>
        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
