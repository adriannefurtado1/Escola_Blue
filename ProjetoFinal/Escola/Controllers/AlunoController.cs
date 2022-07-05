using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola.Context;
using Escola.Models;

namespace Escola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno()
        {
          if (_context.Aluno == null)
          {
              return NotFound();
          }
            return await _context.Aluno.ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
          if (_context.Aluno == null)
          {
              return NotFound();
          }
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound($"Aluno {id} não encontrado na base de dados.");
            }

            return aluno;
        }

        // PUT: api/Aluno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {

            var Turmas = await _context.Turma.ToListAsync();
            bool turmaValida = false;

            if (id != aluno.Id)
            {
                return BadRequest();
            }

            foreach (Turma _turma in Turmas)
            {
                if (_turma.Id == aluno.TurmaID)
                {
                    if (_turma.Ativo == true)
                    {
                        turmaValida = true;
                    }
                }
            }

            if (turmaValida == false)
            {
                return NotFound("Não foi informada uma turma válida.");
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content($"Aluno {id} alterado com sucesso.");

        }

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            var Turmas = await _context.Turma.ToListAsync();
            bool turmaValida = false;

            if (_context.Aluno == null)
            {
                return Problem("Entity set 'EscolaContext.Aluno'  is null.");
            }

            foreach (Turma _turma in Turmas)
            {
                if (_turma.Id == aluno.TurmaID)
                {
                    if (_turma.Ativo == true)
                    {
                        turmaValida = true;
                    }
                }
            }

            if (turmaValida == false)
            {
                return NotFound("Não foi informada uma turma válida.");
            }

            _context.Aluno.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);

        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            if (_context.Aluno == null)
            {
                return NotFound();
            }
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound($"Aluno {id} não encontrada na base de dados.");
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return Content($"Aluno {id} deletado com sucesso.");
        }

        private bool AlunoExists(int id)
        {
            return (_context.Aluno?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
