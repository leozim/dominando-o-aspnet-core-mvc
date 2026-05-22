using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvc.Data;
using AspnetCoreMvc.Extensions;
using AspnetCoreMvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspnetCoreMvc.Controllers
{
    // [Authorize(Roles = "Admin")] // not recommended use roles like that.
                                 // insert into actions only and do not insert into entire Controller
    [Authorize]
    [Route("meus-produtos")]
    public class ProdutosController : Controller
    {
        private readonly ApplicationContext _context;

        public ProdutosController(ApplicationContext context)
        {
            _context = context;
        }
        
        [ClaimsAuthorize("Produtos", "VI")]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User.Identity;
            
            return _context.Produtos != null
                ? View(await _context.Produtos.ToListAsync())
                : Problem("Entity set 'AppDbContext.Produtos'  is null.");
        }
        
        [ClaimsAuthorize("Produtos", "VI")]
        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-novo")]
        public IActionResult CriarNovoProduto()
        {
            return View("Create");
        }
        
        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost("criar-novo")]
        //[ValidateAntiForgeryToken]  ja estáa aplicado globalmente
        public async Task<IActionResult> CriarNovoProduto([Bind("Id,Nome,ImagemUpload,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(produto.ImagemUpload, imgPrefixo))
                {
                    return View(produto);
                }

                produto.Imagem = imgPrefixo + produto.ImagemUpload.FileName;
                
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("Create", produto);
        }
        
        [ClaimsAuthorize("Produtos", "ED")]
        [Route("editar-produto/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        
        [ClaimsAuthorize("Produtos", "ED")]
        [HttpPost("editar-produto/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ImagemUpload,Valor")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            var produtoDb = await _context.Produtos
                .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    // Evita perder imagem do produto existente
                    produto.Imagem = produtoDb.Imagem;
                    if (produto.ImagemUpload != null)
                    {
                        var imgPrefixo = Guid.NewGuid() + "_";
                        if (!await UploadArquivo(produto.ImagemUpload, imgPrefixo))
                        {
                            return View(produto);
                        }

                        produto.Imagem = imgPrefixo + produto.ImagemUpload.FileName;
                    }
                    
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        [Route("excluir/{id}")]
        [ClaimsAuthorize("Produtos", "EX")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Produtos", "EX")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPreFixo)
        {
            if (arquivo.Length <= 0) return false;
            
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPreFixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
