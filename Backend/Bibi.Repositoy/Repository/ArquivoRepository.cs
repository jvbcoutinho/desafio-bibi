using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibi.Domain;
using BlueModas.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Bibi.Repositoy.Repository
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly BibiContext _context;

        public ArquivoRepository(BibiContext context)
        {
            _context = context;
        }
        public async Task Criar(Arquivo arquivo)
        {
            await _context.Arquivo.AddAsync(arquivo);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Arquivo arquivo)
        {
            _context.Arquivo.Remove(arquivo);
            await _context.SaveChangesAsync();
        }

        public async Task<Arquivo> ObterPorResourceId(string id)
        {
            return await this._context.Arquivo
                .AsNoTracking()
                .Where(x => x.ResourceId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Arquivo>> ObterTodos()
        {
            return await this._context.Arquivo.ToListAsync();
        }

        public async Task Atualizar(Arquivo arquivo)
        {
            _context.Entry(arquivo).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }

    }
}